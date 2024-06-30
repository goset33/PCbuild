using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class SellController : MonoBehaviour
{
    public ShopContainer container;
    public UIController uiController;

    [Space]
    public bool hasBox;

    public GameObject[] Components = new GameObject[7];

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Vector3.Distance(transform.position, Camera.main.transform.position) <= 3f)
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
                foreach (CardInfoObject cardInfo in container.CardInfo)
                {
                    foreach (GameObject component in Components)
                    {
                        if (component != null)
                        {
                            component.name = component.name.Replace("(Clone)", ""); 
                            if (cardInfo.name == component.name)
                            {
                                price += Mathf.RoundToInt(cardInfo.cost + (cardInfo.cost * 0.2f));
                                Destroy(component);
                            }
                        }
                    }
                }
                PlayerPrefs.SetInt("Cash", PlayerPrefs.GetInt("Cash") + price);
                uiController.OnCashValueChanged();
                print(PlayerPrefs.GetInt("Cash"));
            }
        }
    }
}
