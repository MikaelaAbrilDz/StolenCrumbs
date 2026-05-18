using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurretOption : MonoBehaviour
{
    [SerializeField] TurretUnlockManager manager;

    public Image icon;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;

    public TurretData turret;

    private void Start()
    {
        SetTurret(turret);
    }

    public void SetTurret(TurretData data)
    {
        turret = data;
        icon.sprite = data.icon;
        nameText.text = data.turretName;
        descriptionText.text = $"Damage: {data.damage}\nCooldown: {data.fireRate}\nRange: {data.range}";
    }

    public void OnClick()
    {
        if (turret != null)
        {
            manager.UnlockTurret(turret);
        }
    }
}
