using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class InteractionEvents : MonoBehaviour
{
    [Tooltip("On every frame after looked at")]
    public UnityEvent onLook;
    [Tooltip("On interaction first frame")]
    public UnityEvent onStart;
    [Tooltip("On interaction every frame after start event")]
    public UnityEvent onUpdate;
    [Tooltip("On interaction stopped")]
    public UnityEvent onExit;
    //set by interactable thing
    public Func<string> lookCallback;
    public string Look()
    {
        onLook?.Invoke();

        if (lookCallback != null)
        {
            return lookCallback();
        }
        else
        {
            return "";
        }
    }
    public void InteractionStart()
    {
        onStart?.Invoke();
    }
    public void InteractionUpdate()
    {
        onUpdate?.Invoke();
    }
    public void InteractionExit()
    {
        onExit?.Invoke();
    }
}