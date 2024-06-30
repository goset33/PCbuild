using UnityEngine;
using UnityEngine.Events;

public class BuildPC : MonoBehaviour
{
    public GameObject casePC;
    public GameObject motherBoard;
    public GameObject videoCard;
    public GameObject processor;
    public GameObject processorCooler;
    public GameObject powerUnit;
    public GameObject caseCooler;
    public Camera cam;
    public Transform startTransform;

    private GameObject _currentComponent;

    private Camera _prevCam;
    private int _currentProgress;

    public UnityEvent onMissedCasePC;

    private void Start()
    {
        StartBuild();
    }

    public void StartBuild()
    {
        if(casePC == null) { onMissedCasePC.Invoke(); return; }
        _prevCam = Camera.current;
        Camera.SetupCurrent(cam);
        if(_currentComponent != null)
        {
            Destroy(_currentComponent);
        }
        if(_currentProgress == 0)
        {
            _currentComponent = Instantiate(motherBoard, startTransform);
        }
    }

    public void EndBuild()
    {
        Camera.SetupCurrent(_prevCam);
    }
}
