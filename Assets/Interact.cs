using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Interact : MonoBehaviour
{
    public Camera worldCamera;
    Vector2 screenMiddle = new Vector2(Screen.width / 2f, Screen.height / 2f);
    public LayerMask interactLayers;
    public float maxInteractDist;
    InteractionEvents interactionEvents;
    TextMeshProUGUI interactionText;
    void Start()
    {
        interactionText = GameObject.FindWithTag("InteractionText").GetComponent<TextMeshProUGUI>();
        if (interactionText == null)
        {
            return;
        }
        interactionText.text = "";
        InputManager.RegisterInteractionInputCallback(InteractionInputHandler);
    }
    void InteractionInputHandler(int inputState, float heldTime)
    {
        if(interactionEvents == null)
        {
            return;
        }

        switch (inputState) {
            case 0:
                //Debug.LogError("Interact start");
                interactionEvents.InteractionStart();
                break;
            case 1:
                //Debug.LogError("Interact update");
                interactionEvents.InteractionUpdate();
                break;
            case 2:
                //Debug.LogError("Interact exit");

                interactionEvents.InteractionExit();
                break;
            default:
                break;

        }
    }
    private void Update()
    {
        if(interactionText == null)
        {
            return;
        }
        Ray ray = worldCamera.ScreenPointToRay(screenMiddle);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, maxInteractDist, interactLayers))
        {
            interactionEvents = hit.collider.GetComponentInParent<InteractionEvents>();
        }
        else
        {
            interactionEvents = null;
        }
        if(interactionEvents != null)
        {
            string lookText = interactionEvents.Look();
            interactionText.text = lookText;
        }
        else
        {
            interactionText.text = "";
        }
    }
}
