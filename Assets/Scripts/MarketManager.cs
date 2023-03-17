using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MarketManager : MonoBehaviour
{    
    public List<ConsumableItem> items = new List<ConsumableItem>();
    public GameObject itemsContainer;
    public GameObject itemMarketIconPrefab;
    public TextMeshProUGUI goldTxt;

    public MainMenuController mainMenuController;

    private void Start()
    {
        ShowProducts();
    }

    public void ShowProducts()
    {
        for (int i = 0; i < items.Count; i++)
        {
            // Cria um novo objeto de jogo para representar o item na UI
            GameObject itemUI = Instantiate(itemMarketIconPrefab, itemsContainer.transform);

            // Adiciona o componente MarketItem ao objeto de jogo
            MarketItem marketItem = itemUI.GetComponent<MarketItem>();
            marketItem.consumableItem = items[i];

            // Define o �ndice do objeto na lista
            marketItem.itemIndex = i;                        
        }
    }

    private void Update()
    {
        goldTxt.text = MainMenuController.Instance.goldCount.ToString();
    }

}