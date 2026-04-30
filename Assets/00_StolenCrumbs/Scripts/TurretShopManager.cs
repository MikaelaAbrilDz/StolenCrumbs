using UnityEngine;

public class TurretShopManager : MonoBehaviour
{
    [SerializeField] Transform[] pivots;
    int unlockedIndex = 0;

    public void AddToShop(TurretData turret)
    {
        Instantiate(turret.turretUiPrefab, pivots[unlockedIndex]);
        unlockedIndex++;
    }
}
