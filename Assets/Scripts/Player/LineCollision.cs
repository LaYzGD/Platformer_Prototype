using System.Collections.Generic;
using UnityEngine;

public class LineCollision : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D _polygonCollider;

    private List<Vector2> _colliderPoints = new List<Vector2>();
    private LineRenderer _lineRenderer;

    private bool _canCreateCollider;

    public void Initialize(LineRenderer lineRenderer)
    {
        _lineRenderer = lineRenderer;
    }

    public void CreateCollider()
    {
        _polygonCollider.enabled = true;
        _canCreateCollider = true;
    }

    public void RemoveCollider()
    {
        _polygonCollider.enabled = false;
        _canCreateCollider = false;
    }

    public void Update()
    {
        if (!_canCreateCollider || !_lineRenderer.enabled)
        {
            return;
        }

        _colliderPoints = CalculateColliderPoints();
        _polygonCollider.SetPath(0, _colliderPoints.ConvertAll(p => (Vector2)transform.InverseTransformPoint(p)));
    }

    private List<Vector2> CalculateColliderPoints()
    {
        Vector3[] positions = new Vector3[_lineRenderer.positionCount];
        _lineRenderer.GetPositions(positions);
        float width = _lineRenderer.startWidth;
        float m = (positions[1].y - positions[0].y) / (positions[1].x - positions[0].x);
        float deltaX = (width / 2) * (m / Mathf.Pow(m * m + 1, 0.5f));
        float deltaY = (width / 2) * (1 / Mathf.Pow(1 + m * m, 0.5f));

        Vector3[] offsets = new Vector3[_lineRenderer.positionCount];
        offsets[0] = new Vector3(-deltaX, deltaY);
        offsets[1] = new Vector3(deltaX, -deltaY);

        List<Vector2> colliderPoints = new List<Vector2> 
        {
            positions[0] + offsets[0],
            positions[1] + offsets[0],
            positions[0] + offsets[1],
            positions[1] + offsets[1]
        };

        return colliderPoints;
    }
}
