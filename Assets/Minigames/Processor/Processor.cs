using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Processor : BaseMinigame
{
    private bool isCanMove = true;
    public Transform[] points; 
    public float speed;
    public float range = 1.0f;
    private Transform _currentPoint;
    [SerializeField] private int _maxErrors = 4;
    [SerializeField] private int _currentErrors = 0;
    [SerializeField] private Transform _posForWin;
    private GameObject movedGameObject;
    private bool _isStart = false;

    public UnityEvent OnError;
    public UnityEvent OnSuccess;


    public override void StartMinigame(GameObject gameObject)
    {
        base.StartMinigame(gameObject);
        movedGameObject = gameObject;
        _isStart = true;
    }


    private void Start()
    {
        _currentPoint = points[0];
        OnSuccess.AddListener(EndMinigame);
    }
    private void Update()
    {
        if (_started)
        {
            if (isCanMove)
            {
                movedGameObject.transform.position = Vector3.MoveTowards(movedGameObject.transform.position, _currentPoint.position, Time.deltaTime * speed);
                if (movedGameObject.transform.position == _currentPoint.position)
                {
                    if (_currentPoint == points[0])
                    {
                        _currentPoint = points[1];
                    }
                    else
                    {
                        _currentPoint = points[0];
                    }
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = newCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {

                    if (hit.collider.gameObject == movedGameObject)
                    {
                        if (IsInRange(movedGameObject.transform.position, _posForWin.position, range) && _currentErrors < _maxErrors)
                        {
                            StartCoroutine(WinCoroutine());
                        }
                        else
                        {
                            _currentErrors++;
                            if (_currentErrors >= _maxErrors)
                            {
                                
                                Destroy(gameObject);
                            }
                            ;
                        }
                    }

                }


            }
        }
        
    }

    public bool IsInRange(Vector3 a, Vector3 b, float range)
    {
        bool isXInRange = Mathf.Abs(b.x - a.x) < range;
        bool isYInRange = Mathf.Abs(b.y - a.y) < range;
        //bool isZInRange = Mathf.Abs(b.z - a.z) < range;
        return isXInRange && isYInRange;  //&&  isZInRange;
    }

    private IEnumerator WinCoroutine()
    {
        isCanMove = false;
        while(!(movedGameObject.transform.position == _posForWin.position))
        {
            movedGameObject.transform.position =  Vector3.MoveTowards(movedGameObject.transform.position, _posForWin.position, Time.deltaTime * speed);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        OnSuccess.Invoke();
        yield return null;
    }

}
