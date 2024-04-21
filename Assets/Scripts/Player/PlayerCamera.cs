using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera Instance;
    [SerializeField] private PlayerManager _playerManager;
    public Camera _camera;
    
    [Header("Settings")] 
    [SerializeField] private Transform _camPivot;
    private Vector3 camVeclocity;
    private float camSmooth = 1;
    [SerializeField] private float horizontalRotationSpeed = 200;
    [SerializeField] private float verticalRotationSpeed = 200;
    [SerializeField] private float horizontalAngle;
    [SerializeField] private float verticalAngle;
    
    //MIN & MAX VALUE FOR VERTICAL
    [SerializeField] private float minValue = -30; 
    [SerializeField] private float maxValue = 60;

    //CAMERA COLLISION VALUE
    [SerializeField] private float currentCamPosZ;
    [SerializeField] private float newCamPosZ;
    [SerializeField] private float collisionOffset = 0.2f;
    [SerializeField] private LayerMask collisionLayers;
    private Vector3 camPos;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        currentCamPosZ = _camera.transform.localPosition.z;
    }
    public void HandleCamera()
    {
        if (_playerManager)
        {
            HandleMovement();
            HandleRotation();
            HandleCollision();
        }
    }

    private void HandleMovement()
    {
        var camPosition = Vector3.SmoothDamp(transform.position, _playerManager.transform.position, 
                                            ref camVeclocity, camSmooth * Time.deltaTime);
        transform.position = camPosition;
    }
    private void HandleRotation()
    {
        // IF LOCK-ON -> LOCK-ON ROTATION
        
        // ELSE NORMAL ROTATION
        horizontalAngle += (ReceiveInput.Instance.LookInputValue.x * horizontalRotationSpeed) * Time.deltaTime;
        verticalAngle -= (ReceiveInput.Instance.LookInputValue.y * verticalRotationSpeed) * Time.deltaTime;
        verticalAngle = Mathf.Clamp(verticalAngle, minValue, maxValue);

        // Y <-> X BECAUSE OF ROTATION
        var rotationValue = Vector3.zero;
        rotationValue.y = horizontalAngle;
        var camRotation = Quaternion.Euler(rotationValue);
        transform.rotation = camRotation;
        
        rotationValue = Vector3.zero;
        rotationValue.x = verticalAngle;
        camRotation = Quaternion.Euler(rotationValue);
        _camPivot.localRotation = camRotation;
    }

    private void HandleCollision()
    {
        newCamPosZ = currentCamPosZ;
        var dir = _camera.transform.position - _camPivot.position;
        dir.Normalize();
        if (Physics.SphereCast(_camPivot.position, collisionOffset, dir, out var hit, Mathf.Abs(newCamPosZ),collisionLayers))
        {
            var distance = Vector3.Distance(_camPivot.position, hit.point);
            newCamPosZ = collisionOffset - distance;
        }
        if (Mathf.Abs(newCamPosZ) < collisionOffset)
        {
            newCamPosZ = -collisionOffset;
        }
        camPos.z = Mathf.Lerp(_camera.transform.localPosition.z, newCamPosZ, 0.1f);
        _camera.transform.localPosition = camPos;
    }
    
}
