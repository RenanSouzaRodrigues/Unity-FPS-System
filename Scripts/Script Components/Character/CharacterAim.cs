using UnityEngine;
using UnityEngine.InputSystem;

namespace FPS.ScriptComponents.Character {
    public class CharacterAim : MonoBehaviour {
        [Header("Aim Input system")]
        [SerializeField] private InputAction _xAxisAimMovementInput; [Space]
        [SerializeField] private InputAction _yAxisAimMovementInput;

        [Header("Character Root Transform")]
        [SerializeField] private Transform _characterRootTransform;
        [SerializeField] private Camera _camera;

        [Header("Define Aim Settings")]
        [SerializeField] private bool _lockCursor = true; [Space]
        [SerializeField] [Range(1f, 200f)] private float _aimSensitivity = 30.0f;
        
        private Camera _mainCamera;
        private float _xAxisCameraRotation = 0f;

        private void Initialize() {
            this._xAxisAimMovementInput.Enable();
            this._yAxisAimMovementInput.Enable();
            if(this._lockCursor) Cursor.lockState = CursorLockMode.Locked;
        }

        private void PerformAimMovement() {
            float axisValueX = this._xAxisAimMovementInput.ReadValue<float>() * Time.deltaTime * this._aimSensitivity;
            float axisValueY = this._yAxisAimMovementInput.ReadValue<float>() * Time.deltaTime * this._aimSensitivity;

            this.transform.Rotate(Vector3.up * axisValueX);
            this._xAxisCameraRotation -= axisValueY;
            this._xAxisCameraRotation = Mathf.Clamp(this._xAxisCameraRotation, -80, 85);
            this._camera.transform.localRotation = Quaternion.Euler(this._xAxisCameraRotation, 0f, 0f);
        }

        private void Start() {
            this.Initialize();    
        }

        private void Update() {
            this.PerformAimMovement();
        }

    }
}

