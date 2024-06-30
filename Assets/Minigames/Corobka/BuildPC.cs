using System;
using System.Collections.Generic;
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

    private GameObject _currentComponent;

    private int _currentProgress;

    public UnityEvent onMissedCasePC;
    public UnityEvent onCompleteBuild;

    public override void StartMinigame()
    {
        base.StartMinigame();
        if(casePC == null) { onMissedCasePC.Invoke(); return; }

        if(_currentComponent != null)
        {
            Destroy(_currentComponent);
        }
       ReturnCurrentProgress();
       Camera.SetupCurrent(Camera.main);
    }

    public override void EndMinigame()
    {
        base.EndMinigame();
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
            _currentComponent = Instantiate(componets[_currentProgress].spawnObject, startTransform);
            UnityAction act = stringFunctionToUnityAction(this, componets[_currentProgress].methodOnConnected);
            if(_currentComponent.TryGetComponent(out BuildComponentPC compPc) )
            {
                compPc.onConnected.AddListener(act);
            }
        }
        else
        {
            onCompleteBuild.Invoke();
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
        FlashMinigame.onEndMiniGame.AddListener(IncrementProgress);
    }

    UnityAction stringFunctionToUnityAction(object target, string functionName)
    {
        UnityAction action = (UnityAction)Delegate.CreateDelegate(typeof(UnityAction), target, functionName);
        return action;
    }
}
