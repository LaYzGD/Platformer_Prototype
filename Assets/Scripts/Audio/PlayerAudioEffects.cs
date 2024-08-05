using UnityEngine;

public class PlayerAudioEffects : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _loopAudioSource;

    public void PlayLoopSound(AudioClip clip)
    {
        _loopAudioSource.clip = clip;
        _loopAudioSource.Play();
    }

    public void StopLoopSound() 
    {
        _loopAudioSource.Stop();
    }

    public void PlaySound(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }

    public void PlayRandomClip(AudioClip[] clips) 
    {
        _audioSource.PlayOneShot(clips[Random.Range(0, clips.Length)]);
    }
}
