using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private int index;
    private string name;
    private ItemType type;
    private Sprite image;

    public int Index
    {
        get { return index; }
        set { index = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public ItemType Type
    {
        get { return type; }
        set { type = value; }
    }

    public Sprite Image
    {
        get { return image; }
        set { image = value; }
    }

    public Item(int index, string name, ItemType type)
    {
        this.index = index;
        this.name = name;
        this.type = type;
    }
}

public class Inventory
{
    private Item[] items = new Item[16];

    public Item this[int index]
    {
        get { return items[index]; }
        set { items[index] = value; }
    }

    public int ItemCount
    {
        get
        {
            int count = 0;
            foreach (Item item in items)
            {
                if (item != null)
                {
                    count++;
                }
            }
            return count;
        }
    }

    public int InventoryCount => items.Length;

    public bool AddItem(Item item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = item;
                return true;
            }
        }
        return false;
    }

    public void RemoveItem(Item item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == item)
            {
                items[i] = null;
                break;
            }
        }
    }
}

public enum ItemType
{
    Weapon,
    Armor,
    Potion,
    QuestItem,
}

public class ExGameSystem : MonoBehaviour
{
    private Inventory inventory = new Inventory();

    Item sword = new Item(0, "Sword", ItemType.Weapon);
    Item shield = new Item(0, "Shield", ItemType.Armor);

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            inventory.AddItem(sword);
            Debug.Log(GetInventoryAsString());
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            inventory.AddItem(shield);
            Debug.Log(GetInventoryAsString());
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            inventory.RemoveItem(sword);
            Debug.Log(GetInventoryAsString());
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            inventory.RemoveItem(shield);
            Debug.Log(GetInventoryAsString());
        }
    }

    private string GetInventoryAsString()
    {
        string result = "";
        for (int i = 0; i < inventory.InventoryCount; i++)
        {
            if (inventory[i] != null)
            {
                result += inventory[i].Name + ", ";
            }
        }

        return result.TrimEnd(',', ' ');
    }
}
