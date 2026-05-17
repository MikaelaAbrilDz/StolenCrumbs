using UnityEngine;

public class UiSfxManager : MonoBehaviour
{
    public void PlaySoundFXClip(AudioClip audioClip)
    {
        SFXManager.instance.PlaySoundFXClip(audioClip, transform, 1f);
    }
}
