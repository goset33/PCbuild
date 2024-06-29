using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Camera cam;
    private Interaction _interactObject;
    [SerializeField] LayerMask layerMask;

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
                if (hit.collider.gameObject.GetComponent<Interaction>() != null)
                {
                    _interactObject = hit.collider.gameObject.GetComponent<Interaction>();
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
}
