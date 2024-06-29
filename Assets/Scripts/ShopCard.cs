using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCard : MonoBehaviour
{
    public CardInfoObject cardInfo;
    public Image icon;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI costText;

    private Button _button;
    public ShopContainer container;

    private void OnValidate()
    {
        if(cardInfo != null)
        {
            costText.text = cardInfo.cost.ToString();
            nameText.text = cardInfo.name.ToString();
            icon.sprite = cardInfo.sprite;
        }
    }

    public void Buy()
    {
        container.Buy(cardInfo, gameObject);
    }
}
