using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExItem
{
    public bool isCollected;
}

public class ExCollectedSystem : MonoBehaviour
{
    public List<ExItem> items = new();

    private void Start()
    {
        items.Add(new() { isCollected = true });
        items.Add(new() { isCollected = true });

        CheckAllItemsCollected();
    }

    private void CheckAllItemsCollected()
    {
        if (items.All(x => x.isCollected))
        {
            Debug.Log("Collected");
        }
        else
        {
            Debug.Log("Uncollected");
        }
    }
}
