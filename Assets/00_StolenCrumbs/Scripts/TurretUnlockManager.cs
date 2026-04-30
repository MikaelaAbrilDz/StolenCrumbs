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
    TurretData[] currentOptions = new TurretData[3];
    //AÑADIR: referencias instancias torretas elegibles (poner en array)


    private void Awake()
    {
        instance = this;
        turretShopManager = FindAnyObjectByType<TurretShopManager>();
    }

    void Start()
    {
        // Al empezar todas bloqueadas
        lockedTurrets = new List<TurretData>(allTurrets);

        //desbloquear una inicial
        if (lockedTurrets.Count > 0)
            UnlockTurret(lockedTurrets[0]);
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
            //AÑADIR: a cada indice su torreta

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
        Time.timeScale = 0f;
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
        Time.timeScale = 1f;
        Debug.Log("Desbloqueada: " + turret.turretName);
    }
}