using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TurretOption : MonoBehaviour
{
    [SerializeField] TurretUnlockManager manager;

    public Image icon;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI basePriceText;
    public TextMeshProUGUI cooldownText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI descriptionText;
    public Image damageTypeIcon;
    [SerializeField] ComboInfoBehavior[] comboInfoBehaviors;
    [SerializeField] Sprite fireIcon, waterIcon, gasIcon, frozenIcon, markedIcon, soapIcon;

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
        cooldownText.text = data.fireRate / 10f + " seg.";
        damageText.text = "" + data.damage;
        basePriceText.text = "" + data.basePrice;

        foreach (var behaviour in comboInfoBehaviors) behaviour.gameObject.SetActive(false);

        switch (data.damageTypes[0])
        {
            case TurretData.damageType.gas:
                damageTypeIcon.sprite = gasIcon;
                descriptionText.text = "Nothing too special on this one, I guess...";
                SetComboInfoBehavior(0, fireIcon, "Big old kaboom! Area damage");
                SetComboInfoBehavior(1, waterIcon, "Sticky stuff, reduced movement!");

                break;
            case TurretData.damageType.fire:
                damageTypeIcon.sprite = fireIcon;
                descriptionText.text = "Burned bugs take damage every half a second!";
                SetComboInfoBehavior(0, waterIcon, "No more fire...");
                break;
            case TurretData.damageType.water:
                damageTypeIcon.sprite = waterIcon;
                descriptionText.text = "Burned bugs take damage every half a second!";
                SetComboInfoBehavior(0, fireIcon, "No fire here!");
                SetComboInfoBehavior(1, gasIcon, "Sticky stuff, reduced movement!");
                SetComboInfoBehavior(2, soapIcon, "Extra bubbles, increased damage");
                break;
            case TurretData.damageType.frozen:
                damageTypeIcon.sprite = frozenIcon;
                descriptionText.text = "Frozen bugs are three times slower!";
                SetComboInfoBehavior(0, fireIcon, "From frozen to wet, nice?");
                SetComboInfoBehavior(1, waterIcon, "Prolongued ice, keep it up!");
                break;
            case TurretData.damageType.marked:
                damageTypeIcon.sprite = markedIcon;
                descriptionText.text = "Marked bugs take double damage from every turret!";
                break;
            case TurretData.damageType.soap:
                damageTypeIcon.sprite = soapIcon;
                descriptionText.text = "After bugs touch soap, they take damage every half a second!";
                SetComboInfoBehavior(0, gasIcon, "Soap kaboom! Area damage");
                SetComboInfoBehavior(1, waterIcon, "Extra bubbles, keep the status effect!");
                break;

        }
    }
    void SetComboInfoBehavior(int i, Sprite icon, string description)
    {
        comboInfoBehaviors[i].gameObject.SetActive(true);
        comboInfoBehaviors[i].icon.sprite = icon;
        comboInfoBehaviors[i].description.text = description;
    }
    public void OnClick()
    {
        if (turret != null)
        {
            manager.UnlockTurret(turret);
        }
    }
}
