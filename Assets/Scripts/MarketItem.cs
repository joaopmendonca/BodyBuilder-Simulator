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
    public Slider amountSlider;
    public TextMeshProUGUI amountText;
    public Button buyButton;
    public Image[] sliderColor;

    private static MarketItem currentDescriptionItem = null;

    // Start is called before the first frame update
    void Start()
    {
        itemName.text = consumableItem.name;
        itemDescription.text = consumableItem.description;
        icon.sprite = consumableItem.image;
        recoveryValue = consumableItem.feedRecoveryValue;
        itemPrice = consumableItem.price;
        descriptionItemName.text = consumableItem.name;
        itemPriceText.text = "$" + itemPrice.ToString("00.00");

        UpdateMaxAmount();

        amountSlider.onValueChanged.AddListener(OnAmountSliderValueChanged);
        amountSlider.maxValue = Mathf.Min(amountSlider.maxValue, 99);
        amountText.text = "1";

        MainMenuController.Instance.onGoldCountChanged.AddListener(UpdateMaxAmount);
    }

    private void OnDestroy()
    {
        MainMenuController.Instance.onGoldCountChanged.RemoveListener(UpdateMaxAmount);
    }

    private void UpdateMaxAmount()
    {
        int maxAmount = Mathf.Min(Mathf.FloorToInt(MainMenuController.Instance.goldCount / itemPrice), 99);

        if (maxAmount <= 0)
        {
            // Define o valor máximo do slider como zero
            amountSlider.maxValue = 0;

            foreach (Image img in sliderColor)
            {
                img.color = new Color(200f / 255f, 200f / 255f, 200f / 255f, 1f);
            }

            amountSlider.interactable = false;
            amountSlider.value = 0;
            amountText.text = "0";
        }
        else
        {
            // Define o valor máximo do slider como maxAmount
            amountSlider.maxValue = maxAmount;

            foreach (Image img in sliderColor)
            {
                img.color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 1f);
            }
            amountSlider.interactable = true;
            amountSlider.value = Mathf.Min(amountSlider.value, maxAmount);
            amountText.text = Mathf.FloorToInt(amountSlider.value).ToString();
        }
    }



    // Update is called once per frame
    void Update()
    {
        // Verifica se a quantidade selecionada no slider é maior que zero e se o jogador tem ouro suficiente para comprar o item
        int totalPrice = itemPrice * Mathf.FloorToInt(amountSlider.value);
        if (amountSlider.value > 0 && totalPrice <= MainMenuController.Instance.goldCount)
        {
            // O jogador tem ouro suficiente e a quantidade selecionada é maior que zero para comprar o item, reativa o botão de compra
            buyButton.interactable = true;
            buyButton.GetComponent<Image>().color = Color.white;
        }
        else
        {
            // O jogador não tem ouro suficiente ou a quantidade selecionada é igual a zero para comprar o item, desativa o botão de compra
            buyButton.interactable = false;
            buyButton.GetComponent<Image>().color = Color.grey;
        }
    }

    private void OnAmountSliderValueChanged(float value)
    {
        amountText.text = Mathf.FloorToInt(value).ToString();
    }

    public void OnItemClick()
    {
        if (currentDescriptionItem != null)
        {
            currentDescriptionItem.itemDescriptionLabel.SetActive(false);
        }

        itemDescriptionLabel.SetActive(true);
        currentDescriptionItem = this;
    }

    public void OnBuyButtonClick()
    {
        int quantity = Mathf.FloorToInt(amountSlider.value);
        int totalPrice = itemPrice * quantity;

        // Verifica se o jogador tem ouro suficiente para comprar o item
        if (totalPrice <= MainMenuController.Instance.goldCount)
        {
            // Remove o ouro necessário do jogador
            MainMenuController.Instance.RemoveGold(totalPrice);

            // Adiciona o item ao inventário
            RefrigeratorInventory.Instance.SetQuantity(consumableItem, RefrigeratorInventory.Instance.items.GetValueOrDefault(consumableItem, 0) + quantity);

        }
    }

}