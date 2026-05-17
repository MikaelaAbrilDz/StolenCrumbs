using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    [SerializeField] private AudioSource soundFXObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        //spawn in gameobject with audio source component, set the clip to the provided one and play it, then destroy the gameobject after the clip is done
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();
        float clipLength = audioClip.length;  
        Destroy(audioSource.gameObject, audioClip.length);
    }
    public void PlaySoundFXClip(AudioClip audioClip)
    {
        PlaySoundFXClip(audioClip, transform, 1f);
    }
}
