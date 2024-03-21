using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExGetData : MonoBehaviour
{
    public Entity_Data data;

    private void Start()
    {
        foreach (var param in data.param)
        {
            Debug.Log($"{param.name} - {param.hp} - {param.mp}");
        }
    }
}
