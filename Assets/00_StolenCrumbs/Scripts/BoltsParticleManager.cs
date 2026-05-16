using Unity.VisualScripting;
using UnityEngine;

public class BoltsParticleManager : MonoBehaviour
{
    Transform target;
    [SerializeField] GameObject forceField;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("BoltsTarget").transform;
        LeanTween.move(forceField, target, 1).setEaseInCubic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
