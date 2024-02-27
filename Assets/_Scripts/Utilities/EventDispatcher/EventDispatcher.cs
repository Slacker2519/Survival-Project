using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class EventDispatcher : MonoBehaviour
{
    Dictionary<EventID, Action<object>> _allEvent = new Dictionary<EventID, Action<object>>();



    static EventDispatcher s_instance;
    public static EventDispatcher Instance
    {
        get
        {
            if (s_instance == null)
            {

                GameObject singletonObject = new GameObject();
                s_instance = singletonObject.AddComponent<EventDispatcher>();
                singletonObject.name = "EventDispatcher";
            }
            return s_instance;
        }
        private set { }
    }

    public static bool HasInstance()
    {
        return s_instance != null;
    }



    /// <summary>
    /// Register Event.
    /// </summary>
    /// <param name="eventID"></param>
    /// <param name="callback"></param>
    public void RegisterEvent(EventID eventID, Action<object> callback)
    {
        if (_allEvent.ContainsKey(eventID))
        {
            _allEvent[eventID] += callback;
        }
        else
        {
            _allEvent.Add(eventID, null);
            _allEvent[eventID] += callback;
        }
    }



    /// <summary>
    /// Unregister Event.
    /// </summary>
    /// <param name="eventID"></param>
    /// <param name="callback"></param>
    public void UnregisterEvent(EventID eventID, Action<object> callback)
    {
        if (_allEvent.ContainsKey(eventID))
        {
            _allEvent[eventID] -= callback;
        }
        else
        {
            Debug.LogWarning($"UnregisterEvent {eventID.ToString()} false, null event!");
        }
    }


    public void PostEvent(EventID eventID, object param = null)
    {
        if (!_allEvent.ContainsKey(eventID))
        {
            Debug.LogWarning("You're post an event do not exist");
            return;
        }
        var callbacks = _allEvent[eventID];
        callbacks?.Invoke(param);
    }



    /// <summary>
    /// Clear all event.
    /// </summary>
    public void ClearAllEvent()
    {
        _allEvent.Clear();
    }


    private void OnDestroy()
    {
        _allEvent.Clear();
    }
}

public static class EventDispatcherExtenstion
{
    /// <summary>
    /// Register event.
    /// </summary>
    /// <param name="listener"></param>
    /// <param name="eventID"></param>
    /// <param name="callback"></param>
    public static void RegisterEvent( this MonoBehaviour listener , EventID eventID, Action<object> callback)
    {
        EventDispatcher.Instance.RegisterEvent(eventID,callback);
    }

    /// <summary>
    /// Unregister event.
    /// </summary>
    /// <param name="listener"></param>
    /// <param name="eventID"></param>
    /// <param name="callback"></param>
    public static void UnregisterEvent(this MonoBehaviour listener, EventID eventID, Action<object> callback)
    {
        EventDispatcher.Instance.UnregisterEvent(eventID,callback);
    }



    /// <summary>
    /// Post event with param.
    /// </summary>
    /// <param name="listener"></param>
    /// <param name="eventID"></param>
    /// <param name="param"></param>
    public static void PostEvent(this MonoBehaviour listener, EventID eventID, object param)
    {
        EventDispatcher.Instance.PostEvent(eventID,param);
    }

    /// <summary>
    /// Post event without param.
    /// </summary>
    /// <param name="listener"></param>
    /// <param name="eventID"></param>
    public static void PostEvent(this MonoBehaviour listener, EventID eventID)
    {
        EventDispatcher.Instance.PostEvent(eventID, null);
    }

}
