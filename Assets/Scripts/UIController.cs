using UnityEngine;
using TMPro;
using UnityEngine.Events;


public class UIController : MonoBehaviour
{
    [Header("Time")]
    public TextMeshProUGUI timeText;
    public UnityEvent OnDayEnded;

    [Space]
    public int hours = 8;
    public int minutes = 0;
    public float timePerHour = 1f;

    private float timer;

    [Header("Cash")]
    public TextMeshProUGUI cashText;
    public int cash;

    private void Awake()
    {
        timeText.text = hours + ":0" + minutes;

        PlayerPrefs.SetInt("Cash", 90000); // ! ÓÄÀËÈÒÜ ÍÀ ÐÅËÈÇÅ
        OnCashValueChanged();
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("Day") != 0)
        {
            timer += Time.deltaTime;
            if (timer >= timePerHour && timer < 100f)
            {
                minutes++;
                timer = 0f;
                if (minutes == 60)
                {
                    minutes = 0;
                    hours++;
                    if (hours == 21)
                    {
                        timeText.text = "21:00";
                        timer = 101f;
                        PlayerPrefs.SetInt("Day", PlayerPrefs.GetInt("Day") + 1);
                        OnDayEnded.Invoke();
                    }
                }
                if (minutes < 10)
                {
                    timeText.text = hours + ":0" + minutes; 
                }
                else
                {
                    timeText.text = hours + ":" + minutes;
                }
            }
        }
    }

    public void OnCashValueChanged()
    {
        cash = PlayerPrefs.GetInt("Cash");
        cashText.text = cash.ToString();
    }
}
