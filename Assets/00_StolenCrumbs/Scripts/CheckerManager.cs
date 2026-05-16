using UnityEngine;

public class CheckerManager : MonoBehaviour
{
	float angle;
	public SpriteRenderer rangeShower;

	public CheckerManager[] sideCheckers;

	public bool isTurretPlaceable, isPathPlaceable;
	void Awake()
	{
		//Sets angle to 30 (change that value if the angle changes) and converts it to radian to be used by sine and cosine functions
		angle = 30 * Mathf.Deg2Rad;
		SetReferences();

		if (!isTurretPlaceable) rangeShower.color = Color.red;
	}

	private void Update()
	{
		if (rangeShower.enabled)
		{
			if (!TurretPlacer.checkersInRange.Contains(this))
			{
				rangeShower.enabled = false;
			}

		}
	}

	private void SetReferences()
	{
		//Casts a raycast in every hexagonal direction based on the provided angle and references all contiguous checkers (offset to the ray to not hit its own collider)

		Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
		sideCheckers[0] = Physics2D.Raycast(transform.position + (Vector3)direction * 0.5f, direction, 2f).collider?.gameObject.GetComponent<CheckerManager>();
		direction = new Vector2(Mathf.Cos(0), Mathf.Sin(0));
		sideCheckers[1] = Physics2D.Raycast(transform.position + (Vector3)direction * 0.5f, direction, 2f).collider?.gameObject.GetComponent<CheckerManager>();
		direction = new Vector2(Mathf.Cos(angle), -Mathf.Sin(angle));
		sideCheckers[2] = Physics2D.Raycast(transform.position + (Vector3)direction * 0.5f, direction, 2f).collider?.gameObject.GetComponent<CheckerManager>();
		direction = new Vector2(-Mathf.Cos(angle), -Mathf.Sin(angle));
		sideCheckers[3] = Physics2D.Raycast(transform.position + (Vector3)direction * 0.5f, direction, 2f).collider?.gameObject.GetComponent<CheckerManager>();
		direction = new Vector2(-Mathf.Cos(0), -Mathf.Sin(0));
		sideCheckers[4] = Physics2D.Raycast(transform.position + (Vector3)direction * 0.5f, direction, 2f).collider?.gameObject.GetComponent<CheckerManager>();
		direction = new Vector2(-Mathf.Cos(angle), Mathf.Sin(angle));
		sideCheckers[5] = Physics2D.Raycast(transform.position + (Vector3)direction * 0.5f, direction, 2f).collider?.gameObject.GetComponent<CheckerManager>();
	}

	public void CreateChild(CheckerPlaceable childToPlace)
	{
		Instantiate(childToPlace, transform);
	}
}
