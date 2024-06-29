using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Pack : MonoBehaviour
{
    [SerializeField] private Vector3 _targetRot;
    [SerializeField] private float _speed;
    [SerializeField] private GameObject gameObject;

    public UnityEvent OnComplete;
    private int _currentCount;
    public void NextRot()
    {
        _currentCount++;
        if(_currentCount == 2) 
        {
            StartCoroutine(Rotate());
        }
        else if( _currentCount == 4) 
        { 
            OnComplete.Invoke();
        }
    }

    private IEnumerator Rotate()
    {
        while (gameObject.transform.rotation != Quaternion.Euler(_targetRot))
        {
            gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, Quaternion.Euler(_targetRot), Time.deltaTime * _speed);
            
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return null;
    }
}
