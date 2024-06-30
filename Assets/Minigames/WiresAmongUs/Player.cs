using UnityEngine;

public class Player : BaseMinigame
{
    public Camera cam;
    private Wire _interactObject;
    private int _interactCount = 0;
    [SerializeField] LayerMask layerMask;

    public GameObject wires;

    public override void StartMinigame()
    {
        base.StartMinigame();
        wires.SetActive(true);
    }
    public override void EndMinigame()
    {
        base.EndMinigame();
        wires.SetActive(false);
    }

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && _interactObject == null)
        {
            SetDrag(true);
        }
        if(Input.GetMouseButtonUp(0) && _interactObject != null) 
        {
            SetDrag(false);
        }
        if(_interactObject != null)
        {
            if(!_interactObject._isConnected) 
            {
                UpdatePositionInteract();
                SphereTraceForConnection();
            }
            else
            {
                SetDrag(false);
            }
            
        }
        
    }

    void SetDrag(bool isDrag)
    {
        if (isDrag)
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.GetComponent<Wire>() != null)
                {
                    _interactObject = hit.collider.gameObject.GetComponent<Wire>();
                    _interactObject.SetDrag(true);
                }
            }
        }
        else
        {
            _interactObject.SetDrag(false);
            if (!_interactObject._isConnected)
            {
                _interactObject.GetLine().SetPosition(1, _interactObject.GetStartPos());
            }
            _interactObject = null;
        }
    }
    void UpdatePositionInteract()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool IsCasting = Physics.Raycast(ray, out hit);
        Vector3 TargetPos = new Vector3();
        float Distance = Vector3.Distance(transform.position, _interactObject.transform.position);
        if (IsCasting)
        {
            TargetPos.x = hit.point.x;
            TargetPos.y = hit.point.y;
            TargetPos.z = _interactObject.transform.position.z;
        }
        else
        {
            TargetPos.x = ray.GetPoint(Distance).x;
            TargetPos.y = ray.GetPoint(Distance).y;
            TargetPos.z = _interactObject.transform.position.z;
        }
        _interactObject.SetNewPosLinePoint(TargetPos);
    }

    void SphereTraceForConnection()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        bool blocking = Physics.Raycast(ray, out hit, 1000000f, layerMask); //Physics.SphereCast(ray.origin, radiusForConnected, ray.direction, out hit, 1000000.0f, layerMask); 
        if (blocking)
        {
            if (!_interactObject._isConnected)
            {
                Connected connected = hit.collider.gameObject.GetComponent<Connected>();
                if (connected != null && _interactObject.indexInteraction == connected.index)
                {
                    _interactObject.SetIsConnected(true, connected.transform.position + connected.offsetPositionForConnected);
                }
            }
        }
    }

    public void IncrementWire()
    {
        _interactCount++;
        if(_interactCount == 5)
        {
            EndMinigame();
        }
    }
}
