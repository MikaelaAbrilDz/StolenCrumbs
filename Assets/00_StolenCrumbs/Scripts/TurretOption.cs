using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurretOption : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;

    TurretData turret;

    public void SetTurret(TurretData data)
    {
        turret = data;
        icon.sprite = data.icon;
        nameText.text = data.turretName;
        descriptionText.text = $"Damage: {data.damage}\nFire Rate: {data.fireRate}\nRange: {data.range}";
    }

    public void OnClick()
    {
        if (turret != null)
        {
            TurretUnlockManager.instance.UnlockTurret(turret);
            gameObject.SetActive(false);
        }
    }
}
