using System.Collections.Generic;
using UnityEngine;

public class TurretUnlockManager : MonoBehaviour
{
	public static TurretUnlockManager instance;
	public List<TurretData> allTurrets;

	[HideInInspector] public List<TurretData> unlockedTurrets = new List<TurretData>();
	[HideInInspector] public List<TurretData> lockedTurrets = new List<TurretData>();

	private void Awake()
	{
		instance = this;
	}

	void Start()
	{
		// Al empezar todas bloqueadas
		lockedTurrets = new List<TurretData>(allTurrets);

		//desbloquear una inicial
		UnlockTurret(lockedTurrets[0]);
	}

	public List<TurretData> GetRandomOptions(int amount) //Devuelve una lista de torretas aleatorias sin repetir
	{
		List<TurretData> options = new List<TurretData>();
		List<TurretData> copy = new List<TurretData>(lockedTurrets);

		for (int i = 0; i < amount && copy.Count > 0; i++)
		{
			int index = Random.Range(0, copy.Count);
			options.Add(copy[index]);
			copy.RemoveAt(index);
		}

		return options;
	}

	public void UnlockTurret(TurretData turret) //Mueve una torreta de bloqueadas a desbloqueadas
	{
		if (!lockedTurrets.Contains(turret)) return;

		lockedTurrets.Remove(turret);
		unlockedTurrets.Add(turret);

		Debug.Log("Desbloqueada: " + turret.turretName);
	}
}