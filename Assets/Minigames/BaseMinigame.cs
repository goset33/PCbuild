using UnityEngine;
using UnityEngine.Events;

public class BaseMinigame : MonoBehaviour
{
    public Camera newCamera;
    protected Camera _prevCamera;
    public UnityEvent onStartMiniGame;
    public UnityEvent onEndMiniGame;
    protected bool _started = false;

    virtual public void StartMinigame()
    {
        _prevCamera = Camera.main;
        
        _prevCamera.enabled = false;
        newCamera.enabled = true;
        Camera.SetupCurrent(newCamera);
        _started = true;
        onStartMiniGame.Invoke();
    }

    virtual public void StartMinigame(GameObject gameObject)
    {
        StartMinigame();
    }

    virtual public void EndMinigame()
    {
        Debug.Log(gameObject.name);
        Camera.current.enabled = false;
        _prevCamera.enabled=true;
        Camera.SetupCurrent(_prevCamera);
        _started = false;
        onEndMiniGame.Invoke();
    }

    public void NewCamera(Camera cam)
    {
        if(cam != null)
        {
            Camera.SetupCurrent(cam);
        }
    }
}
