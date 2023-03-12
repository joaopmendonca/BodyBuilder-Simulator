using UnityEngine;

[CreateAssetMenu(fileName = "New ConsumableItem", menuName = "Itens/ConsumableItem")]
public class ConsumableItem : ScriptableObject
{
    public string name;
    public string description;
    public Sprite image;
    public int recoveryValue;
    public int price;
    public GameObject itemPrefab;
}