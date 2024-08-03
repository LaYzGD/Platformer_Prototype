using System;
using System.Collections.Generic;
using UnityEngine;

public class StickyLaserThrower : MonoBehaviour
{
    [SerializeField] private StickyLaser[] _stickyLasers;
    [SerializeField] private Transform _parent;
    [SerializeField] private Laser _laser;
    [SerializeField] private float _activationRange;
    [SerializeField] private float _activationTime;

    private int _currentLaserIndex = 0;

    private int _placedLasers = 0;

    private const int _placedLasersToTrigger = 2;

    private List<Transform> _placedLasersTransforms;

    private Action _onThrowAction;

    public void Initialize(Action onThrowAction)
    {
        _currentLaserIndex = 0;
        _onThrowAction = onThrowAction;
        _placedLasersTransforms = new List<Transform>();
        _laser.SetUp(TriggerStickyLasers);
    }

    public void ThrowLaser(Vector2 direction, float force)
    {
        if (_currentLaserIndex >= _stickyLasers.Length)
        {
            _currentLaserIndex = 0;
            DestroyLaser();
            for (int i = 0; i < _stickyLasers.Length; i++)
            {
                _stickyLasers[i].Destroy();
            }
            return;
        }

        _onThrowAction();
        _stickyLasers[_currentLaserIndex].Initialize(OnPlaced, transform.position, direction * force, _parent, _activationRange, _activationTime);
        _currentLaserIndex++;
    }

    private void OnPlaced(StickyLaser laser)
    {
        _placedLasersTransforms.Add(laser.transform);
        _placedLasers++;
        if (_placedLasers >= _placedLasersToTrigger)
        {
            CreateLaser();
        }
    }

    private void CreateLaser()
    {
        _laser.CreateLine(_placedLasersTransforms);
    }

    private void DestroyLaser() 
    {
        _placedLasersTransforms.Clear();
        _placedLasers = 0;
        _laser.RemoveLine();
    }

    private void TriggerStickyLasers()
    {
        _currentLaserIndex = 0;
        for (int i = 0; i < _stickyLasers.Length; i++)
        {
            _stickyLasers[i].Trigger();
        }
        DestroyLaser();
    }
}
