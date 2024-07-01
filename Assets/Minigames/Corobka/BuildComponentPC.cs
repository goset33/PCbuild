using System;
using UnityEngine;
using UnityEngine.Events;

public interface IBuild
{
    bool IsConnected();
    void SetConnected(bool connected);

    void Dragged();
}

[Flags]
public enum PCComponentType
{
    Motherboard = 0,
    VideoCard = 1,
    Processor = 2,
    Ram = 3,
    ProcCooler = 4,
    PowerUnit = 5,
    Hard = 6
}

public class BuildComponentPC : MonoBehaviour, IBuild
{
    private bool _connected = false;

    public Vector3 positionDragged;
    public Vector3 draggedRotation;
    public int indexConnect;
    public UnityEvent onConnected;
    public AudioClip ConnectSound;
    public PCComponentType pcComponentType;

    public AudioSource audioSource;

    private Vector3 _startPos;
    private Vector3 _startRot;
    public Quaternion _startLocalRot;

    private void Start()
    {
        _startPos = transform.position;
        _startRot = transform.rotation.eulerAngles;
        _startLocalRot = transform.localRotation;

    }

    public void Dragged()
    {
       // transform.position = positionDragged;
       transform.localRotation = Quaternion.Euler(draggedRotation);
    }
    public void UnDragged()
    {
        transform.position = _startPos;
        transform.localRotation = Quaternion.Euler(_startRot);
    }
    public void Connected()
    {
        transform.localRotation = Quaternion.Euler(_startRot);
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
