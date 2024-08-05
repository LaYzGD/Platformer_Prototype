using System.Collections;
using UnityEngine;

public class Fan : ITriggerable
{
    [SerializeField] private float _force;
    [SerializeField] private GameObject _fanEffects;
    [SerializeField] private ParticleSystem _endEffect;
    [SerializeField] private FanCollider _collider;
    [SerializeField] private ForceType _forceType;
    [SerializeField] private bool _isOn;
    [SerializeField] private AudioSource _audio;

    private void Start()
    {
        _collider.Initialize(_force, _forceType);
        _fanEffects.SetActive(_isOn);
        _collider.gameObject.SetActive(_isOn);
        
        if (_isOn)
        {
            _audio.Play();
        }
    }

    public override void Trigger()
    {
        _isOn = !_isOn;
        _fanEffects.SetActive(_isOn);
        _collider.gameObject.SetActive(_isOn);

        if (!_fanEffects.activeSelf)
        {
            _endEffect.Play();
            _audio.Stop();
            return;
        }

        _audio.Play();
    }

    public override void UnTrigger()
    {
        _isOn = !_isOn;
        _fanEffects?.SetActive(_isOn);
        _collider.gameObject.SetActive(_isOn);

        if (!_fanEffects.activeSelf)
        {
            _endEffect.Play();
            _audio.Stop();
            return;
        }

        _audio.Play();
    }

    public override void Trigger(float time)
    {
        Trigger();
        StartCoroutine(UnTriggerByTime(time));
    }

    private IEnumerator UnTriggerByTime(float time) 
    {
        yield return new WaitForSecondsRealtime(time);
        UnTrigger();
    }
}
