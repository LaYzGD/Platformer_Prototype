using UnityEngine;

public class PlayerAudioEffects : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _loopAudioSource;
    [SerializeField][Range(0.5f, 1.2f)] private float _minPitch;
    [SerializeField][Range(0.5f, 1.2f)] private float _maxPitch;

    private float _defaultPitch;

    private void Start()
    {
        _defaultPitch = _audioSource.pitch;
    }

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
        _audioSource.pitch = Random.Range(_minPitch, _maxPitch);
        _audioSource.PlayOneShot(clip);
    }

    public void PlayRandomClip(AudioClip[] clips) 
    {
        _audioSource.pitch = Random.Range(_minPitch, _maxPitch);
        _audioSource.PlayOneShot(clips[Random.Range(0, clips.Length)]);
    }
}
