using System.Collections;
using UnityEngine;

public class Fan : ITriggerable
{
    [SerializeField] private float _force;
    [SerializeField] private GameObject _fanEffects;
    [SerializeField] private ParticleSystem _endEffect;
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private ForceType _forceType;
    [SerializeField] private bool _isOn;
    [SerializeField] private AudioSource _audio;

    private void Start()
    {
        _fanEffects.SetActive(_isOn);
        _collider.enabled = _isOn;
        
        if (_isOn)
        {
            _audio.Play();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!_isOn)
        {
            return;
        }

        if (collision.TryGetComponent(out IForceControllable forceControllable))
        {
            forceControllable.ControlForce(transform.up * _force, _forceType);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IForceControllable forceControllable))
        {
            forceControllable.StopForceControl();
        }
    }

    public override void Trigger()
    {
        _isOn = !_isOn;
        _fanEffects.SetActive(_isOn);
        _collider.enabled = _isOn;

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
        _collider.enabled = _isOn;

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
