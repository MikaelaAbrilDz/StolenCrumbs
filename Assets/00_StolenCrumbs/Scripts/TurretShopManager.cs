using UnityEngine;

public class TurretShopManager : MonoBehaviour
{
    [SerializeField] Transform[] pivots;
    [SerializeField] RectTransform contentBox;
    int unlockedIndex = 0;

    public void AddToShop(TurretData turret)
    {
        Instantiate(turret.turretUiPrefab, pivots[unlockedIndex]);
        contentBox.sizeDelta = new Vector2(contentBox.sizeDelta.x, contentBox.sizeDelta.y + 350);
        unlockedIndex++;
    }
}
