using UnityEngine;

public class PlayerAudioEffects : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    public void PlaySound(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }

    public void PlayRandomClip(AudioClip[] clips) 
    {
        _audioSource.PlayOneShot(clips[Random.Range(0, clips.Length)]);
    }
}
