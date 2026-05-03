using UnityEngine;

public class ParticleAligner : MonoBehaviour
{
    Transform parentTransform;
    Vector3 oldPos;
    void Start()
    {
        parentTransform = transform.parent;
        oldPos = parentTransform.position;
        InvokeRepeating(nameof(Align), .01f, .01f);
    }

    void Align()
    {
        Vector3 alignment = parentTransform.position - oldPos;
        transform.up = alignment;
        oldPos = parentTransform.position;
    }
}
