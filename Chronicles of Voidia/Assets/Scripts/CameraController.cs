using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Camera cam;
    
    [Header("Settings")]
    private float moveSpeed = 5f;
    [SerializeField] private float moveMaxSpeed = 5f;
    [SerializeField] private float moveAcceleration = 10f;
    [SerializeField] private float moveDeceleration = 15f;
    [Space]
    [SerializeField] private float zoomStep = 2f;
    [SerializeField] private float zoomSpeed = 2f;
    [SerializeField] private float zoomAcceleration = 10f;
    [SerializeField] private float zoomDeceleration = 7.5f;
    [SerializeField] private float zoomMin = 5f;
    [SerializeField] private float zoomMax = 50f;
    [Space]
    [SerializeField] private float rotateSpeed = 1f;
    [Space]
    [SerializeField] private float edgeTollerance = 0.05f;
    [SerializeField] private bool useScreenEdgeInput = false;
    
    private Vector3 targetPosition;
    private float zoomHeigt;
    
    private Vector3 moveVelocity;
    private Vector3 lastPosition;
    private Vector3 dragStartPosition;
    
    private void Awake()
    {
        lastPosition = transform.position;
    }

    private void OnEnable()
    {
        InputHandler.OnKeyboardMovementTriggered += UpdateKeyBoardMovement;
    }
    
    private void OnDisable()
    {
        InputHandler.OnKeyboardMovementTriggered -= UpdateKeyBoardMovement;
    }

    private void Update()
    {
        UpdateVelocity();
        UpdateBasePosition();
        
        // UpdateCameraMovement();
        // UpdateCameraZoom();
        // UpdateCameraRotation();
    }
    
    private void UpdateVelocity()
    {
        var pos = transform.position;
        moveVelocity = (pos - lastPosition) / Time.deltaTime;
        moveVelocity.y = 0;
        lastPosition = pos;
    }
    
    private void UpdateKeyBoardMovement(Vector2 input)
    {
        Debug.Log($"Inputs: {input}");
        
        Vector3 inputValues = new Vector3(input.x, 0, input.y);
        inputValues.x *= GetCameraRight().x;
        inputValues.z *= GetCameraForward().z;
        
        inputValues.Normalize();
        if (inputValues.sqrMagnitude <= 0.1f) return;
        
        targetPosition += inputValues * moveSpeed;
    }
    
    private Vector3 GetCameraRight()
    {
        var right = cam.transform.right;
        right.y = 0;
        return right.normalized;
    }
    
    private Vector3 GetCameraForward()
    {
        var forward = cam.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }
    
    private void UpdateBasePosition()
    {
        if (targetPosition.sqrMagnitude > 0.1f)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, moveMaxSpeed, moveAcceleration * Time.deltaTime);
            transform.position += targetPosition * (moveSpeed * Time.deltaTime);
        }
        else
        {
            moveVelocity = Vector3.Lerp(moveVelocity, Vector3.zero, moveDeceleration * Time.deltaTime);
            transform.position += moveVelocity * Time.deltaTime;
        }
        targetPosition = Vector3.zero;
    }
    
    private void RotateCamera(Vector2 input)
    {
        if (!Mouse.current.middleButton.isPressed) return;
        
        transform.rotation *= Quaternion.Euler(0, input.x * rotateSpeed, 0);
    }
    
    private void ZoomCamera(float input)
    {
        zoomHeigt -= input * zoomStep * zoomSpeed;
        zoomHeigt = Mathf.Clamp(zoomHeigt, zoomMin, zoomMax);
        
        cam.transform.localPosition = new Vector3(0, zoomHeigt, -zoomHeigt);
    }
}
