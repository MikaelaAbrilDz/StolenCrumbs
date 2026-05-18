using UnityEngine;
using UnityEngine.Rendering;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    [SerializeField] private AudioSource soundFXObject;

    [SerializeField] public AudioClip turretPlaced;
    [SerializeField] public AudioClip noMoneySound;
    [SerializeField] public AudioClip clickSound;
    [SerializeField] public AudioClip startGameSound;
    [SerializeField] public AudioClip loseMoneySound;
    [SerializeField] public AudioClip gainMoneySound;

    [SerializeField] AudioSource globalAudioSource;
    [SerializeField] public AudioClip loseSound;
    [SerializeField] public AudioClip winSound;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        if (audioClip == null) return;
        //spawn in gameobject with audio source component, set the clip to the provided one and play it, then destroy the gameobject after the clip is done
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume/3;
        audioSource.Play();
        Destroy(audioSource.gameObject, audioClip.length);
    }
    public void PlaySoundFXClip(AudioClip audioClip)
    {
        PlaySoundFXClip(audioClip, transform, 1f);
    }
    public void PlayGlobalSoundFXClip(AudioClip audioClip)
    {
        PlayGlobalSoundFXClip(audioClip, 1);
    }
    public void PlayGlobalSoundFXClip(AudioClip audioClip, float volume)
    {
        globalAudioSource.clip = audioClip;
        globalAudioSource.volume = volume/3;
        globalAudioSource.Play();
    }
}
