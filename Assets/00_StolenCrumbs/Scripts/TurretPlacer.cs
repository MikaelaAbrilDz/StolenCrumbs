using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class TurretPlacer : MonoBehaviour
{
	private TextMeshProUGUI nameText;
	private Image iconTurret;
	public GameObject turretPrefab;

	TurretPrevisualizer previsualizer;
	private bool isPicked;

	void Start()
	{
		iconTurret = GetComponent<Image>();
		nameText = GetComponentInChildren<TextMeshProUGUI>();
		iconTurret.sprite = turretPrefab.GetComponent<Turret>().turretData.icon;
		nameText.text = turretPrefab.GetComponent<Turret>().turretData.turretName;
		
		previsualizer = FindAnyObjectByType<TurretPrevisualizer>();
	}

	void Update()
	{

	}

	public void GrabTurret()
	{
		isPicked = true;
		previsualizer.Using(iconTurret.sprite);

	}

	public void PlaceTurret(InputAction.CallbackContext context)
	{
		if (isPicked && context.canceled)
		{
			isPicked=false;
			previsualizer.StopUsing();

			Camera mainCam = Camera.main;
			GameObject placedturret = Instantiate(turretPrefab, mainCam.ScreenToWorldPoint(Mouse.current.position.value) + Vector3.forward, Quaternion.identity);
		}
	}
}
