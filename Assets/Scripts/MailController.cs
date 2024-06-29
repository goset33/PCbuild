using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MailController : MonoBehaviour
{
    public TextMeshProUGUI mailText;
    public UnityEvent taskAccepted;

    public string[] videocardsNames, processorNames, hardDiskNames, coolerNames, operativeNames, plateNames;

    [Space]
    [TextArea(3, 10)]
    public string[] textVariations;

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
                choosedVariation.Replace("=", videocardsNames[Random.Range(0, videocardsNames.Length)]);
            }
            if (choosedVariation.Contains("+"))
            {
                choosedVariation.Replace("+", processorNames[Random.Range(0, processorNames.Length)]);
            }
            if (choosedVariation.Contains("-"))
            {
                choosedVariation.Replace("-", hardDiskNames[Random.Range(0, hardDiskNames.Length)]);
            }
            if (choosedVariation.Contains("$"))
            {
                choosedVariation.Replace("$", coolerNames[Random.Range(0, coolerNames.Length)]);
            }
            if (choosedVariation.Contains("/"))
            {
                choosedVariation.Replace("/", operativeNames[Random.Range(0, operativeNames.Length)]);
            }
            if (choosedVariation.Contains("|"))
            {
                choosedVariation.Replace("|", plateNames[Random.Range(0, plateNames.Length)]);
            }
        }
        
        mailText.text = choosedVariation;
    }

    public void Accept()
    {
        taskAccepted?.Invoke();
    }

    public void Cancel()
    {
        StartCoroutine(CancelCorutine());
    }

    IEnumerator CancelCorutine()
    {
        mailText.DOFade(0f, 1f);
        yield return new WaitForSeconds(1f);
        CreateTask();
        mailText.DOFade(1f, 1f);
    }
}
