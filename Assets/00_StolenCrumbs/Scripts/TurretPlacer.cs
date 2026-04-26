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
	private float placingRange = 1f;

    void Start()
	{
        iconTurret = GetComponent<Image>();
		nameText = GetComponentInChildren<TextMeshProUGUI>();
		previsualizer = FindAnyObjectByType<TurretPrevisualizer>(FindObjectsInactive.Include);
		SetData();
	}

	public void SetData()
	{
        TurretData data = turretPrefab.GetComponent<Turret>().turretData;

		iconTurret.sprite = turretPrefab.GetComponent<Turret>().turretData.icon;
		nameText.text = data.turretName + " ("+ data.FinalPrice() + " bolts)";
	}

	public void GrabTurret()
	{
		TurretData data = turretPrefab.GetComponent<Turret>().turretData;

        if (CurrencyManager.RemoveCurrency(data.FinalPrice()))
        {
	        isPicked = true;
			previsualizer.Using(iconTurret.sprite);    
        }
	}

	public void PlaceTurret(InputAction.CallbackContext context)
	{
		if (isPicked && context.canceled)
		{
			isPicked=false;
			previsualizer.StopUsing();

			Camera mainCam = Camera.main;
			GameObject placedturret = Instantiate(turretPrefab, mainCam.ScreenToWorldPoint(Mouse.current.position.value) + Vector3.forward, Quaternion.identity);
			Collider2D[] avalaibleCheckers = Physics2D.OverlapCircleAll(placedturret.transform.position, placingRange, LayerMask.GetMask("Checkers"));
			CheckerManager finalChecker = null;
			foreach (Collider2D checker in avalaibleCheckers)
			{
				if (checker.GetComponentInChildren<Path>() == null && checker.GetComponentInChildren<Fort>() == null && checker.GetComponentInChildren<Turret>() == null)
				{
					if (finalChecker == null)
					{
						finalChecker = checker.GetComponent<CheckerManager>();
					}
					else if ((finalChecker.transform.position - placedturret.transform.position).magnitude > (checker.transform.position - placedturret.transform.position).magnitude)
					{
						finalChecker = checker.GetComponent<CheckerManager>();
                    }
                }
            }

            TurretData data = turretPrefab.GetComponent<Turret>().turretData;

            if (finalChecker == null)
			{
                CurrencyManager.AddCurrency(data.FinalPrice());
                Destroy(placedturret);
			}
			else 
			{
				data.placedTurrets++;
				SetData();

				placedturret.GetComponent<Turret>().SetParentChecker(finalChecker);
				placedturret.transform.localPosition = Vector3.zero - Vector3.forward;

            }

        }
	}
}
