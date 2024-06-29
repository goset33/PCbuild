using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopContainer : MonoBehaviour
{
    public List<GameObject> shopCards = new List<GameObject>();
    public GameObject shopCardPrefab;


    private void Start()
    {
        TestCards();
    }
    public void AddCard(string text, int coin, Sprite sprite)
    {
        GameObject card = Instantiate(shopCardPrefab, transform);
        shopCards.Add(card);
        ShopCard shopCard = card.GetComponent<ShopCard>();
        shopCard.icon.sprite = sprite;
        shopCard.nameText.text = text;
    }

    public void TestCards()
    {
        int rand = Random.Range(33, 66);
        for(int i = 0; i < rand; i ++)
        {
            AddCard("test", 123, null);
        }
    }

    public void RemoveAllCards()
    {
        for(int i = 0; i < transform.childCount; i ++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
