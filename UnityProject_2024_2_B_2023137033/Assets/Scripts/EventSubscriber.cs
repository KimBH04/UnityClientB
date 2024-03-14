using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSubscriber : MonoBehaviour
{
    public EventChannel channel;

    private void OnEnable()
    {
        Debug.Log("Enable");
        channel.Event.AddListener(OnEventRaised);
    }

    private void OnDisable()
    {
        Debug.Log("Disable");
        channel.Event.RemoveListener(OnEventRaised);
    }

    private void OnEventRaised()
    {
        Debug.Log($"Event On : {name}");
    }
}
