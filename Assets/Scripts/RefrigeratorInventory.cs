using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefrigeratorInventory : MonoBehaviour
{
    public static RefrigeratorInventory Instance;

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

    public void AddItem(ConsumableItem item)
    {
        if (items.ContainsKey(item))
        {
            items[item]++;
        }
        else
        {
            items.Add(item, 1);
        }
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

}