using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TurretPrevisualizer : MonoBehaviour
{
	private Vector3 originalPosition;
	private bool isBeingUsed = false;
	private Image iconHolder;
	void Awake()
	{
		originalPosition = transform.position;
		iconHolder = GetComponent<Image>();
	}

	void Update()
	{
		if (isBeingUsed)
		{
			GetComponent<RectTransform>().position = Mouse.current.position.value;
		}
	}

	public void Using(Sprite icon)
	{
		gameObject.SetActive(true);
		isBeingUsed = true;
		iconHolder.sprite = icon;
	}

	public void StopUsing()
	{
		isBeingUsed = false;
		gameObject.SetActive(false);
	}
}
