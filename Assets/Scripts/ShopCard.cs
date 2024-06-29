using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCard : MonoBehaviour
{
    public CardInfoObject cardInfo;
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
        }
    }

    public void Buy()
    {
        container.Buy(cardInfo, gameObject);
    }
}
