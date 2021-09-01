using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FPS.Player {
    public class FPSAimController : MonoBehaviour {
        [Header("Input system")]
        [SerializeField] private InputAction _xAxisAimMovementInput;
        [SerializeField] private InputAction _yAxisAimMovementInput;

        [Header("Define player main component to apply proper rotation")]
        // Use the father empty object as reference
        [SerializeField] private Transform _playerControllerBodyTransform;

        //Base properties
        private readonly float _aimSensitivity = 30.0f;
        private float _xAxisCameraRotation = 0f;
        private float _axisXValue;
        private float _axisYValue;

        // Initialize input system and apply cursor lock mode
        private void InitializePlayerAimMomentController() {
            _xAxisAimMovementInput.Enable();
            _yAxisAimMovementInput.Enable();
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void SetInputValues() {
            _axisXValue = _xAxisAimMovementInput.ReadValue<float>() * Time.deltaTime * _aimSensitivity;
            _axisYValue = _yAxisAimMovementInput.ReadValue<float>() * Time.deltaTime * _aimSensitivity;
        }

        private void PerformPlayerAimMovement() {
            _playerControllerBodyTransform.Rotate(Vector3.up * _axisXValue);
            //Apply clamp to prevent bad rotation
            _xAxisCameraRotation -= _axisYValue;
            _xAxisCameraRotation = Mathf.Clamp(_xAxisCameraRotation, -80, 85);
            transform.localRotation = Quaternion.Euler(_xAxisCameraRotation, 0f, 0f);
        }

        private void Start() {
            InitializePlayerAimMomentController();
        }

        private void Update() {
            SetInputValues();
            PerformPlayerAimMovement();
        }
    }
}