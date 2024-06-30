using UnityEngine;
using UnityEngine.Events;

public interface IBuild
{
    bool IsConnected();
    void SetConnected(bool connected);

    void Dragged();
}

public class BuildComponentPC : MonoBehaviour, IBuild
{
    private bool _connected = false;

    public Vector3 positionDragged;
    public Vector3 draggedRotation;
    public int indexConnect;
    public UnityEvent onConnected;
    public AudioClip ConnectSound;

    public AudioSource audioSource;

    private Vector3 _startPos;
    private Quaternion _startRot;

    private void Start()
    {
        _startPos = transform.position;
        _startRot = transform.rotation;


    }

    public void Dragged()
    {
       // transform.position = positionDragged;
       transform.localRotation = Quaternion.Euler(draggedRotation);
    }
    public void UnDragged()
    {
        transform.position = _startPos;
        transform.localRotation = _startRot;
    }
    public void Connected()
    {
        transform.localRotation = _startRot;
    }

    public bool IsConnected()
    {
        return _connected;
    }

    public void SetConnected(bool connected)
    {
        _connected = connected;
        if(_connected)
        {
            onConnected.Invoke();
            audioSource.Play();
        }
    }

    
}
