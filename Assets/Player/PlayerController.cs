using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera worldCamera;
    public Transform cameraPivot;
    CharacterController c;
    public float cameraSensitivity;
    public float yRotationLimit;
    public float moveSpeed;
    Vector2 angles;
    void Start()
    {
        c = GetComponent<CharacterController>();
        InputManager.RegisterMouseInputCallback(CameraRotationHandler);
        InputManager.RegisterMoveInputCallback(CharacterMovementHandler);
    }
    void CameraRotationHandler(Vector2 mouseScreenPos, Vector2 mouseWorldPos, Vector2 mouseDelta)
    {
        RotateCamera(mouseDelta*cameraSensitivity);
        angles.y = Mathf.Clamp(angles.y, -yRotationLimit, yRotationLimit);
        Quaternion xRot = Quaternion.AngleAxis(angles.x, Vector3.up);
        Quaternion yRot = Quaternion.AngleAxis(angles.y, Vector3.left);
        cameraPivot.transform.localRotation = xRot * yRot;
    }
    void CharacterMovementHandler(Vector2 moveInput)
    {
        Vector3 dv = (cameraPivot.forward * moveInput.y + cameraPivot.right * moveInput.x) * moveSpeed * Time.deltaTime;
        dv.y = 0;
        c.Move(dv);
    }
    void Update()
    {
        worldCamera.transform.position = cameraPivot.position;
        worldCamera.transform.forward = cameraPivot.forward;
        if(!c.isGrounded)
        {
            c.Move(Vector3.down);
        }
    }
    public void RotateCamera(Vector2 screenMoveDelta)
    {
        angles.x += screenMoveDelta.x;
        angles.y += screenMoveDelta.y;
    }
}
