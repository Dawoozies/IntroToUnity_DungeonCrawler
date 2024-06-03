using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerController : MonoBehaviour
{
    public Camera worldCamera;
    public Transform cameraPivot;
    Vector3 cameraPivotOriginPos;
    CharacterController c;
    public float cameraSensitivity;
    public float yRotationLimit;
    public float moveSpeed;
    public float sprintMultiplier;
    bool sprintInput;
    Vector2 angles;

    public float stepSpeed;
    public float sprintStepSpeed;
    float step;
    public UnityEvent<float> onStepTaken;
    Vector3 lastFramePos;
    void Start()
    {
        c = GetComponent<CharacterController>();
        InputManager.RegisterMouseInputCallback(CameraRotationHandler);
        InputManager.RegisterMoveInputCallback(CharacterMovementHandler);
        lastFramePos = transform.position;

        cameraPivotOriginPos = cameraPivot.localPosition;
    }
    void CameraRotationHandler(Vector2 mouseScreenPos, Vector2 mouseWorldPos, Vector2 mouseDelta)
    {
        RotateCamera(mouseDelta*cameraSensitivity);
        angles.y = Mathf.Clamp(angles.y, -yRotationLimit, yRotationLimit);
        Quaternion xRot = Quaternion.AngleAxis(angles.x, Vector3.up);
        Quaternion yRot = Quaternion.AngleAxis(angles.y, Vector3.left);
        cameraPivot.transform.localRotation = xRot * yRot;
    }
    void CharacterMovementHandler(Vector2 moveInput, bool sprintInput)
    {
        float finalMoveSpeed = moveSpeed;
        this.sprintInput = sprintInput;
        if(sprintInput)
        {
            finalMoveSpeed *= sprintMultiplier;
        }
        Vector3 projForward = cameraPivot.forward;
        projForward.y = 0f;
        projForward.Normalize();
        Vector3 projRight = cameraPivot.right;
        projRight.y = 0f;
        projRight.Normalize();
        Vector3 dv = (projForward * moveInput.y + projRight * moveInput.x) * finalMoveSpeed * Time.deltaTime;
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

        Vector3 absMovement = transform.position - lastFramePos;
        float absMovementMagnitude = absMovement.magnitude;
        //Debug.LogError($"absMovement = {absMovement} || magnitude = {absMovement.magnitude}");
        if(absMovementMagnitude > 0)
        {
            float stepLoudnessMultiplier = 1f;
            if(sprintInput)
            {
                stepLoudnessMultiplier = 2f;
                step += Time.deltaTime * sprintStepSpeed;
            }
            else
            {
                step += Time.deltaTime * stepSpeed;
            }

            if(step >= 1)
            {
                onStepTaken?.Invoke(stepLoudnessMultiplier);
                step = 0f;
            }
        }
        lastFramePos = transform.position;
    }
    public void RotateCamera(Vector2 screenMoveDelta)
    {
        angles.x += screenMoveDelta.x;
        angles.y += screenMoveDelta.y;
    }
    public void CameraShake(Vector3 shakeMove)
    {
        cameraPivot.localPosition = cameraPivotOriginPos + shakeMove;
    }
}
