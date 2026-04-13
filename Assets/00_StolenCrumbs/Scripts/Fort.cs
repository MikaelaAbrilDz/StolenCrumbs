using UnityEngine;
using TMPro;

public class Fort : CheckerPlaceable
{
	int maxLife;
	static int currentLife;
	[SerializeField]TextMeshProUGUI lifeText;

    private void Start()
	{
		maxLife = 20;
		currentLife = maxLife;
	}
    private void Update()
    {
        if (currentLife < 0) currentLife = 0;
        if (lifeText) lifeText.text = currentLife.ToString();

        if (currentLife == 0) WinLoseManager._gameState = WinLoseManager.GameState.Lost;
    }

    public void GetDamaged(int damaged)
	{
		currentLife -= damaged;
    }
}
