using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractionEvents
{
    Animator animator;
    public bool locked;
    public Collectible requiredCollectible;
    bool canUnlock => Collectibles.collected[(int)requiredCollectible];
    bool isOpen;
    void Awake()
    {
        lookCallback = () => {
            string doorText = $"LOCKED";
            if (canUnlock)
            {
                doorText = $"Press {InputManager._interactionKeyText()} to unlock with {requiredCollectible.ToString()} ";
            }
            if(!locked)
            {
                if(isOpen)
                {
                    doorText = $"Press {InputManager._interactionKeyText()} to close";
                }
                else
                {
                    doorText = $"Press {InputManager._interactionKeyText()} to open";
                }
            }
            return doorText;
        };
        animator = GetComponent<Animator>();
    }
    public void ToggleOpen()
    {
        if (locked && !canUnlock)
            return;
        if(locked && canUnlock)
        {
            locked = false;
            return;
        }
        isOpen = !isOpen;
    }
    void Update()
    {
        animator.SetBool("Open", isOpen);
    }
}
