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

    [TextArea(3, 10)]
    public string[] textVariations;

    public void CreateTask()
    {
        string choosedVariation = textVariations[Random.Range(0, textVariations.Length)];
        mailText.text = choosedVariation;
    }

    public void Accept()
    {

    }

    public void Cancel()
    {
        mailText.DOFade(0f, 1f);
        CreateTask();
        mailText.DOFade(1f, 1f);
    }
}
