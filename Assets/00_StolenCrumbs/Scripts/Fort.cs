using UnityEngine;

public class Fort : CheckerPlaceable
{
	int maxLife;
	int currentLife;

	private void Start()
	{
		maxLife = 100;
		currentLife = maxLife;

	}

	public void GetDamaged(int damaged)
	{
		currentLife -= damaged;
		print(currentLife);

	}
}
