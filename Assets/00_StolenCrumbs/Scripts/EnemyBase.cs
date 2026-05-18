using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using static TurretData;
using System;

public class EnemyBase : CheckerPlaceable
{
    public float visualSize = 0.7f;
    public bool isDead = false;
    int maxLife;
    [SerializeField] int currentLife = 40;
    int damage = 2;
    [SerializeField] float movementSpeed = 1.0f;
    float currentMovementSpeed;
    [SerializeField] int prize = 1;
    int killingPrice;
    GameObject visual;
    [HideInInspector] public List<TurretData.damageType> statusEffects = new List<TurretData.damageType>();
    [HideInInspector] public List<float> statusEffectsTimeLeft = new List<float>();
    [HideInInspector] public LTDescr currentTween;

    [SerializeField] GameObject explosionVisual, soapExplosionVisual, boltsExplosionVisual;

    Animator anim;
    [SerializeField] Slider lifeBar;
    [SerializeField] Image lifeBarFill;

    [SerializeField] Image[] statusIcons;
    [SerializeField] Sprite fireIcon, waterIcon, gasIcon, stickyIcon, frozenIcon, markedIcon, soapIcon;

    [SerializeField] AudioClip deadSound;
    [SerializeField] AudioClip takeDamageSound;
    [SerializeField] AudioClip eatSound;
    [SerializeField] AudioClip explosionSound;
    [SerializeField] AudioClip soapExplosionSound;


    public enum bugType
    {
        flyer, armored
    }
    bugType[] bugTypes;
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        lifeBar.maxValue = currentLife;
        lifeBar.value = currentLife;
        InvokeRepeating(nameof(GetStatusConditions), 0, 0.5f);
    }
    private void OnEnable()
    {
        Invoke(nameof(MoveNextPath), 1.0f);
    }
    private void Update()
    {
        WearOffStatus();
        UpdateIcons();
    }
    private void UpdateIcons()
    {
        foreach (var icon in statusIcons) icon.gameObject.SetActive(false);

        for (int i = 0; i < statusEffects.Count; i++)
        {
            statusIcons[i].gameObject.SetActive(true);
            switch (statusEffects[i])
            {
                case damageType.gas:
                    statusIcons[i].sprite = gasIcon;
                    break;
                case damageType.water:
                    statusIcons[i].sprite = waterIcon;
                    break;
                case damageType.fire:
                    statusIcons[i].sprite = fireIcon;
                    break;
                case damageType.soap:
                    statusIcons[i].sprite = soapIcon;
                    break;
                case damageType.sticky:
                    statusIcons[i].sprite = stickyIcon;
                    break;
                case damageType.frozen:
                    statusIcons[i].sprite = frozenIcon;
                    break;
                case damageType.marked:
                    statusIcons[i].sprite = markedIcon;
                    break;
            }
        }
    }
    private void GetStatusConditions()
    {
        if (statusEffects.Contains(TurretData.damageType.fire))
        {
            GetDamaged(1, new damageType[0], null, null);
        }
        if (statusEffects.Contains(TurretData.damageType.soap))
        {
            GetDamaged(2, new damageType[0], null, null);
        }
    }
    private void WearOffStatus()
    {
        for (int i = 0; i < statusEffectsTimeLeft.Count; i++)
        {
            statusEffectsTimeLeft[i] -= Time.deltaTime;
            if (statusEffectsTimeLeft[i] <= 0)
            {
                statusEffectsTimeLeft.RemoveAt(i);
                statusEffects.RemoveAt(i);
            }
        }
    }
    public void GetStatusEffect(damageType[] damageTypes, float[] damageTimes)
    {
        for (int i = 0; i < damageTypes.Length; i++)
        {
            if (!statusEffects.Contains(damageTypes[i]))
            {
                statusEffects.Add(damageTypes[i]);
                statusEffectsTimeLeft.Add(damageTimes[i]);
            }
            else
            {
                statusEffectsTimeLeft[statusEffects.IndexOf(damageTypes[i])] = damageTimes[i];
            }
        }
    }
    public void GetDamaged(int damage, damageType[] damageTypes, GameObject hitVisual, AudioClip hitSound)
    {
        int finalDamage = damage;
        foreach (var damageType in damageTypes) //Checks synergies
        {
            if (damageType == damageType.marked) finalDamage *= 2;

            foreach (var statusEffect in new List<damageType>(statusEffects))
            {
                if (damageType == TurretData.damageType.explosion && statusEffect == TurretData.damageType.water)
                {
                    return;
                }
                if (damageType == TurretData.damageType.fire && statusEffect == TurretData.damageType.gas)
                {
                    GetDamaged(25, new damageType[1] { TurretData.damageType.explosion }, explosionVisual, explosionSound);
                    foreach (CheckerManager checker in parentChecker.sideCheckers)
                    {
                        foreach (EnemyBase sideEnemy in checker.GetComponentsInChildren<EnemyBase>())
                        {
                            sideEnemy.GetDamaged(25, new damageType[1] { TurretData.damageType.explosion }, null, null);
                        }

                    }
                }
                if (damageType == TurretData.damageType.gas && statusEffect == TurretData.damageType.soap)
                {
                    GetStatusEffect(new damageType[1] { TurretData.damageType.soap }, new float[1] { 2f });
                    GetDamaged(2, new damageType[1] { TurretData.damageType.soap }, soapExplosionVisual, soapExplosionSound);
                    foreach (CheckerManager checker in parentChecker.sideCheckers)
                    {
                        foreach (EnemyBase sideEnemy in checker.GetComponentsInChildren<EnemyBase>())
                        {
                            sideEnemy.GetStatusEffect(new damageType[1] { TurretData.damageType.soap }, new float[1] { 2f });
                            sideEnemy.GetDamaged(2, new damageType[1] { TurretData.damageType.soap }, null, null);
                        }

                    }
                }
                if (damageType == TurretData.damageType.fire && statusEffect == TurretData.damageType.frozen)
                {
                    statusEffectsTimeLeft.RemoveAt(statusEffects.IndexOf(TurretData.damageType.frozen));
                    statusEffects.Remove(TurretData.damageType.frozen);
                    GetStatusEffect(new damageType[1] { TurretData.damageType.water }, new float[1] { 1f });
                }
                if (damageType == TurretData.damageType.fire && statusEffect == TurretData.damageType.water)
                {
                    statusEffectsTimeLeft.RemoveAt(statusEffects.IndexOf(TurretData.damageType.fire));
                    statusEffects.Remove(TurretData.damageType.fire);
                }
                if (damageType == TurretData.damageType.water && statusEffect == TurretData.damageType.fire)
                {
                    statusEffectsTimeLeft.RemoveAt(statusEffects.IndexOf(TurretData.damageType.fire));
                    statusEffects.Remove(TurretData.damageType.fire);
                }
                if (damageType == TurretData.damageType.gas && statusEffect == TurretData.damageType.water)
                {
                    GetStatusEffect(new damageType[1] { TurretData.damageType.sticky }, new float[1] { 3 });
                }
                if (damageType == TurretData.damageType.water && statusEffect == TurretData.damageType.gas)
                {
                    GetStatusEffect(new damageType[1] { TurretData.damageType.sticky }, new float[1] { 3 });
                }
                if (damageType == TurretData.damageType.water && statusEffect == TurretData.damageType.soap)
                {
                    statusEffectsTimeLeft[statusEffects.IndexOf(TurretData.damageType.soap)] += 0.5f;
                }
                if (damageType == TurretData.damageType.soap && statusEffect == TurretData.damageType.water)
                {
                    finalDamage *= 2;
                }
                if (damageType == TurretData.damageType.water && statusEffect == TurretData.damageType.frozen)
                {
                    statusEffectsTimeLeft[statusEffects.IndexOf(TurretData.damageType.frozen)] += 1f;
                }

            }
        }
        if (takeDamageSound != null)
        {
            FindAnyObjectByType<SFXManager>().PlaySoundFXClip(takeDamageSound, transform, 1f);
        }
        if (hitSound != null)
        {
            FindAnyObjectByType<SFXManager>().PlaySoundFXClip(hitSound, transform, 1f);
        }
        DamageNumberManager.instance.SpawnDamageNumber(transform.position + Vector3.up * 1.5f, finalDamage);
        anim.SetTrigger("damaged");
        currentLife = Mathf.Max(currentLife - finalDamage, 0);
        lifeBar.value = currentLife;
        if (lifeBar.value <= lifeBar.maxValue / 3) lifeBarFill.color = Color.red;
        if (hitVisual) Instantiate(hitVisual, transform.position, Quaternion.identity);

        if (currentLife == 0) Die();

    }
    void MoveNextPath()
    {
        Path path = parentChecker.GetComponentInChildren<Path>();
        if (path != null)
        {
            if (path.direction > 2) anim.gameObject.transform.localScale = new Vector3(visualSize, visualSize, visualSize);
            else anim.gameObject.transform.localScale = new Vector3(-visualSize, visualSize, visualSize);

            currentMovementSpeed = movementSpeed;
            foreach (var statusEffect in statusEffects)
            {
                if (statusEffect == TurretData.damageType.sticky)
                {
                    currentMovementSpeed = movementSpeed * 3;
                }
                if (statusEffect == TurretData.damageType.frozen)
                {
                    currentMovementSpeed = movementSpeed * 3;
                }
            }

            currentTween = LeanTween.move(gameObject, parentChecker.sideCheckers[path.direction].transform.position, currentMovementSpeed).setOnComplete(MoveNextPath);
            StartCoroutine(PassNextChecker(currentMovementSpeed / 2, parentChecker.sideCheckers[path.direction]));
        }
        Fort fort = parentChecker.GetComponentInChildren<Fort>();
        if (fort != null)
        {
            fort.GetDamaged(damage);
            HitFort();
        }

    }
    void Die()
    {
        if (isDead) return;
        isDead = true;
        LeanTween.cancel(gameObject);
        anim.SetBool("isDead", true);
        if (deadSound != null) FindAnyObjectByType<SFXManager>().PlaySoundFXClip(deadSound, transform, 1f);
        StartCoroutine(GetTheBolts());
    }
    IEnumerator GetTheBolts()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        Instantiate(boltsExplosionVisual, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1.2f);
        CurrencyManager.AddCurrency(prize);
        SFXManager sfx = FindAnyObjectByType<SFXManager>();
        sfx.PlaySoundFXClip(sfx.gainMoneySound, transform, 1f);
        Destroy(gameObject);
    }
    void HitFort()
    {
        FindAnyObjectByType<SFXManager>().PlaySoundFXClip(eatSound, transform, 1f);
        isDead = true;
        anim.SetTrigger("ate");
        Destroy(gameObject, 2);
    }

    IEnumerator PassNextChecker(float delay, CheckerManager parent)
    {
        yield return new WaitForSeconds(delay);
        SetParentChecker(parent);
    }
}
