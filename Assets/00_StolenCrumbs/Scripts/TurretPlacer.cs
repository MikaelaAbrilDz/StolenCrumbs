using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using NUnit.Framework;
using System.Collections.Generic;

public class TurretPlacer : MonoBehaviour
{
	[SerializeField] LayerMask checkerMask;
	private TextMeshProUGUI nameText;
	private Image iconTurret;
	public GameObject turretPrefab;

	TurretPrevisualizer previsualizer;
	private bool isPicked;
	private float placingRange = 1f;
	public static List<CheckerManager> checkersInRange = new List<CheckerManager>();
	public static List<CheckerManager> checkersInRangePrep = new List<CheckerManager>();
    void Start()
	{
        iconTurret = GetComponent<Image>();
		nameText = GetComponentInChildren<TextMeshProUGUI>();
		previsualizer = FindAnyObjectByType<TurretPrevisualizer>(FindObjectsInactive.Include);
		SetData();
	}
	private void Update()
	{
		if (isPicked)
		{
			CheckCurrentRange();
		}
	}

	public void SetData()
	{
        TurretData data = turretPrefab.GetComponent<Turret>().turretData;

		iconTurret.sprite = turretPrefab.GetComponent<Turret>().turretData.icon;
		nameText.text = data.turretName + " ("+ data.FinalPrice() + " bolts)";
	}

	void CheckCurrentRange()
	{
		TurretData data = turretPrefab.GetComponent<Turret>().turretData;
		
		Camera mainCam = Camera.main;
		Vector3 cursorPos = mainCam.ScreenToWorldPoint(Mouse.current.position.value); //Posición del cursor

		Collider2D[] posibleCheckers = Physics2D.OverlapCircleAll(cursorPos, 5f, checkerMask);

		Collider2D finalChecker = posibleCheckers[0];

		foreach (Collider2D checker in posibleCheckers)
		{
			if ((finalChecker.transform.position - cursorPos).magnitude > (checker.transform.position - cursorPos).magnitude)
			{
				finalChecker = checker;
			}
		}
		checkersInRangePrep.Clear();
		GetSideCheckers(finalChecker.GetComponent<CheckerManager>(), data.range - 1);
		checkersInRange.Clear();
		checkersInRange = checkersInRangePrep;
	}

	void GetSideCheckers(CheckerManager finalChecker, int rangeLeft)
	{
		int newRange = rangeLeft - 1;
		foreach (CheckerManager sideChecker in finalChecker.sideCheckers)
		{
			if (sideChecker != null)
			{
				sideChecker.rangeShower.enabled = true;
				checkersInRangePrep.Add(sideChecker);
				if (rangeLeft == 0)
				{

				}
				else
				{
					GetSideCheckers(sideChecker, newRange);
				}
			}
		}
	}
	public void GrabTurret()
	{
		TurretData data = turretPrefab.GetComponent<Turret>().turretData;
        if (CurrencyManager.RemoveCurrency(data.FinalPrice()))
        {
            SFXManager sfx = FindAnyObjectByType<SFXManager>();
			sfx.PlaySoundFXClip(sfx.buyTurretSound, transform, 0.5f);
            FindAnyObjectByType<TurretShopManager>().GetComponent<AppearDisappearUI_Manager>().Disppear();
	        isPicked = true;
			previsualizer.Using(iconTurret.sprite);    
        }
		else
		{
			SFXManager sfx = FindAnyObjectByType<SFXManager>();
			sfx.PlaySoundFXClip(sfx.noMoneySound, transform, 0.5f);
        }
	}

	public void PlaceTurret(InputAction.CallbackContext context)
	{
		if (isPicked && context.canceled)
		{
			FindAnyObjectByType<TurretShopManager>().GetComponent<AppearDisappearUI_Manager>().Appear();
			isPicked=false;
			previsualizer.StopUsing();

			Camera mainCam = Camera.main;
			GameObject placedturret = Instantiate(turretPrefab, mainCam.ScreenToWorldPoint(Mouse.current.position.value) + Vector3.forward, Quaternion.identity);
			Collider2D[] avalaibleCheckers = Physics2D.OverlapCircleAll(placedturret.transform.position, placingRange, LayerMask.GetMask("Checkers"));
			CheckerManager finalChecker = null;
			foreach (Collider2D checker in avalaibleCheckers)
			{
				if (checker.GetComponent<CheckerManager>().isTurretPlaceable && checker.GetComponentInChildren<Path>() == null && checker.GetComponentInChildren<Fort>() == null && checker.GetComponentInChildren<Turret>() == null)
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
				SFXManager sfx = FindAnyObjectByType<SFXManager>();
				sfx.PlaySoundFXClip(sfx.turretPlaced, transform, 0.5f);
                data.placedTurrets++;
				SetData();

				placedturret.GetComponent<Turret>().SetParentChecker(finalChecker);
				placedturret.transform.localPosition = Vector3.zero;

            }

        }
	}
}
