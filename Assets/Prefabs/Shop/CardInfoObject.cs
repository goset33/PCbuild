using UnityEngine;

[CreateAssetMenu(fileName = "Card info", menuName = "Card")]
public class CardInfoObject : ScriptableObject
{
    public new string name;
    public int cost;
    public Sprite sprite;
    public GameObject gameObjectForSpawn;
    public int timeForDelivery = 5;
}
