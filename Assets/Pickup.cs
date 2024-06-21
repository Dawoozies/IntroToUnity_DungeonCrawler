using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : InteractionEvents
{
    public Collectible collectible;
    protected virtual void Awake()
    {
        lookCallback = () =>
        {
            return $"Press {InputManager._interactionKeyText()} to pickup {collectible.ToString()}";
        };
        onStart.AddListener(() => { Collectibles.Collect(collectible); } ) ;
    }
}
