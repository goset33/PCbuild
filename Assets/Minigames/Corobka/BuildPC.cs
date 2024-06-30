using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuildPC : BaseMinigame
{
    public GameObject casePC;
    public GameObject motherBoard;
    public GameObject videoCard;
    public GameObject processor;
    public GameObject processorCooler;
    public GameObject powerUnit;
    public GameObject caseCooler;
    public Transform startTransform;

    [Header("Mini game's")]
    public GameObject processorCoolerConnect;
    public GameObject processorMinigame;
    public GameObject TermopastMingame;
    public GameObject wiresMiniGame;

    private GameObject _currentComponent;

    private int _currentProgress;

    public UnityEvent onMissedCasePC;

    private void Start()
    {
        ReturnCurrentProgress();
        Camera.SetupCurrent(Camera.main);
    }

    public override void StartMinigame()
    {
        base.StartMinigame();
        if(casePC == null) { onMissedCasePC.Invoke(); return; }

        if(_currentComponent != null)
        {
            Destroy(_currentComponent);
        }
       ReturnCurrentProgress();
    }

    public override void EndMinigame()
    {
        base.EndMinigame();
    }

    public void IncrementProgress()
    {
        _currentProgress++;
        _currentComponent.GetComponent<BuildComponentPC>().onConnected.RemoveAllListeners();
        ReturnCurrentProgress();
    }
    public void ReturnCurrentProgress()
    {
        switch (_currentProgress)
        {
            case 0:
                _currentComponent = Instantiate(motherBoard, startTransform);
                _currentComponent.GetComponent<BuildComponentPC>().onConnected.AddListener(IncrementProgress);
                break;
            case 1:
                _currentComponent = Instantiate(processor, startTransform);
                _currentComponent.GetComponent<BuildComponentPC>().onConnected.AddListener(StartProcessorMiniGame);
                break;
            case 2:
                _currentComponent = Instantiate(processorCooler, startTransform);
                processorCoolerConnect.SetActive(true);
                _currentComponent.GetComponent<BuildComponentPC>().onConnected.AddListener(IncrementProgress);
                break;
            case 3:
                _currentComponent = Instantiate(powerUnit, startTransform);
                _currentComponent.GetComponent<BuildComponentPC>().onConnected.AddListener(StartWiresMiniGame);
                break;
            case 4:
                _currentComponent = Instantiate(videoCard, startTransform);
                break;
        }
        Debug.Log(_currentProgress);
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
}
