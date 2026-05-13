using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using NUnit.Framework;
using System.Collections.Generic;

public class TurretMover : MonoBehaviour
{
    [SerializeField] LayerMask checkerMask;
    [SerializeField] Camera cam;
    public TurretData turretData;
    int movePrice = 5;
    public bool isInEditMode = false;

    TurretPrevisualizer previsualizer;
    private bool isPicked;
    private float placingRange = 1f;
    public static List<CheckerManager> checkersInRange = new List<CheckerManager>();
    public static List<CheckerManager> checkersInRangePrep = new List<CheckerManager>();


    void Start()
    {
        previsualizer = FindAnyObjectByType<TurretPrevisualizer>(FindObjectsInactive.Include);
    }
    private void Update()
    {
        if (isPicked)
        {
            CheckCurrentRange();
        }
    }
    public void OnClick(InputAction.CallbackContext ctx)
    {
        if (isInEditMode && !isPicked && ctx.performed)
        {
            Vector3 mousePos = cam.ScreenToWorldPoint(Mouse.current.position.value) + Vector3.forward;
            Collider2D[] avalaibleCheckers = Physics2D.OverlapCircleAll(mousePos, placingRange, checkerMask);
            CheckerManager finalChecker = null;
            foreach (Collider2D checker in avalaibleCheckers)
            {
                    if (finalChecker == null)
                    {
                        finalChecker = checker.GetComponent<CheckerManager>();
                    }
                    else if ((finalChecker.transform.position - mousePos).magnitude > (checker.transform.position - mousePos).magnitude)
                    {
                        finalChecker = checker.GetComponent<CheckerManager>();
                    }
            }
            GrabTurret(finalChecker.GetComponentInChildren<Turret>()?.turretData);
            finalChecker.GetComponentInChildren<Turret>()?.gameObject.SetActive(false);
        }
    }
    void CheckCurrentRange()
    {

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
        GetSideCheckers(finalChecker.GetComponent<CheckerManager>(), turretData.range - 1);
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
    public void GrabTurret(TurretData data)
    {
        turretData = data;
        if (CurrencyManager.RemoveCurrency(movePrice))
        {
            isPicked = true;
            previsualizer.Using(turretData.icon);
        }
    }

    public void PlaceTurret(InputAction.CallbackContext context)
    {
        if (isPicked && context.canceled)
        {
            FindAnyObjectByType<TurretShopManager>().GetComponent<AppearDisappearUI_Manager>().Appear();
            isPicked = false;
            previsualizer.StopUsing();

            Camera mainCam = Camera.main;
            GameObject placedturret = Instantiate(turretData.turretPrefab, mainCam.ScreenToWorldPoint(Mouse.current.position.value) + Vector3.forward, Quaternion.identity);
            Collider2D[] avalaibleCheckers = Physics2D.OverlapCircleAll(placedturret.transform.position, placingRange, checkerMask);
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

            if (finalChecker == null)
            {
                CurrencyManager.AddCurrency(movePrice);
                Destroy(placedturret);
            }
            else
            {
                turretData.placedTurrets++;

                placedturret.GetComponent<Turret>().SetParentChecker(finalChecker);
                placedturret.transform.localPosition = Vector3.zero;

            }
        }
    }

    public void ChangeEditMode()
    {
        isInEditMode = !isInEditMode;
    }
}
