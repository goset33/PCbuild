using UnityEngine;
using UnityEngine.Events;


public class Wire : MonoBehaviour
{
    private bool _IsDraged = false;

    [HideInInspector]
    public bool _isConnected = false;

    public int indexInteraction;
    public UnityEvent onConnected;


    private Vector3 _startPos;
    private LineRenderer _lineRenderer;

    void Start()
    {
        
        _lineRenderer = GetComponent<LineRenderer>();
        _startPos = _lineRenderer.GetPosition(1);
    }

    public void SetDrag(bool isDrag)
    {
        _IsDraged = isDrag;
    }
    public Vector3 GetStartPos()
    {
        return _startPos;
    }

    public void SetNewPosLinePoint(Vector3 newPos)
    {
        if (_lineRenderer != null)
        {
            _lineRenderer.SetPosition(1, transform.InverseTransformPoint(newPos));
        }
    }

    public LineRenderer GetLine()
    {
        return _lineRenderer;
    }

    public void SetIsConnected(bool isConnected, Vector3 PosConnected)
    {
        if (_lineRenderer != null && isConnected)
        {
            _lineRenderer.SetPosition(1, transform.InverseTransformPoint(PosConnected));
            _isConnected = isConnected;
            onConnected.Invoke();
        }
    }
}
