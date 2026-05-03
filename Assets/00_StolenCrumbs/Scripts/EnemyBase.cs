using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static TurretData;
using System;

public class EnemyBase : CheckerPlaceable
{
	public bool isDead = false;
	int maxLife;
	[SerializeField] int currentLife = 40;
	int damage = 2;
	[SerializeField ] float movementSpeed = 1.0f;
	float currentMovementSpeed;
	[SerializeField ] int prize = 1;
	int killingPrice;
	GameObject visual;
	[HideInInspector] public List<TurretData.damageType> statusEffects = new List<TurretData.damageType>();
	[HideInInspector] public List<float> statusEffectsTimeLeft = new List<float>();
    [HideInInspector] public LTDescr currentTween;

	[SerializeField] GameObject explosionVisual;

	Animator anim;

	public enum bugType
	{
		flyer, armored
	}
	bugType[] bugTypes;
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }
    private void OnEnable()
	{
		Invoke(nameof(MoveNextPath), 1.0f);
	}
    private void Update()
    {
		WearOffStatus();
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
			statusEffects.Add(damageTypes[i]);
			statusEffectsTimeLeft.Add(damageTimes[i]);
        }
    }
    public void GetDamaged(int damage, damageType[] damageTypes, GameObject hitVisual)
	{

		foreach (var damageType in damageTypes) //Checks synergies
		{
			foreach (var statusEffect in statusEffects) 
			{
				if (damageType == TurretData.damageType.explosion && statusEffect == TurretData.damageType.water)
				{
					return;
                }
				if (damageType == TurretData.damageType.fire && statusEffect == TurretData.damageType.gas)
				{
                    GetDamaged(8, new damageType[1] { TurretData.damageType.explosion }, explosionVisual);
                    foreach (CheckerManager checker in parentChecker.sideCheckers)
					{
						foreach (EnemyBase sideEnemy in checker.GetComponentsInChildren<EnemyBase>())
						{
							sideEnemy.GetDamaged(25, new damageType[1] { TurretData.damageType.explosion }, null);
						}
						
					}
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
					statusEffects.Add(TurretData.damageType.sticky);
					statusEffectsTimeLeft.Add(3);
                }
				if (damageType == TurretData.damageType.water && statusEffect == TurretData.damageType.gas)
				{
					statusEffects.Add(TurretData.damageType.sticky);
					statusEffectsTimeLeft.Add(3);
                }

            }
		}
		anim.SetTrigger("damaged");
		currentLife = Mathf.Max(currentLife - damage, 0);

		if (hitVisual) Instantiate(hitVisual, transform.position, Quaternion.identity);

		if (currentLife == 0) Die();

	}
	void MoveNextPath()
	{
		Path path = parentChecker.GetComponentInChildren<Path>();
        if (path != null)
		{
			if (path.direction > 2) anim.gameObject.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
			else anim.gameObject.transform.localScale = new Vector3(-0.7f, 0.7f, 0.7f);

			currentMovementSpeed = movementSpeed;
            foreach (var statusEffect in statusEffects)
            {
                if (statusEffect == TurretData.damageType.sticky)
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
		CurrencyManager.AddCurrency(prize);
		Destroy(gameObject, 1.5f);
	}
	void HitFort()
	{
		gameObject.SetActive(false);
		Destroy(gameObject);
	}

	IEnumerator PassNextChecker(float delay, CheckerManager parent)
	{
		yield return new WaitForSeconds(delay);
		SetParentChecker(parent);
	}
}
