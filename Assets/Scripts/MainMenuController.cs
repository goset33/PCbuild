using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Transform startButton, exitButton;
    public Image DarkWindow;

    private float timer;
    private float maxTime;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= maxTime)
        {
            maxTime = Random.Range(1f, 3f);
            startButton.DORotate(new Vector3(0f, 0f, Random.Range(-7f, 7f)), maxTime);
            exitButton.DORotate(new Vector3(0f, 0f, Random.Range(-7f, 7f)), maxTime);
            timer = 0f;
        }
    }

    public void OnStartPressed()
    {
        if (!PlayerPrefs.HasKey("Cash"))
        {
            PlayerPrefs.SetInt("Cash", 1000);
        }
        StartCoroutine(buttonPressed(0));
    }

    public void OnExitPressed()
    {
        StartCoroutine(buttonPressed(1));
    }

    IEnumerator buttonPressed(int numButton)
    {
        DarkWindow.DOColor(new Color(0f, 0f, 0f, 255f), 1000f);
        yield return new WaitForSeconds(2.5f);
        if (numButton == 0)
        {
            SceneManager.LoadScene("MainScene");
        }
        else
        {
            Application.Quit();
        }
    }
}
