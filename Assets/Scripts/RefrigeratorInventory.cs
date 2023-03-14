using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefrigeratorInventory : MonoBehaviour
{
    public static RefrigeratorInventory Instance;

    public int maxCapacity = 20;
    public Dictionary<ConsumableItem, int> items = new Dictionary<ConsumableItem, int>();
    public GameObject inventoryPanel;
    public GameObject iconsContainerWindow;
    public GameObject itemIconPrefab;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {

    }

    public void AddItem(ConsumableItem item, int quantity)
    {
        if (items.ContainsKey(item))
        {
            items[item] += quantity;
        }
        else
        {
            items.Add(item, quantity);
        }

        // Verifica se a quantidade máxima foi atingida e remove o item mais antigo se necessário
        while (items.Count > maxCapacity)
        {
            ConsumableItem oldestItem = null;
            foreach (ConsumableItem i in items.Keys)
            {
                if (oldestItem == null || items[i] < items[oldestItem])
                {
                    oldestItem = i;
                }
            }

            if (oldestItem != null)
            {
                items.Remove(oldestItem);
            }
        }

        ShowInventory();
    }

    public void RemoveItem(ConsumableItem item)
    {
        if (items.ContainsKey(item))
        {
            items[item]--;
            if (items[item] == 0)
            {
                items.Remove(item);
            }
        }

        ShowInventory();
    }

    public void ShowInventory()
    {
        inventoryPanel.SetActive(true);

        // Limpa todos os ícones antigos antes de atualizar a UI
        foreach (Transform child in iconsContainerWindow.transform)
        {
            Destroy(child.gameObject);
        }

        // Cria um novo ícone para cada item no inventário
        foreach (KeyValuePair<ConsumableItem, int> item in items)
        {
            GameObject itemUI = Instantiate(itemIconPrefab, iconsContainerWindow.transform);
            InventoryItem inventoryItem = itemUI.GetComponent<InventoryItem>();
            inventoryItem.consumableItem = item.Key;
            inventoryItem.itemCount.text = item.Value.ToString("00");
        }
    }

    public void HideInventory()
    {
        inventoryPanel.SetActive(false);
    }

    public void SetQuantity(ConsumableItem item, int quantity)
    {
        if (quantity <= 0)
        {
            items.Remove(item);
        }
        else
        {
            items[item] = quantity;
        }

        ShowInventory();
    }
}
