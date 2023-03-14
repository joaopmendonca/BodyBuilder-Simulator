using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenCabinetInventory : MonoBehaviour
{
    public static KitchenCabinetInventory Instance { get; private set; }

    public Dictionary<InventoryItem, int> items = new Dictionary<InventoryItem, int>();

    public int maxCapacity = 20;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItem(InventoryItem item)
    {
        if (items.Count < maxCapacity)
        {
            int quantity;
            if (items.TryGetValue(item, out quantity))
            {
                items[item] = quantity + 1;
            }
            else
            {
                items.Add(item, 1);
            }
        }
        else
        {
            Debug.LogWarning("Kitchen cabinet inventory is full!");
        }
    }

    public void SetQuantity(InventoryItem item, int quantity)
    {
        if (items.ContainsKey(item))
        {
            items[item] = quantity;
        }
    }

    public void RemoveItem(InventoryItem item)
    {
        if (items.ContainsKey(item))
        {
            items.Remove(item);
        }
    }

    public bool HasItem(InventoryItem item)
    {
        return items.ContainsKey(item);
    }

    public int GetQuantity(InventoryItem item)
    {
        int quantity = 0;
        if (items.TryGetValue(item, out quantity))
        {
            return quantity;
        }
        return 0;
    }

    public int GetTotalItems()
    {
        int total = 0;
        foreach (KeyValuePair<InventoryItem, int> pair in items)
        {
            total += pair.Value;
        }
        return total;
    }
}
