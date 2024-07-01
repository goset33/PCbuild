using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DayController : MonoBehaviour
{
    public Image Dark;
    public TextMeshProUGUI subtitle;
    public TextMeshProUGUI task;

    public BuildController buildController;
    public BuildPC buildPC;

    [Space]
    public AudioSource source;
    public AudioClip[] guideAudioClips;

    private int day;

    public int componentCounter;

    private void Start()
    {
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
            task.text = "������� � �������� �� Q � ���������� ������ �� �����";
        }
    }

    public void isMailOpen()
    {
        if (day == 0)
        {
            if (task.text == "������� � �������� �� Q � ���������� ������ �� �����")
            {
                subtitle.text = "�, ���� ��� �� �������!";
                task.text = "";
                source.PlayOneShot(guideAudioClips[5], source.volume);
            }
        }
    }

    public void AcceptTask(string[] i)
    {
        if (day == 0)
        {
            if (subtitle.text == "�, ���� ��� �� �������!")
            {
                StartCoroutine(CoroutineAcceptTask());
            }
        }
    }

    IEnumerator CoroutineAcceptTask()
    {
        if (day == 0)
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
    }

    public void OnBuyAllComponents(CardInfoObject cardInfo)
    {
        if (day == 0)
        {
            if (cardInfo.name == "CORE I3 12100F" || cardInfo.name == "LAMMAX 200T" || cardInfo.name == "GTX 1050 ti" || cardInfo.name == "NSI AM4 DDR4" || cardInfo.name == "SuperX 8GB" || cardInfo.name == "Hard Drive 500GB" || cardInfo.name == "COMPUTER CASE" || cardInfo.name == "POWER UNIT 500W")
            {
                componentCounter++;
                if (componentCounter == 8)
                {
                    task.text = "�������� �������� �� E � ������� �� �� ���� ������� ����";
                    subtitle.text = "������ ����� �� ����������� � ��������� ������ �� ������� ����";
                    source.PlayOneShot(guideAudioClips[8], source.volume);

                    buildPC = buildController.buildPC;
                    buildPC.onStartMiniGame.AddListener(OnStartBuild);
                    buildPC.transform.GetChild(3).GetComponent<BuildConnected>().onConnect.AddListener(OnPlateOnPlace);
                    buildPC.transform.GetChild(6).GetComponent<BuildConnected>().onConnect.AddListener(OnCoolerOnPlace);
                    buildPC.transform.GetChild(20).GetComponent<FlashMinigame>().onEndMiniGame.AddListener(OnTimeForPack);
                    buildPC.transform.GetChild(21).GetComponent<Pack>().onEndMiniGame.AddListener(OnCompleteBuild);
                }
            }
        }
    }

    public void OnStartBuild()
    {
        if (day == 0)
        {
            task.text = "";
            subtitle.text = "������ ����� �������! ������� ���...";
            source.PlayOneShot(guideAudioClips[9], source.volume);
            StartCoroutine(CoroutineOnStartBuild());
        }
    }

    IEnumerator CoroutineOnStartBuild()
    {
        if (day == 0)
        {
            yield return new WaitForSeconds(4.3f);
            task.text = "�������� ����������� �����";
            subtitle.text = "��� ������ ����� �������� ���������";
            source.PlayOneShot(guideAudioClips[10], source.volume);
        }
    }

    public void OnPlateOnPlace()
    {
        if (day == 0)
        {
            task.text = "��������� ���������, �������� ���������� � ��������� �����";
            subtitle.text = "���, ������ ����� �������� ��������� � ������� ��� ������, � ����� ��������� �����";
            source.PlayOneShot(guideAudioClips[11], source.volume);
        }
    }

    public void OnCoolerOnPlace()
    {
        if (day == 0)
        {
            task.text = "���������� ���� �������, ����������, ������� ����, ����������� ������";
            subtitle.text = "������ ����� ���������� ������, ������ � ����";
            source.PlayOneShot(guideAudioClips[12], source.volume);
        }
    }

    public void OnTimeForPack()
    {
        if (day == 0)
        {
            task.text = "��������� ���������";
            subtitle.text = "�������, ����� ��� ��������� � ����� ����������";
            source.PlayOneShot(guideAudioClips[14], source.volume);
        }
    }

    public void OnCompleteBuild()
    {
        if (day == 0)
        {
            task.text = "��������� ���������, ������� ��� ����� � ������ � ����� R";
            subtitle.text = "������� ���� ���������� ��� �������, ������";
            source.PlayOneShot(guideAudioClips[15], source.volume);
        }
    }

    public void OnFinallySell()
    {
        if (day == 0)
        {
            StartCoroutine(FinalCoroutine());
        }
    }

    IEnumerator FinalCoroutine()
    {
        task.text = "";
        subtitle.text = "������ � ����� � ����������� �������!";
        source.PlayOneShot(guideAudioClips[16], source.volume);
        Dark.transform.GetChild(0).gameObject.SetActive(false);
        Dark.gameObject.SetActive(true);
        Dark.DOFade(1f, 1.5f);
        yield return new WaitForSeconds(2.5f);
        PlayerPrefs.SetInt("Day", 1);
        PlayerPrefs.SetInt("Cash", 120000);
        SceneManager.LoadScene("MainScene");
    }
}
