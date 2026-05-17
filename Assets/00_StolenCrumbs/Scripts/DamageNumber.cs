using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    TextMeshProUGUI damageText;
    private void Awake()
    {
        damageText = GetComponent<TextMeshProUGUI>();
    }
   
    public void SetDamage(int damage)
    {
        damageText.text = damage.ToString();

        float randomSize = Random.Range(0.8f, 1.2f);
        transform.localScale = Vector3.one * randomSize;

        Vector2 targetPose = transform.position + new Vector3(0, Random.Range(1f, 1.2f), 0);
        LeanTween.move(gameObject, targetPose, 1f).setEase(LeanTweenType.easeOutCubic);
        Invoke(nameof(Destroy), 1f);
    }
    void Destroy()
    {
        Destroy(transform.parent.gameObject);
    }
}
