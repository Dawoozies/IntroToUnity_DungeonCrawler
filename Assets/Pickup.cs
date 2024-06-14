using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : InteractionEvents
{
    protected virtual void Awake()
    {
        lookCallback = () =>
        {
            return $"Press {InputManager._interactionKeyText()} to pickup";
        };
    }
}
