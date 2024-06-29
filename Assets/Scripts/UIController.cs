using UnityEngine;
using TMPro;


public class UIController : MonoBehaviour
{
    [Header("Time")]
    public TextMeshProUGUI timeText;

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

        cash = PlayerPrefs.GetInt("Cash");
        cashText.text = cash.ToString();
        PlayerPrefs.SetInt("Cash", 20);
        OnCashValueChanged();
    }

    private void Update()
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

    public void OnCashValueChanged()
    {
        cash = PlayerPrefs.GetInt("Cash");
        cashText.text = cash.ToString();
    }
}
