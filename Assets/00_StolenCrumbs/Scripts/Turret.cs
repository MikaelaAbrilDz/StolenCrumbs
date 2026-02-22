using UnityEngine;

public class Turret : CheckerPlaceable
{
	int fireRate;
	int damage;
	int range;
	int paidPric;
	enum damageType
	{
		pesticide, electric, water, torch, glue
	}
	damageType[] damageTypes;
	EnemyBase nearestEnemy;

	[SerializeField] LayerMask enemyLayer;
	void Start()
	{

	}

	void Update()
	{
		Shoot();
	}

	public void Shoot()
	{
		EnemyBase target = FindTarget();

		if (target == null)
			return;

		//target.GetDamaged(damage, damageTypes);
	}

	EnemyBase FindTarget()
	{
		Collider[] hits = Physics.OverlapSphere(transform.position, range, enemyLayer);

		float shortestDistance = Mathf.Infinity;
		EnemyBase nearestEnemy = null;

		foreach (Collider hit in hits)
		{
			EnemyBase enemy = hit.GetComponent<EnemyBase>();

			if (enemy == null)
				continue;

			float sqrDistance = (enemy.transform.position - transform.position).sqrMagnitude;

			if (sqrDistance < shortestDistance)
			{
				shortestDistance = sqrDistance;
				nearestEnemy = enemy;
			}
		}
		return nearestEnemy;
	}
	public void Sell()
	{

	}
}
