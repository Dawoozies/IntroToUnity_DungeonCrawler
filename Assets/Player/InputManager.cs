using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static List<Action<Vector2, Vector2, Vector2>> mouseInputActions = new();
    public static List<Action<Vector2>> moveInputActions = new();
    public static List<Action<float>> mouseLeftClickActions = new();
    public static List<Action<float>> jumpInputActions = new();
    float leftClickHeldTime = 0f;
    float jumpHeldTime = 0f;
    public KeyCode reloadKey;
    float reloadInputBuffer;
    public static List<Action<float>> reloadInputActions = new();
    public static List<Action<float>> mouseRightClickActions = new();
    float rightClickHeldTime = 0f;
    public static List<Action<float>> scrollWheelInputActions = new();
    public float scrollWheelDeadzone;

    //Interaction
    public static List<Action<int, float>> interactionInputActions = new();
    public KeyCode interactionKey;
    public static KeyCode _interactionKey;
    float interactionHeldTime = 0f;
    public static string _interactionKeyText()
    {
        return $"<b><color=#ff0000ff>{_interactionKey}</color></b>";
    }
    void Update()
    {
        _interactionKey = interactionKey;

        Vector2 mouseScreenPos = Input.mousePosition;
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        Vector2 mouseDelta = new(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        foreach (Action<Vector2,Vector2,Vector2> action in mouseInputActions)
        {
            action(mouseScreenPos, mouseWorldPos, mouseDelta);
        }
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        foreach (Action<Vector2> action in moveInputActions)
        {
            action(moveInput);
        }
        foreach (Action<float> action in mouseLeftClickActions)
        {
            action(leftClickHeldTime);
        }
        if (Input.GetMouseButton(0))
        {

            leftClickHeldTime += Time.deltaTime;
        }
        else
        {
            leftClickHeldTime = 0f;
        }
        foreach (Action<float> action in jumpInputActions)
        {
            action(jumpHeldTime);
        }
        if (Input.GetButton("Jump"))
        {
            jumpHeldTime += Time.deltaTime;
        }
        else
        {
            jumpHeldTime = 0f;
        }
        //Interaction
        bool interactionDown = Input.GetKeyDown(interactionKey);
        bool interactionHeld = Input.GetKey(interactionKey);
        bool interactionUp = Input.GetKeyUp(interactionKey);
        //Debug.Log($"down = {interactionDown} held = {interactionHeld} up = {interactionUp}");
        int interactionKeyState = -1;
        switch ((interactionDown, interactionHeld, interactionUp))
        {
            case (true, true, false):
                //down input
                interactionKeyState = 0;
                break;
            case (false, true, false):
                //held input
                interactionKeyState = 1;
                interactionHeldTime += Time.deltaTime;
                break;
            case (false, false, true):
                interactionKeyState = 2;
                break;
            default:
                interactionHeldTime = 0;
                break;
        }
        foreach (var action in interactionInputActions)
        {
            action(interactionKeyState, interactionHeldTime);
        }


        if (Input.GetKey(reloadKey))
        {
            reloadInputBuffer = 0.45f;
        }
        else
        {
            if(reloadInputBuffer > 0)
            {
                reloadInputBuffer -= Time.deltaTime;
            }
        }
        foreach (Action<float> action in reloadInputActions)
        {
            action(reloadInputBuffer);
        }
        if (Input.GetMouseButton(1))
        {
            rightClickHeldTime += Time.deltaTime;
        }
        else
        {
            rightClickHeldTime = 0f;
        }
        foreach (var action in mouseRightClickActions)
        {
            action(rightClickHeldTime);
        }
        float scrollDelta = Input.mouseScrollDelta.y;
        if(Mathf.Abs(scrollDelta) < scrollWheelDeadzone)
        {
            scrollDelta = 0f;
        }
        foreach(var action in scrollWheelInputActions)
        {
            action(scrollDelta);
        }
    }
    public static void RegisterMouseInputCallback(Action<Vector2, Vector2, Vector2> action)
    {
        mouseInputActions.Add(action);
    }
    public static void RegisterMoveInputCallback(Action<Vector2> action)
    {
        moveInputActions.Add(action);
    }
    public static void RegisterMouseLeftClickCallback(Action<float> action)
    {
        mouseLeftClickActions.Add(action);
    }
    public static void RegisterJumpInputCallback(Action<float> action)
    {
        jumpInputActions.Add(action);
    }
    public static void RegisterInteractionInputCallback(Action<int, float> action)
    {
        interactionInputActions.Add(action);
    }
    public static void RegisterReloadInputCallback(Action<float> action)
    {
        reloadInputActions.Add(action);
    }
    public static void RegisterMouseRightClickCallback(Action<float> action)
    {
        mouseRightClickActions.Add(action);
    }
    public static void RegisterScrollWheelCallback(Action<float> action)
    {
        scrollWheelInputActions.Add(action);
    }
}
