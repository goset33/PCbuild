using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MailController : MonoBehaviour
{
    public TextMeshProUGUI mailText;
    public UnityEvent<string[]> taskAccepted;

    public string[] acceptedComponents = new string[7];
    public string[] videocardsNames, processorNames, hardDiskNames, coolerNames, operativeNames;

    [Space]
    [TextArea(3, 10)]
    public string[] textVariations;
    [TextArea(3, 10)]
    public string guideText;

    private void Awake()
    {
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
            choosedVariation.Replace("[название компании]", PlayerPrefs.GetString("Company"));
        }
        if (choosedVariation.Contains("*"))
        {
            if (choosedVariation.Contains("="))
            {
                acceptedComponents[1] = videocardsNames[Random.Range(0, videocardsNames.Length)];
                choosedVariation.Replace("=", acceptedComponents[0]);
            }
            if (choosedVariation.Contains("+"))
            {
                acceptedComponents[0] = processorNames[Random.Range(0, processorNames.Length)];
                choosedVariation.Replace("+", acceptedComponents[1]);
            }
            if (choosedVariation.Contains("-"))
            {
                acceptedComponents[3] = hardDiskNames[Random.Range(0, hardDiskNames.Length)];
                choosedVariation.Replace("-", acceptedComponents[2]);
            }
            if (choosedVariation.Contains("$"))
            {
                acceptedComponents[6] = coolerNames[Random.Range(0, coolerNames.Length)];
                choosedVariation.Replace("$", acceptedComponents[3]);
            }
            if (choosedVariation.Contains("/"))
            {
                acceptedComponents[5] = operativeNames[Random.Range(0, operativeNames.Length)];
                choosedVariation.Replace("/", acceptedComponents[4]);
            }
            acceptedComponents[2] = "NSI AM4 DDR4";
            acceptedComponents[4] = "COMPUTER CASE";
        }
        mailText.text = choosedVariation;
    }

    public void Accept()
    {
        taskAccepted?.Invoke(acceptedComponents);
    }

    public void Cancel()
    {
        if (PlayerPrefs.GetInt("Day") !=  0)
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
}
