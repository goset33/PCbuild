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
        Dark.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "День " + day;
        Dark.transform.GetChild(0).GetComponent<TextMeshProUGUI>().DOColor(new Color(255f, 255f, 255f, 255f), 500f);

        yield return new WaitForSeconds(1.5f);
        Dark.DOColor(new Color(0f, 0f, 0f, 0f), 2f);
        Dark.transform.GetChild(0).GetComponent<TextMeshProUGUI>().DOColor(new Color(255f, 255f, 255f, 0f), 2f);
        yield return new WaitForSeconds(2f);
        Dark.gameObject.SetActive(false);
        

        if (day == 0)
        {
            subtitle.text = "Спустя столько времени я смог! Я открыл своё дело!";
            source.PlayOneShot(guideAudioClips[0], source.volume);
            yield return new WaitForSeconds(4.7f);
            subtitle.text = "Теперь я могу заниматься своим любимым делом!";
            source.PlayOneShot(guideAudioClips[1], source.volume);
            yield return new WaitForSeconds(3f);
            subtitle.text = "Собирать компютеры на заказ!";
            source.PlayOneShot(guideAudioClips[2], source.volume);
            yield return new WaitForSeconds(2.5f);
            subtitle.text = "Чтож, пора приступать к работе";
            source.PlayOneShot(guideAudioClips[3], source.volume);
            yield return new WaitForSeconds(2.5f);
            subtitle.text = "Так, нужно по почте посмотреть";
            source.PlayOneShot(guideAudioClips[4], source.volume);
            yield return new WaitForSeconds(2.3f);
            subtitle.text = "";
            task.text = "Зайдите в компютер на Q и посмотрите заказы на почте";
        }
    }

    public void isMailOpen()
    {
        if (day == 0)
        {
            if (task.text == "Зайдите в компютер на Q и посмотрите заказы на почте")
            {
                subtitle.text = "О, папа что то прислал!";
                task.text = "";
                source.PlayOneShot(guideAudioClips[5], source.volume);
            }
        }
    }

    public void AcceptTask(string[] i)
    {
        if (day == 0)
        {
            if (subtitle.text == "О, папа что то прислал!")
            {
                StartCoroutine(CoroutineAcceptTask());
            }
        }
    }

    IEnumerator CoroutineAcceptTask()
    {
        if (day == 0)
        {
            subtitle.text = "Ух! Ну держитесь! Я сейчас вам такой комп соберу!";
            source.PlayOneShot(guideAudioClips[6], source.volume);
            yield return new WaitForSeconds(4.7f);
            subtitle.text = "Так, для начала мне нужно заказать все детали из списка";
            source.PlayOneShot(guideAudioClips[7], source.volume);
            yield return new WaitForSeconds(3.3f);
            subtitle.text = "";
            task.text = "Закажите все компоненты";
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
                    task.text = "Распакуй запчасти на E и поставь всё на свой рабочий стол";
                    subtitle.text = "Теперь нужно всё распаковать и поставить корпус на рабочий стол";
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
            subtitle.text = "Теперь самое простое! Собрать его...";
            source.PlayOneShot(guideAudioClips[9], source.volume);
            StartCoroutine(CoroutineOnStartBuild());
        }
    }

    IEnumerator CoroutineOnStartBuild()
    {
        if (day == 0)
        {
            yield return new WaitForSeconds(4.3f);
            task.text = "Вставьте материнскую плату";
            subtitle.text = "Для начала нужно вставить материнку";
            source.PlayOneShot(guideAudioClips[10], source.volume);
        }
    }

    public void OnPlateOnPlace()
    {
        if (day == 0)
        {
            task.text = "Поставьте процессор, нанесите термопасту и поставьте кулер";
            subtitle.text = "Так, теперь нужно вставить процессор и смазать его пастой, и потом поставить кулер";
            source.PlayOneShot(guideAudioClips[11], source.volume);
        }
    }

    public void OnCoolerOnPlace()
    {
        if (day == 0)
        {
            task.text = "Подключите блок питания, видеокарту, жесткий диск, оперативную память";
            subtitle.text = "Теперь нужно подключить память, видюху и блок";
            source.PlayOneShot(guideAudioClips[12], source.volume);
        }
    }

    public void OnTimeForPack()
    {
        if (day == 0)
        {
            task.text = "Запакуйте компьютер";
            subtitle.text = "Отлично, нужно его упаковать и можно отправлять";
            source.PlayOneShot(guideAudioClips[14], source.volume);
        }
    }

    public void OnCompleteBuild()
    {
        if (day == 0)
        {
            task.text = "Отправьте компьютер, положив его рядом с дверью и нажав R";
            subtitle.text = "Надеюсь тебе понравится мой подарок, мамуля";
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
        subtitle.text = "Теперь я готов к полноценным заказам!";
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
