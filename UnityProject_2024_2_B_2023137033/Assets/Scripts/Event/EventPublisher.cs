using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPublisher : MonoBehaviour
{
    public EventChannel channel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            channel.RaiseEvent();
        }
    }
}
