using UnityEngine;

public class SellController : MonoBehaviour
{
    public ShopContainer container;
    public UIController uiController;
    public MailController mailController;

    [Space]
    public AudioSource voice;
    public bool hasBox;

    public GameObject[] Components = new GameObject[7];
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
        if (Input.GetKeyDown(KeyCode.R) && Vector3.Distance(transform.position, Camera.main.transform.position) <= 3f)
        {
            print(1);
            Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale, Quaternion.identity);
            foreach (Collider collider in colliders)
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
                if (collider.CompareTag("CompCase"))
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
            }
            if (hasBox)
            {
                int price = 0;
                bool isRight = true;
                foreach (CardInfoObject cardInfo in container.CardInfo)
                {
                    foreach (GameObject component in Components)
                    {
                        foreach(string required in requaredComponents)
                        {
                            if (component != null)
                            {
                                if (required != null)
                                {
                                    component.name = component.name.Replace("(Clone)", "");
                                    if (cardInfo.name == component.name && cardInfo.name == required && isRight)
                                    {
                                        price += Mathf.RoundToInt(cardInfo.cost + (cardInfo.cost * 0.2f));
                                    }
                                    else
                                    {
                                        isRight = false;
                                    }
                                }
                                else
                                {
                                    price += Mathf.RoundToInt(cardInfo.cost + (cardInfo.cost * 0.2f));
                                }
                            }
                            else
                            {
                                isRight = false;
                            }
                        }
                        Destroy(component);
                    }
                }

                if (isRight)
                {
                    PlayerPrefs.SetInt("Cash", PlayerPrefs.GetInt("Cash") + price);
                    uiController.OnCashValueChanged();
                    print(PlayerPrefs.GetInt("Cash"));

                    voice.PlayOneShot(good[Random.Range(0, good.Length)]);
                }
                else
                {
                    voice.PlayOneShot(bad[Random.Range(0, good.Length)]);
                }   
            }
        }
    }
}
