using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Transform[] buttons;
    public Image DarkWindow;

    private float timer;
    private float maxTime;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= maxTime)
        {
            maxTime = Random.Range(1f, 3f);
            foreach (Transform button in buttons)
            {
                button.DORotate(new Vector3(0f, 0f, Random.Range(-7f, 7f)), maxTime);
            }
            timer = 0f;
        }
    }

    public void OnButtonPressed(int index)
    {
        if (!PlayerPrefs.HasKey("Cash") && index == 0)
        {
            PlayerPrefs.SetInt("Cash", 90000);
            PlayerPrefs.SetInt("Day", 0);
        }
        StartCoroutine(buttonPressed(index));
    }

    IEnumerator buttonPressed(int numButton)
    {
        if (numButton != 3)
        {
            DarkWindow.gameObject.SetActive(true);
            DarkWindow.DOColor(new Color(0f, 0f, 0f, 255f), 1000f);
            yield return new WaitForSeconds(2.5f);
        }

        if (numButton == 0)
        {
            SceneManager.LoadScene("MainScene");
        }
        else if (numButton == 1)
        {
            Application.Quit();
        }
        else if (numButton == 2)
        {
            SceneManager.LoadScene("Credits");
        }
        else if (numButton == 3)
        {
            transform.GetChild(1).GetComponent<TextMeshProUGUI>().DOFade(0f, 1f);
            transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().DOFade(0f, 1f);
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("MainMenu");
        }
    }
}
