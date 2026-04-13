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
        if (lifeText) lifeText.text = currentLife.ToString();
    }

    public void GetDamaged(int damaged)
	{
		currentLife -= damaged;
	}
}
