using UnityEngine;

public class AppearDisappearUI_Manager : MonoBehaviour
{
    [SerializeField] Transform show, hide;
    bool shown = true;
    public void Change()
    {
        if (shown) Disppear();
        else Appear();
    }
    public void Appear()
    {
        shown = true;
        LeanTween.move(gameObject, show.position, .5f);
    }
    public void Disppear()
    {
        shown = false;
        LeanTween.move(gameObject, hide.position, .5f);
    }
}
