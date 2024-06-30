using System.Collections.Generic;
using UnityEngine;


public class ShopContainer : MonoBehaviour
{
    public List<GameObject> shopCards = new List<GameObject>();
    public GameObject shopCardPrefab;
    public UIController uiController;
    public CardInfoObject[] CardInfo;
    public PCController pcController;


    private void Start()
    {
        GenerateCards();
        //OnBuy.AddListener(Buy);
    }
    public void AddCard(CardInfoObject cardInfo)
    {
        GameObject card = Instantiate(shopCardPrefab, transform);
        shopCards.Add(card);
        ShopCard shopCard = card.GetComponent<ShopCard>();
        shopCard.cardInfo = cardInfo;
        shopCard.container = this;

    }

    public void GenerateCards()
    {
        foreach (CardInfoObject card in CardInfo)
        {
            AddCard(card);
        }
    }

    public void RemoveAllCards()
    {
        for(int i = 0; i < transform.childCount; i ++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
    public void Buy(CardInfoObject cardInfo, GameObject destroyObject)
    {
        
        if(PlayerPrefs.GetInt("Cash") >= cardInfo.cost)
        {
            PlayerPrefs.SetInt("Cash", PlayerPrefs.GetInt("Cash") - cardInfo.cost);
            uiController.OnCashValueChanged();
            pcController.StartCoroutine(pcController.Delivery(cardInfo.gameObjectForSpawn, cardInfo.timeForDelivery));
            Destroy(destroyObject);
        }
    }

   
}
