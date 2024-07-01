using System.Collections.Generic;
using UnityEngine;

public class SellController : MonoBehaviour
{
    public ShopContainer container;
    public UIController uiController;
    public MailController mailController;
    public BuildController buildController;

    [Space]
    public AudioSource voice;
    public bool hasBox;

    public BuildPC PC;
    public string[] requaredComponents = new string[7];

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
        if (Input.GetKeyDown(KeyCode.R) && PC != null)
        {
            //Vector3.Distance(transform.position, Camera.main.transform.position) <= 3f
            //Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale, Quaternion.identity);
            //foreach (Collider collider in colliders)
            //{
            //    if (collider.CompareTag("Box"))
            //    {
            //        hasBox = true;
            //    }
            //    if (collider.CompareTag("Core"))
            //    {
            //        Components[0] = collider.gameObject;
            //    }
            //    if (collider.CompareTag("Videocard"))
            //    {
            //        Components[1] = collider.gameObject;
            //    }
            //    if (collider.CompareTag("MotherBoard"))
            //    {
            //        Components[2] = collider.gameObject;
            //    }
            //    if (collider.CompareTag("HardDrive"))
            //    {
            //        Components[3] = collider.gameObject;
            //    }
            //    if (collider.CompareTag("CompCase"))
            //    {
            //        Components[4] = collider.gameObject;
            //    }
            //    if (collider.CompareTag("RAM"))
            //    {
            //        Components[5] = collider.gameObject;
            //    }
            //    if (collider.CompareTag("Cooler"))
            //    {
            //        Components[6] = collider.gameObject;
            //    }
            //    if (collider.CompareTag("Power"))
            //    {
            //        Components[7] = collider.gameObject;
            //    }
            //}

                int price = 0;
                bool isRight = true;
            List<PCComponets> componentss = PC.componets;
                foreach (CardInfoObject cardInfo in container.CardInfo)
                {
                    foreach (PCComponets component in componentss)
                    {
                        foreach(string required in requaredComponents)
                        {
                            if (component.spawnObject != null)
                            {
                                if (required != null)
                                {
                                    print(1);
                                    //component.spawnObject.name = component.spawnObject.name.Replace("(Clone)", "");
                                    if (cardInfo.name == component.spawnObject.name && cardInfo.name == required && isRight)
                                    {
                                        print("pricing");
                                        price += Mathf.RoundToInt(cardInfo.cost + (cardInfo.cost * 0.2f));
                                    }
                                    else
                                    {
                                        
                                        isRight = false;
                                    }
                                }
                                else
                                {
                                    print(component.spawnObject.name + "  " + cardInfo.name + "  " + required);
                                    price += Mathf.RoundToInt(cardInfo.cost + (cardInfo.cost * 0.2f));
                                }
                            }
                            else
                            {
                                print(component.spawnObject.name + "  " + cardInfo.name + "  " + required);
                                isRight = false;
                            }
                        }
                        
                    }
                

                  
            }
            if (isRight)
            {
                PlayerPrefs.SetInt("Cash", PlayerPrefs.GetInt("Cash") + price);
                uiController.OnCashValueChanged();
                print(PlayerPrefs.GetInt("Cash"));
                Destroy(PC.gameObject);
                voice.PlayOneShot(good[Random.Range(0, good.Length)]);
            }
            else
            {
                PlayerPrefs.SetInt("Cash", PlayerPrefs.GetInt("Cash") + price);
                uiController.OnCashValueChanged();
                Destroy(PC.gameObject);
                voice.PlayOneShot(bad[Random.Range(0, bad.Length)]);
            }
            buildController.GetComponent<Collider>().enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
        
        if (other.CompareTag("PC"))
        {
            BuildPC pc = other.GetComponent<BuildPC>();
            PC = pc;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PC"))
        {
            PC = null;
        }
    }
}
