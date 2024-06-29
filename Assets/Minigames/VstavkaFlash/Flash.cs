using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Flash : MonoBehaviour, IDragged
{
    [SerializeField] private Vector3 _axisForRotation;

    [HideInInspector] private float _targetRotEuler;
    
    public int countChangeRotation = 0;
    public int maxCountChangeRotation = 3;
    [SerializeField] private float secondForReturnToStart = 1.0f;

    [HideInInspector] public bool canDragged = true;


    public UnityEvent<float> OnChangeRotation;

    [HideInInspector] public bool isDirtyConnected = false;
    [HideInInspector] public bool isConnected = false;

    private Quaternion _startRot;
    private Vector3 _startPos;
    public bool TestBool = false;

    private Vector3 _otherAxis;
    private Quaternion _startQuaternionForOtherAxis;

    private void Start()
    {
        canDragged = true;
        _startPos = transform.position;
        _startRot = transform.rotation;
        _otherAxis = _axisForRotation;
        _otherAxis -= new Vector3(1, 1, 1);
        _otherAxis *= -1;
        _startQuaternionForOtherAxis = new Quaternion(transform.rotation.x * _otherAxis.x,
            transform.rotation.y * _otherAxis.y, transform.rotation.z * _otherAxis.z, transform.rotation.w);
    }

    private void Update()
    {
    }

    public void ChangeRotation()
    {
        Vector3 axis;
        float angle = 0.0f;
        transform.rotation.ToAngleAxis(out angle, out axis);
        if(angle < 2.0f)
        {
            _targetRotEuler = 180;
            countChangeRotation++;
            OnChangeRotation.Invoke(_targetRotEuler);

        }
        else
        {
            _targetRotEuler = 0;
            countChangeRotation++;
            OnChangeRotation.Invoke(_targetRotEuler);

        }
        
    }

    private IEnumerator DisableDrag()
    {
        canDragged = false;
        yield return new WaitForSeconds(1.0f);
        canDragged = true;
    }

    public bool CanDragged()
    {
        return canDragged;
    }
    public void StartReturnToStartPos()
    {
        StartCoroutine(ReturnToStartPos(secondForReturnToStart));
    }

    private IEnumerator ReturnToStartPos(float inSeconds)
    {
        canDragged = false;
        ChangeRotation();
        float currentDuration = 0.0f;
        Quaternion targetRot = Quaternion.AngleAxis(_targetRotEuler, _axisForRotation) * _startQuaternionForOtherAxis;
        StartCoroutine(TimerMove(currentDuration, inSeconds, transform.position, targetRot, transform.rotation));
        yield return new WaitForSeconds(inSeconds);
        StopAllCoroutines();
        canDragged = true;
        yield return null;
    }

    private IEnumerator TimerMove(float currentDuration, float inSeconds, Vector3 currentPos, Quaternion targetRot, Quaternion startRot)
    {
        transform.position = Vector3.Lerp(currentPos, _startPos, currentDuration / inSeconds * 2);
        
        transform.rotation = Quaternion.Slerp(startRot, targetRot, currentDuration / inSeconds * 2);
        yield return new WaitForSeconds(Time.deltaTime);
        currentDuration += Time.deltaTime;
        StartCoroutine(TimerMove(currentDuration, inSeconds, currentPos, targetRot, startRot));
        yield return null;
    }
    
}
