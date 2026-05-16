using UnityEngine;

public class SortingLayerManager : MonoBehaviour
{
    SpriteRenderer[] sprites;
    int[] sortingOrders;
    void Start()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();
        sortingOrders = new int[sprites.Length];
        for (int i = 0; i < sprites.Length; i++)
        {
            sortingOrders[i] = sprites[i].sortingOrder;
        }
    }

    void Update()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            if (sprites[i].sortingLayerName != "BG") sprites[i].sortingOrder = sortingOrders[i] - (int)(transform.position.y * 10) * 100;
        }
    }
}
