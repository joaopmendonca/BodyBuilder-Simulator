using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public ConsumableItem consumableItem;

    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    public TextMeshProUGUI itemCount;
    public Image icon;
    public int recoveryValue;

    public GameObject itemDescriptionLabel;

    // Start is called before the first frame update
    void Start()
    {
        itemName.text = consumableItem.name;
        itemDescription.text = consumableItem.description;
        icon.sprite = consumableItem.image;
        recoveryValue = consumableItem.recoveryValue;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void showItemDescription()
    {
        itemDescriptionLabel.SetActive(true);
    }

    public void hideItemDescription()
    {
        itemDescriptionLabel.SetActive(false);
    }

    public void UseItem()
    {
        // Verifica se o jogador tem pelo menos um item desse tipo no inventário
        if (RefrigeratorInventory.Instance.items.ContainsKey(consumableItem))
        {
            // Remove um item desse tipo do inventário
            RefrigeratorInventory.Instance.RemoveItem(consumableItem);

            // Executa a ação do item

        }
        else
        {
            Debug.Log("You don't have any of this item in your inventory!");
        }
    }
}