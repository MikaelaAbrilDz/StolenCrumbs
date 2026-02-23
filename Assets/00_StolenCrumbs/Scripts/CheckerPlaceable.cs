using UnityEngine;

public class CheckerPlaceable : MonoBehaviour
{
	[SerializeField] protected CheckerManager parentChecker;

	public void SetParentChecker(CheckerManager parent)
	{
		parentChecker = parent;
		transform.parent = parent.transform;
	}
}
