using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Processor : MonoBehaviour
{
    public bool isProcessorGame = true;
    private bool isCanMove = true;
    public Transform[] points;
    public Camera cam;
    public float speed;
    public float range = 1.0f;
    private Transform _currentPoint;
    private Coroutine _coroutine;
    [SerializeField] private int _maxErrors = 4;
    [SerializeField] private int _currentErrors = 0;
    [SerializeField] private Transform _posForWin;

    public UnityEvent OnError;
    public UnityEvent OnSuccess;

    private void Start()
    {
        _currentPoint = points[0];
        _coroutine = null;
    }
    private void Update()
    {
        if (isProcessorGame)
        {
            if (isCanMove)
            {
                transform.position = Vector3.MoveTowards(transform.position, _currentPoint.position, Time.deltaTime * speed);
                if (transform.position == _currentPoint.position)
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
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {

                    if (hit.collider.gameObject == gameObject)
                    {

                        if (IsInRange(transform.position, _posForWin.position, range) && _currentErrors < _maxErrors)
                        {
                            _coroutine = StartCoroutine(WinCoroutine());
                        }
                        else
                        {
                            _currentErrors++;
                            if(_currentErrors >= _maxErrors)
                            {
                                OnError.Invoke();
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
        while(!(transform.position == _posForWin.position))
        {
            transform.position =  Vector3.MoveTowards(transform.position, _posForWin.position, Time.deltaTime * speed);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        OnSuccess.Invoke();
        yield return null;
    }

}
