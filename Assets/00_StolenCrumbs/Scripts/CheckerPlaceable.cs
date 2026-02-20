using UnityEngine;

public class CheckerPlaceable : MonoBehaviour
{
    CheckerManager parentChecker;

    public void SetParentChecker(CheckerManager parent)
    {
        parentChecker = parent;
    }
}
