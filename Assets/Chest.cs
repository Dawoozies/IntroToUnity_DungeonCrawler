using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : InteractionEvents
{
    //
    bool isOpen;
    protected virtual void Awake()
    {
        lookCallback = () =>
        {
            string stateText = isOpen ? "close" : "open";
            return $"Press {InputManager._interactionKeyText()} to {stateText}";
        };
    }
    public void ToggleOpen()
    {
        isOpen = !isOpen;
    }
}
