using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MarketItem : MonoBehaviour
{
    public ConsumableItem consumableItem;

    public TextMeshProUGUI itemName;
    public int itemIndex;
    public TextMeshProUGUI itemDescription;
    public Image icon;
    public int recoveryValue;
    public int itemPrice;
    public TextMeshProUGUI itemPriceText;
    public TextMeshProUGUI descriptionItemName;

    public GameObject itemDescriptionLabel;

    // Start is called before the first frame update
    void Start()
    {
        itemName.text = consumableItem.name;
        itemDescription.text = consumableItem.description;
        icon.sprite = consumableItem.image;
        recoveryValue = consumableItem.recoveryValue;
        itemPrice = consumableItem.price;
        descriptionItemName.text = consumableItem.name;
        itemPriceText.text = "$" + itemPrice.ToString("00.00");
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

    public void BuyItem(int itemIndex)
    {
        // Acessa o script MarketManager
        MarketManager marketManager = FindObjectOfType<MarketManager>();

        // Verifica se o jogador tem ouro suficiente para comprar o item
        if (marketManager.items[itemIndex].price <= MainMenuController.Instance.goldCount)
        {
            // Deduz o preço do item do ouro do jogador
            MainMenuController.Instance.goldCount -= marketManager.items[itemIndex].price;

            // Adiciona o item ao inventário do jogador
            RefrigeratorInventory.Instance.AddItem(consumableItem);
        }
        else
        {
            Debug.Log("You don't have enough gold!");
        }
    }
}