using UnityEngine;

public class DamageNumberManager : MonoBehaviour
{
    public static DamageNumberManager instance;
    [SerializeField] Canvas damageNumberCanvas;
    [SerializeField] GameObject damageNumberPrefab;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    public void SpawnDamageNumber (Vector3 worldPosition, int damage)
    {
        GameObject num = Instantiate(damageNumberPrefab,worldPosition, Quaternion.identity);
        num.GetComponentInChildren<DamageNumber>().SetDamage(damage);
    }
  
}
