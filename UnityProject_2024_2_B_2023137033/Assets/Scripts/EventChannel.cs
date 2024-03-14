using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewEventChannel", menuName = "Events/Event Channel")]
public class EventChannel : ScriptableObject
{
    public UnityEvent Event;

    public void RaiseEvent()
    {
        Event?.Invoke();
    }
}
