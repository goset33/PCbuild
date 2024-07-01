using UnityEditor;
using UnityEngine;

public class BuildPCCamera : MonoBehaviour
{
    private GameObject _interactObject;
    private Camera _cam;
    [SerializeField] private LayerMask _connectedMask;
    void Start()
    {
        _cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool blocking = Physics.Raycast(ray, out hit);
            if (blocking)
            {
                if (hit.collider.TryGetComponent(out BuildComponentPC pcComponent))
                {
                    if (!pcComponent.IsConnected())
                    {
                        _interactObject = hit.collider.gameObject;
                        pcComponent.Dragged();
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_interactObject)
            {
                _interactObject.GetComponent<BuildComponentPC>().UnDragged();
            }
            _interactObject = null;
        }


        if (_interactObject != null)
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            float Distance = Vector3.Distance(transform.position, _interactObject.transform.position);
            Vector3 TargetPos;
            TargetPos.x = ray.GetPoint(Distance / 2).x;
            TargetPos.y = ray.GetPoint(Distance / 2).y;
            TargetPos.z = _interactObject.transform.position.z;
            _interactObject.transform.position = TargetPos;
            RaycastHit hit;
            bool blocking = Physics.Raycast(ray, out hit, 1000.0f, _connectedMask);
            
            if (blocking)
            {
                if (hit.collider.TryGetComponent(out BuildConnected buildConnect))
                {
                    
                    _interactObject.TryGetComponent(out BuildComponentPC pcComponent);
                    
                    if(buildConnect.indexConnect == pcComponent.indexConnect && !pcComponent.IsConnected())
                    {
                        _interactObject.transform.SetParent(buildConnect.transform);
                        _interactObject.transform.localPosition = Vector3.zero;
                        _interactObject.transform.localRotation = pcComponent._startLocalRot;
                        pcComponent.Connected();
                        pcComponent.SetConnected(true);
                        _interactObject = null;
                        buildConnect.onConnect.Invoke();
                        Destroy(buildConnect);
                        Destroy(pcComponent);
                        _interactObject = null;
                    }
                }
            }
        }
    }
}
