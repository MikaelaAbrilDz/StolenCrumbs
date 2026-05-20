using System.Collections.Generic;
using UnityEngine;

public class TurretUnlockManager : MonoBehaviour
{
    public static TurretUnlockManager instance;
    TurretShopManager turretShopManager;
    public List<TurretData> allTurrets;

    [HideInInspector] public List<TurretData> unlockedTurrets = new List<TurretData>();
    [HideInInspector] public List<TurretData> lockedTurrets = new List<TurretData>();


    [SerializeField] private TurretOption[] turretSlots;
    [SerializeField] private GameObject bg;
    TurretData[] currentOptions = new TurretData[3];

    [SerializeField] AudioClip turretSelection;

    private void Awake()
    {
        instance = this;
        turretShopManager = FindAnyObjectByType<TurretShopManager>();
        // Al empezar todas bloqueadas
        lockedTurrets = new List<TurretData>(allTurrets);
    }

    List<TurretData> GetRandomOptions(int amount) //Devuelve una lista de torretas aleatorias sin repetir
    {
        List<TurretData> options = new List<TurretData>();
        List<TurretData> copy = new List<TurretData>(lockedTurrets);

        for (int i = 0; i < amount && copy.Count > 0; i++)
        {
            int index = Random.Range(0, copy.Count);
            options.Add(copy[index]);
            copy.RemoveAt(index);
        }

        return options;
    }

    public void ShowUnlockOptions() //Muestra las opciones de torretas desbloqueables en el canvas
    {

        List<TurretData> options = GetRandomOptions(3);
        for (int i = 0; i < currentOptions.Length; i++)
        {
            if (i < options.Count)
            {
                currentOptions[i] = options[i];
                turretSlots[i].SetTurret(currentOptions[i]);
                turretSlots[i].gameObject.SetActive(true);
            }
            else
            {
                currentOptions[i] = null;
                turretSlots[i].gameObject.SetActive(false);
            }
        }
        bg.SetActive(true);
        TimeScaleManager.PauseTime();
    }

    public void UnlockTurret(TurretData turret) //Mueve una torreta de bloqueadas a desbloqueadas
    {
        if (!lockedTurrets.Contains(turret)) return;

        lockedTurrets.Remove(turret);
        unlockedTurrets.Add(turret);

        foreach (var item in turretSlots)
        {
            item.gameObject.SetActive(false);
        }
        turretShopManager.AddToShop(turret);
        turret.placedTurrets = 0;
        bg.SetActive(false);
        TimeScaleManager.UnPauseTime();
        FindAnyObjectByType<SFXManager>().PlaySoundFXClip(turretSelection, transform, 1f);
        Debug.Log("Desbloqueada: " + turret.turretName);
    }
}