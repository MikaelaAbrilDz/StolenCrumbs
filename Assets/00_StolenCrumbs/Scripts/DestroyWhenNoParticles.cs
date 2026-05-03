using UnityEngine;

public class DestroyWhenNoParticles : MonoBehaviour
{
    void Update()
    {
        if(GetComponentInChildren<ParticleSystem>() == null) Destroy(gameObject);
    }
}
