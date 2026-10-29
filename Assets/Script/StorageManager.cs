using System.Collections.Generic;
using UnityEngine;

public class StorageManager
{
    private readonly List<Rarity> items = new List<Rarity>();

    public IReadOnlyList<Rarity> Items => items;

    public int TotalValue
    {
        get
        {
            int coinValue = 0;

            foreach(Rarity rarity in items)
            {
                coinValue += SellValue(rarity);
            }

            return coinValue;
        }
    }

    public void AddItem(Rarity rarity)
    {
        items.Add(rarity);
        items.Sort((a, b) => b.CompareTo(a));
    }

    public bool RemoveItem(Rarity rarity)
    {
        return items.Remove(rarity);
    }

    public int SellValue(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.Metal: return 10;
            case Rarity.Bronze: return 20;
            case Rarity.Silver: return 30;
            case Rarity.Gold: return 40;
            case Rarity.Diamond: return 50;
            default: return 0;
        }
    }
}
