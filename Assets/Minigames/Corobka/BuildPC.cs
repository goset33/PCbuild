using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public struct PCComponets
{
    public GameObject spawnObject;
    public string methodOnConnected;
}
 
public class BuildPC : BaseMinigame
{
    public GameObject casePC;
    public Transform startTransform;
    public List<PCComponets> componets = new List<PCComponets>();


    [Header("Mini game's")]
    public GameObject processorCoolerConnect;
    public GameObject processorMinigame;
    public GameObject TermopastMingame;
    public GameObject wiresMiniGame;
    public BaseMinigame FlashMinigame;
    public BaseMinigame windowsInstallMinigame;
    public BaseMinigame packMiniGame;

    public AudioClip connectSound;

    private GameObject _currentComponent;

    private int _currentProgress;

    public UnityEvent onMissedCasePC;
    public UnityEvent onCompleteBuild;

    public override void StartMinigame()
    {
        _prevCamera = Camera.current;
        newCamera.enabled = true;
        Camera.main.enabled = false;
        Camera.SetupCurrent(newCamera);
        _started = true;
        onStartMiniGame.Invoke();
        // if(casePC == null) { onMissedCasePC.Invoke(); return; }
        ReturnCurrentProgress();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        onStartMiniGame.AddListener(FindObjectOfType<DayController>().OnStartBuild);
    }

    public override void EndMinigame()
    {
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Camera.main.enabled = true;
        Camera.SetupCurrent(FindObjectOfType<FirstPersonLook>().GetComponent<Camera>());
        FindObjectOfType<FirstPersonLook>().GetComponent<Camera>().enabled = true;
        gameObject.AddComponent<DragObject>();
        gameObject.AddComponent<Rigidbody>();
    }

    public void IncrementProgress()
    {
        _currentProgress++;
        ReturnCurrentProgress();
    }
    public void ReturnCurrentProgress()
    {
        if(_currentProgress <= componets.Count - 1)
        {
            _currentComponent = componets[_currentProgress].spawnObject;
            _currentComponent.SetActive(true);
            UnityAction act = stringFunctionToUnityAction(this, componets[_currentProgress].methodOnConnected);
            if(_currentComponent.TryGetComponent(out BuildComponentPC compPc) )
            {
                compPc.onConnected.AddListener(act);
                AudioSource aO = compPc.AddComponent<AudioSource>();
                compPc.audioSource = aO;
                compPc.audioSource.clip = connectSound;
            }
        }
        else
        {
            onCompleteBuild.Invoke();
            StartFlashMinigame();
        }
    }

    public void StartProcessorMiniGame()
    {
        processorMinigame.GetComponent<Processor>().StartMinigame(_currentComponent);
        processorMinigame.GetComponent<BaseMinigame>().onEndMiniGame.AddListener(StartPaintTermopastaMiniGame);
    }
    public void StartPaintTermopastaMiniGame()
    {
        TermopastMingame.GetComponent<PaintingTermopasta>().StartMinigame();
        TermopastMingame.GetComponent<BaseMinigame>().onEndMiniGame.AddListener(IncrementProgress);
    }

    public void StartWiresMiniGame()
    {
        wiresMiniGame.GetComponent<BaseMinigame>().StartMinigame();
        wiresMiniGame.GetComponent<BaseMinigame>().onEndMiniGame.AddListener(IncrementProgress);
    }

    public void StartFlashMinigame()
    {
        FlashMinigame.StartMinigame();
        FlashMinigame.onEndMiniGame.AddListener(StartPackingPC);
    }

    public void StartWindowsInstallMinigame()
    {
        windowsInstallMinigame.StartMinigame();
        windowsInstallMinigame.onEndMiniGame.AddListener(IncrementProgress);
        
    }

    public void StartPackingPC()
    {
        packMiniGame.StartMinigame();
        packMiniGame.onEndMiniGame.AddListener(EndMinigame);
    }

    UnityAction stringFunctionToUnityAction(object target, string functionName)
    {
        UnityAction action = (UnityAction)Delegate.CreateDelegate(typeof(UnityAction), target, functionName);
        return action;
    }
}
