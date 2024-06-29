using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopCardInfo : MonoBehaviour
{
    public GameObject componentPrefab;
    public int componentCost;

    private void Awake()
    {
        transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = componentPrefab.name;
        transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = componentCost + "ð.";
    }
}
