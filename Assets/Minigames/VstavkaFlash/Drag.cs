using Unity.VisualScripting;
using UnityEngine;

public interface IDragged
{
    bool CanDragged();
}


public class Drag : MonoBehaviour
{

    public LayerMask connectedMask;
    private GameObject _interactObject;

    // Update is called once per frame
    private Camera cam;



    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool blocking = Physics.Raycast(ray, out hit);
            if (blocking)
            {
                if (hit.collider.TryGetComponent(out IDragged draggedObject))
                {
                    if (draggedObject.CanDragged())
                    {
                        Flash flash = hit.collider.GetComponent<Flash>();
                        if(flash.isDirtyConnected && !flash.isConnected)
                        {
                            flash.StartReturnToStartPos();
                            flash.isDirtyConnected = false;
                            flash.isConnected = false;
                        }
                        else
                            _interactObject = hit.collider.gameObject;
                    }
                }
            }
        }

        if(Input.GetMouseButtonUp(0)) 
        {
            _interactObject = null;
        }
        

        if(_interactObject != null)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            float Distance = Vector3.Distance(transform.position, _interactObject.transform.position);
            Vector3 TargetPos;
            TargetPos.x = ray.GetPoint(Distance / 2).x;
            TargetPos.y = ray.GetPoint(Distance / 2).y;
            TargetPos.z = _interactObject.transform.position.z;
            _interactObject.transform.position = TargetPos;

            RaycastHit hit;
            bool blocking = Physics.Raycast(ray, out hit, 1000.0f, connectedMask);
            if(blocking)
            {
                Flash flash = _interactObject.GetComponent<Flash>();
                if (flash.countChangeRotation >= flash.maxCountChangeRotation)
                {
                    _interactObject.transform.position = hit.collider.transform.GetChild(0).transform.position;
                    flash.isConnected = true;
                    flash.isDirtyConnected = false;
                    flash.canDragged = false;
                    _interactObject = null;
                }
                else
                {
                    _interactObject.transform.position = hit.collider.transform.GetChild(0).transform.position;
                    flash.isDirtyConnected = true;
                    _interactObject = null;
                }
                
            }
        }
    }
}
