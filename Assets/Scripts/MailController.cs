using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MailController : MonoBehaviour
{
    public SellController sellController;

    public TextMeshProUGUI mailText;
    public UnityEvent<string[]> taskAccepted;

    public string[] acceptedComponents = new string[7];
    public string[] videocardsNames, processorNames, hardDiskNames, coolerNames, operativeNames;

    [Space]
    [TextArea(3, 10)]
    public string[] textVariations;
    [TextArea(3, 10)]
    public string guideText;

    public bool isAlreadyAccepted;

    private void Awake()
    {
        sellController.OnSell.AddListener(OnSelled);

        if (PlayerPrefs.GetInt("Day") == 0)
        {
            mailText.text = guideText;
        }
        else
        {
            CreateTask();
        }
    }

    public void CreateTask()
    {
        string choosedVariation = textVariations[Random.Range(0, textVariations.Length)];
        if (choosedVariation.Contains("[название компании]"))
        {
            choosedVariation = choosedVariation.Replace("[название компании]", PlayerPrefs.GetString("Company"));
        }
        if (choosedVariation.Contains("="))
        {
            acceptedComponents[1] = videocardsNames[Random.Range(0, videocardsNames.Length)];
            choosedVariation = choosedVariation.Replace("=", acceptedComponents[1]);
        }
        else
        {
            acceptedComponents[1] = null;
        }

        if (choosedVariation.Contains("+"))
        {
            acceptedComponents[0] = processorNames[Random.Range(0, processorNames.Length)];
            choosedVariation = choosedVariation.Replace("+", acceptedComponents[0]);
        }
        else
        {
            acceptedComponents[0] = null;
        }

        if (choosedVariation.Contains("-"))
        {
            acceptedComponents[3] = hardDiskNames[Random.Range(0, hardDiskNames.Length)];
            choosedVariation = choosedVariation.Replace("-", acceptedComponents[3]);
        }
        else
        {
            acceptedComponents[3] = null;
        }

        if (choosedVariation.Contains("$"))
        {
            acceptedComponents[6] = coolerNames[Random.Range(0, coolerNames.Length)];
            choosedVariation = choosedVariation.Replace("$", acceptedComponents[6]);
        }
        else
        {
            acceptedComponents[6] = null;
        }

        if (choosedVariation.Contains("/"))
        {
            acceptedComponents[5] = operativeNames[Random.Range(0, operativeNames.Length)];
            choosedVariation = choosedVariation.Replace("/", acceptedComponents[5]);
        }
        else
        {
            acceptedComponents[5] = null;
        }

        acceptedComponents[2] = "NSI AM4 DDR4";
        acceptedComponents[4] = "COMPUTER CASE";
        acceptedComponents[7] = "POWER UNIT 500W";
        mailText.text = choosedVariation;
    }

    public void Accept()
    {
        isAlreadyAccepted = true;
        taskAccepted?.Invoke(acceptedComponents);
    }

    public void Cancel()
    {
        if (PlayerPrefs.GetInt("Day") !=  0 && !isAlreadyAccepted)
        {
            StartCoroutine(CancelCorutine());
        }
    }

    IEnumerator CancelCorutine()
    {
        mailText.DOFade(0f, 1f);
        yield return new WaitForSeconds(1f);
        CreateTask();
        mailText.DOFade(1f, 1f);
    }

    public void OnSelled()
    {
        isAlreadyAccepted = false;
    }
}
