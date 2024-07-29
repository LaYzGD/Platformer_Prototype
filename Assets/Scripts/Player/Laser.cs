using System;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private LineCollision _lineCollision;
    [SerializeField] private Vector3 _offset = new Vector3 (0, 0, 5f);

    private Action _onCollide;
    private List<Transform> _points;
    private bool _canShowLaser;

    public void SetUp(Action onCollide)
    {
        _onCollide = onCollide;
    }

    public void CreateLine(List<Transform> points)
    {
        _points = points;
        _lineRenderer.enabled = true;
        _lineRenderer.positionCount = _points.Count;
        _lineCollision.Initialize(_lineRenderer);
        _canShowLaser = true;
        _lineCollision.CreateCollider();
    }

    private void Update()
    {
        if (!_canShowLaser)
        {
            return;
        }

        _lineRenderer.SetPositions(_points.ConvertAll(p => p.position - _offset).ToArray());
    }

    public void RemoveLine()
    {
        _canShowLaser = false;
        _lineRenderer.enabled = false;
        _lineCollision.RemoveCollider();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _onCollide();
    }
}
