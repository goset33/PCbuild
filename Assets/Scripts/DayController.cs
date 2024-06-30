using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayController : MonoBehaviour
{
    public Image Dark;
    public TextMeshProUGUI subtitle;
    public TextMeshProUGUI task;

    public GameObject monitorCanvas;
    public MailController mailController;
    public ShopContainer shopContainer;

    [Space]
    public AudioSource source;
    public AudioClip[] guideAudioClips;

    private int day;

    private int componentCounter;

    private void Start()
    {
        mailController.taskAccepted.AddListener(AcceptTask);
        monitorCanvas.transform.parent.parent.GetChild(1).GetComponent<PCController>().isInMail.AddListener(AcceptTask);
        shopContainer.OnBuy.AddListener(OnBuyAllComponents);
        StartCoroutine(StartDay());
    }

    IEnumerator StartDay()
    {
        yield return new WaitForSeconds(0.3f);
        day = PlayerPrefs.GetInt("Day");
        if (day == 0)
        {
            Dark.transform.parent.transform.GetChild(1).gameObject.SetActive(false);
        }
        Dark.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "���� " + day;
        Dark.transform.GetChild(0).GetComponent<TextMeshProUGUI>().DOColor(new Color(255f, 255f, 255f, 255f), 500f);

        yield return new WaitForSeconds(1.5f);
        Dark.DOColor(new Color(0f, 0f, 0f, 0f), 2f);
        Dark.transform.GetChild(0).GetComponent<TextMeshProUGUI>().DOColor(new Color(255f, 255f, 255f, 0f), 2f);
        yield return new WaitForSeconds(2f);
        Dark.gameObject.SetActive(false);
        

        if (day == 0)
        {
            subtitle.text = "������ ������� ������� � ����! � ������ ��� ����!";
            source.PlayOneShot(guideAudioClips[0], source.volume);
            yield return new WaitForSeconds(4.7f);
            subtitle.text = "������ � ���� ���������� ����� ������� �����!";
            source.PlayOneShot(guideAudioClips[1], source.volume);
            yield return new WaitForSeconds(3f);
            subtitle.text = "�������� ��������� �� �����!";
            source.PlayOneShot(guideAudioClips[2], source.volume);
            yield return new WaitForSeconds(2.5f);
            subtitle.text = "����, ���� ���������� � ������";
            source.PlayOneShot(guideAudioClips[3], source.volume);
            yield return new WaitForSeconds(2.5f);
            subtitle.text = "���, ����� �� ����� ����������";
            source.PlayOneShot(guideAudioClips[4], source.volume);
            yield return new WaitForSeconds(2.3f);
            subtitle.text = "";
            task.text = "������� � �������� � ���������� ������ �� �����";
        }
    }

    public void AcceptTask()
    {
        if (day == 0)
        {
            if (task.text.Equals("������� � �������� � ���������� ������ �� �����"))
            {
                subtitle.text = "�, ���� ��� �� �������!";
                task.text = "";
                source.PlayOneShot(guideAudioClips[5], source.volume);
            }
            else if (subtitle.text.Equals("�, ���� ��� �� �������!")) 
            {
                StartCoroutine(CoroutineAcceptTask());
            }
        }
    }

    IEnumerator CoroutineAcceptTask()
    {
        subtitle.text = "��! �� ���������! � ������ ��� ����� ���� ������!";
        source.PlayOneShot(guideAudioClips[6], source.volume);
        yield return new WaitForSeconds(4.7f);
        subtitle.text = "���, ��� ������ ��� ����� �������� ��� ������ �� ������";
        source.PlayOneShot(guideAudioClips[7], source.volume);
        yield return new WaitForSeconds(3.3f);
        subtitle.text = "";
        task.text = "�������� ��� ����������";
    }

    public void OnBuyAllComponents(CardInfoObject cardInfo)
    {
        if (cardInfo.name == "CORE I3 12100F" || cardInfo.name == "LAMMAX 200T" || cardInfo.name == "GTX 1050 ti")
        {
            componentCounter++;
            if (componentCounter == 3)
            {
                task.text = "�������� �������� � ������� ��� �� ���� ������� ����";
                subtitle.text = "������ ����� �� ����������� � ��������� ������ �� ������� ����";
                source.PlayOneShot(guideAudioClips[8], source.volume);
            }
        }
    }
}
