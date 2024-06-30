using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCard : MonoBehaviour
{
    public CardInfoObject cardInfo;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI costText;
    public Image previewImage;

    private Button _button;
    public ShopContainer container;

    private void Start()
    {
        if(cardInfo != null)
        {
            previewImage.sprite = cardInfo.preview;
            nameText.text = cardInfo.name.ToString();
            costText.text = cardInfo.cost + "ð.";
        }
    }

    public void Buy()
    {
        container.Buy(cardInfo);
    }
}
