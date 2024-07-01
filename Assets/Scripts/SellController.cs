using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SellController : MonoBehaviour
{
    public UnityEvent OnSell;

    public ShopContainer container;
    public UIController uiController;
    public MailController mailController;
    public BuildController buildController;

    [Space]
    public AudioSource voice;
    public bool hasBox;

    public BuildPC PC;
    public string[] requaredComponents = new string[8];
    public GameObject[] Components = new GameObject[8];

    public AudioClip[] good, bad;

    private void Awake()
    {
        mailController.taskAccepted.AddListener(OnTaskAccepted);
    }

    public void OnTaskAccepted(string[] acceptComponents)
    {
        requaredComponents = acceptComponents;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && hasBox)
        {
            int price = 0;
            bool isRight = true;
            for (int i = 0; i < requaredComponents.Length; i++)
            {
                if (requaredComponents[i] != null)
                {
                    if (Components[i].name.Equals(requaredComponents[i]) && isRight)
                    {
                        print("pricing");
                        CardInfoObject rightCard = null;
                        foreach (CardInfoObject cardInfo in container.CardInfo)
                        {
                            if (cardInfo.name.Equals(requaredComponents[i]))
                            {
                                rightCard = cardInfo;
                                break;
                            }
                        }
                        price += Mathf.RoundToInt(rightCard.cost + (rightCard.cost * 0.2f));
                        continue;
                    }
                    else
                    {
                        isRight = false;
                    }
                }
                else
                {
                    CardInfoObject rightCard = null;
                    foreach (CardInfoObject cardInfo in container.CardInfo)
                    {
                        if (cardInfo.name.Equals(Components[i].name))
                        {
                            rightCard = cardInfo;
                            break;
                        }
                    }
                    print(Components[i].name + "  " + rightCard.name + "  " + requaredComponents[i]);
                    price += Mathf.RoundToInt(rightCard.cost + (rightCard.cost * 0.2f));
                    continue;
                }
            }

            if (isRight)
            {
                PlayerPrefs.SetInt("Cash", price);
                uiController.OnCashValueChanged();
                Destroy(PC.gameObject);
                voice.PlayOneShot(good[Random.Range(0, good.Length)]);
                OnSell.Invoke();
            }
            else
            {
                Destroy(PC.gameObject);
                voice.PlayOneShot(bad[Random.Range(0, bad.Length)]);
            }
            buildController.GetComponent<Collider>().enabled = true;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Box"))
        {
            hasBox = true;
        }
        if (collider.CompareTag("Core"))
        {
            Components[0] = collider.gameObject;
        }
        if (collider.CompareTag("Videocard"))
        {
            Components[1] = collider.gameObject;
        }
        if (collider.CompareTag("MotherBoard"))
        {
            Components[2] = collider.gameObject;
        }
        if (collider.CompareTag("HardDrive"))
        {
            Components[3] = collider.gameObject;
        }
        if (collider.CompareTag("ComputerCase"))
        {
            Components[4] = collider.gameObject;
        }
        if (collider.CompareTag("RAM"))
        {
            Components[5] = collider.gameObject;
        }
        if (collider.CompareTag("Cooler"))
        {
            Components[6] = collider.gameObject;
        }
        if (collider.CompareTag("Power"))
        {
            Components[7] = collider.gameObject;
        }


        print(collider.name);

        if (collider.CompareTag("PC"))
        {
            BuildPC pc = collider.GetComponent<BuildPC>();
            PC = pc;
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Box"))
        {
            hasBox = false;
        }
        if (collider.CompareTag("Core"))
        {
            Components[0] = null;
        }
        if (collider.CompareTag("Videocard"))
        {
            Components[1] = null;
        }
        if (collider.CompareTag("MotherBoard"))
        {
            Components[2] = null;
        }
        if (collider.CompareTag("HardDrive"))
        {
            Components[3] = null;
        }
        if (collider.CompareTag("ComputerCase"))
        {
            Components[4] = null;
        }
        if (collider.CompareTag("RAM"))
        {
            Components[5] = null;
        }
        if (collider.CompareTag("Cooler"))
        {
            Components[6] = null;
        }
        if (collider.CompareTag("Power"))
        {
            Components[7] = null;
        }

        if (collider.CompareTag("PC"))
        {
            PC = null;
        }
    }
}
