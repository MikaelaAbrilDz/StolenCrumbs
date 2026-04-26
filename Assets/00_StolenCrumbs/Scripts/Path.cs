using UnityEngine;
using System.Collections.Generic;

public class Path : CheckerPlaceable
{
    public int direction;
    public int stepsUntilFort;

    [SerializeField] Sprite[] pathSprites;
    SpriteRenderer[] spriteRenderers;
    public void SetPath(int prevDir, int direction, int stepsUntilFort)
    {
        spriteRenderers =  GetComponentsInChildren<SpriteRenderer>();
        this.direction = (prevDir + 3) % 6;
        this.stepsUntilFort = stepsUntilFort;

        SetVisuals(prevDir, direction);
    }
    private void SetVisuals(int prevDir, int direction)
    {
        spriteRenderers[0].sprite = pathSprites[(prevDir + 3) % 6];
        spriteRenderers[1].sprite = pathSprites[direction];
    }
}
