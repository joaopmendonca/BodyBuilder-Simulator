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
    public AudioClip useItemSound;

    public GameObject itemDescriptionLabel;

    // Start is called before the first frame update
    void Start()
    {
        itemName.text = consumableItem.itemName;
        itemDescription.text = consumableItem.description;
        icon.sprite = consumableItem.image;
        recoveryValue = consumableItem.feedRecoveryValue;
        useItemSound = consumableItem.useItemSound;
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
        switch (consumableItem.itemType)
        {
            case ConsumableItemType.Food:
                UseFoodItem();
                break;
            case ConsumableItemType.Supplement:
                UseSupplementItem();
                break;
            case ConsumableItemType.Medicine:
                UseMedicineItem();
                break;
            default:
                break;
        }
    }

    private void UseFoodItem()
    {
        if (useItemSound != null)
        {
            AudioController.Instance.PlaySound(useItemSound);
        }
        // Adiciona o valor de recuperação de feed do item no jogador
        MainMenuController.Instance.currentFeed += recoveryValue;

        // Verifica se o jogador ultrapassou o valor máximo permitido de feed
        if (MainMenuController.Instance.currentFeed > MainMenuController.Instance.maxFeed)
        {
            MainMenuController.Instance.currentFeed = MainMenuController.Instance.maxFeed;
        }

        // Atualiza a barra de feed do jogador
        MainMenuController.Instance.feedBar.fillAmount = (float)MainMenuController.Instance.currentFeed / MainMenuController.Instance.maxFeed;

        //Atualiza a quantidade do item no inventário
        int quantity = 0;
        if (RefrigeratorInventory.Instance.items.TryGetValue(consumableItem, out quantity))
        {
            RefrigeratorInventory.Instance.SetQuantity(consumableItem, quantity - 1);
        }
    }

    private void UseSupplementItem()
    {
        if (useItemSound != null)
        {
            AudioController.Instance.PlaySound(useItemSound);
        }

        // Acessa a quantidade de suplementação do jogador e aumenta em 10
        MainMenuController.Instance.currentSuplementation += 10;

        //Atualiza a quantidade do item no inventário
        int quantity = 0;
        if (RefrigeratorInventory.Instance.items.TryGetValue(consumableItem, out quantity))
        {
            RefrigeratorInventory.Instance.SetQuantity(consumableItem, quantity - 1);
        }
    }

    private void UseMedicineItem()
    {
        if (useItemSound != null)
        {
            AudioController.Instance.PlaySound(useItemSound);
        }
        AudioController.Instance.PlaySound(useItemSound);
        // Acessa a quantidade de energia do jogador e aumenta em 10
        MainMenuController.Instance.currentEnergy += 10;

        //Atualiza a quantidade do item no inventário
        int quantity = 0;
        if (RefrigeratorInventory.Instance.items.TryGetValue(consumableItem, out quantity))
        {
            RefrigeratorInventory.Instance.SetQuantity(consumableItem, quantity - 1);
        }
    }

}