using System.Collections;
using UnityEngine;

public class MiniGamesManager : MonoBehaviour
{
    private bool _canStartMiniGame = true;
    public ProgressInstallWindows installWindows;

    public void StartInstallWindows()
    {
        if (installWindows != null && _canStartMiniGame)
        {
            installWindows.StartGame();
            _canStartMiniGame = false;
            installWindows.OnCompleted.AddListener(OnCompleteInstallWindows);
        }
    }
    private void OnCompleteInstallWindows()
    {
        _canStartMiniGame = true;
        installWindows.OnCompleted.RemoveListener(OnCompleteInstallWindows);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        StartCoroutine(TestStart());
    }

    private IEnumerator TestStart()
    {
        yield return new WaitForSeconds(2);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        yield return null;
    }
}

