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
            task.text = "Зайдите в компютер и посмотрите заказы на почте";
        }
    }

    public void AcceptTask()
    {
        if (day == 0)
        {
            if (task.text.Equals("Зайдите в компютер и посмотрите заказы на почте"))
            {
                subtitle.text = "О, папа что то прислал!";
                task.text = "";
                source.PlayOneShot(guideAudioClips[5], source.volume);
            }
            else if (subtitle.text.Equals("О, папа что то прислал!")) 
            {
                StartCoroutine(CoroutineAcceptTask());
            }
        }
    }

    IEnumerator CoroutineAcceptTask()
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

    public void OnBuyAllComponents(CardInfoObject cardInfo)
    {
        if (cardInfo.name == "CORE I3 12100F" || cardInfo.name == "LAMMAX 200T" || cardInfo.name == "GTX 1050 ti")
        {
            componentCounter++;
            if (componentCounter == 3)
            {
                task.text = "Распакуй запчасти и поставь все на свой рабочий стол";
                subtitle.text = "Теперь нужно всё распаковать и поставить корпус на рабочий стол";
                source.PlayOneShot(guideAudioClips[8], source.volume);
            }
        }
    }
}
