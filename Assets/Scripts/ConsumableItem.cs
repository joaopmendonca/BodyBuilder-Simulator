using UnityEngine;

public enum ConsumableItemType
{
    Food,
    Supplement,
    Medicine
}

[CreateAssetMenu(fileName = "New ConsumableItem", menuName = "Itens/ConsumableItem")]
public class ConsumableItem : ScriptableObject
{
    public string itemName;
    public AudioClip useItemSound;
    public string description;
    public Sprite image;
    public int feedRecoveryValue;
    public int suplementationRecoveryValue;
    public int healthRecoveryValue;
    public int price;
    public GameObject itemPrefab;
    public ConsumableItemType itemType;
}