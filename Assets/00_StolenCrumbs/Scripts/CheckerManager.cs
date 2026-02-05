using UnityEngine;

public class CheckerManager : MonoBehaviour
{
    public CheckerManager[] sideCheckers;
    void Start()
    {
        SetReferences();
    }

    private void SetReferences()
    {
        Vector2 direction = new Vector2(Mathf.Cos(30), Mathf.Sin(30));
        sideCheckers[0] = Physics2D.Raycast(transform.position + (Vector3)direction * 0.3f, direction, 2f).collider?.gameObject.GetComponent<CheckerManager>();
        direction = new Vector2(Mathf.Cos(0), Mathf.Sin(0));
        sideCheckers[1] = Physics2D.Raycast(transform.position + (Vector3)direction * 0.3f, direction, 2f).collider?.gameObject.GetComponent<CheckerManager>();
        direction = new Vector2(Mathf.Cos(30), -Mathf.Sin(30));
        sideCheckers[2] = Physics2D.Raycast(transform.position + (Vector3)direction * 0.3f, direction, 2f).collider?.gameObject.GetComponent<CheckerManager>();
        direction = new Vector2(-Mathf.Cos(30), -Mathf.Sin(30));
        sideCheckers[3] = Physics2D.Raycast(transform.position + (Vector3)direction * 0.3f, direction, 2f).collider?.gameObject.GetComponent<CheckerManager>();
        direction = new Vector2(-Mathf.Cos(0), -Mathf.Sin(0));
        sideCheckers[4] = Physics2D.Raycast(transform.position + (Vector3)direction * 0.3f, direction, 2f).collider?.gameObject.GetComponent<CheckerManager>();
        direction = new Vector2(-Mathf.Cos(30), Mathf.Sin(30));
        sideCheckers[5] = Physics2D.Raycast(transform.position + (Vector3)direction * 0.3f, direction, 2f).collider?.gameObject.GetComponent<CheckerManager>();
    }
}
