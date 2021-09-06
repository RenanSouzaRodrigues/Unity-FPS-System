using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using FPS.ScriptableObjects;
using FPS.Common;

namespace FPS.Player {
    public class FPSMovementController : MonoBehaviour {
        [Header("Base Movement Controller Properties")]
        [SerializeField] private CommonAttributes _attributes;

        [Header("Define if necessary, the personal audio manager")]
        [SerializeField] private bool _usePersonalAudioManager = true;
        [SerializeField] private bool _autoImportPersonalAudioManager = false;
        [SerializeField] private PersonalSoundManager _personalSoundManager;

        [Header("Define all the movement inputs")]
        [SerializeField] private InputAction _controllerMovementInput;
        [SerializeField] private InputAction _controllerRunInput;
        [SerializeField] private InputAction _controllerJumpInput;

        [Header("Define the Character Controller component")]
        [SerializeField] private CharacterController _characterController;

        [Header("Define the ground check position and the layer thats marks the floor")]
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private LayerMask _groundLayerMask;

        [Header("Player body and face references")]
        [SerializeField] private bool _displayPlayerBodyAndFace = true;
        [SerializeField] private GameObject _playerCapsuleBody;
        [SerializeField] private GameObject _PlayerCubeFace;

        // This definitions are necessary to calculate the player movement and physics attributes
        private readonly float _groundDistanceRadius = 0.5f;
        private Vector3 _controllerVelocity;
        private bool _controllerIsGrounded = true;
        private Vector2 _movementDirection;
        private bool _controllerIsRunning = false;
        private bool _controllerIsJumping = false;

        private void InitializePlayerControllerMovementConfig() {
            this._controllerMovementInput.Enable();
            this._controllerRunInput.Enable();
            this._controllerJumpInput.Enable();
            this._characterController = GetComponent<CharacterController>();
            if (!this._displayPlayerBodyAndFace) {
                this._playerCapsuleBody.GetComponent<MeshRenderer>().enabled = false;
                this._PlayerCubeFace.GetComponent<MeshRenderer>().enabled = false;
            }
            if(this._usePersonalAudioManager && this._autoImportPersonalAudioManager) {
                this._personalSoundManager = this.GetComponent<PersonalSoundManager>();
            }
        }

        private void PerformPlayerControllerMovement() {
            this.SetInputs();
            this.HandleControllerGroundCheckAndFallPhysics();
            this.HandleControllerMovement();
            this.HandleControllerJump();
        }

        private void SetInputs() {
            // In this case, the actual values that must be used are x and y. But y value
            // must be used on the Character Controller z value. For Character Controller, 
            // x will define left and right and z will define back and foward movement.
            this._movementDirection = this._controllerMovementInput.ReadValue<Vector2>();
            this._controllerIsRunning = this._controllerRunInput.ReadValue<float>() > 0;
            this._controllerIsJumping = this._controllerJumpInput.ReadValue<float>() > 0;
        }

        private void HandleControllerMovement() {
            // Simple moving mechanics
            Vector3 characterControllerMovement =
               transform.right * this._movementDirection.x + transform.forward * this._movementDirection.y;

            bool playerIsRunning = this._controllerIsRunning;

            if (this._movementDirection.y < 0) playerIsRunning = false; 

            this._characterController.Move(
                characterControllerMovement * (playerIsRunning && this._controllerIsGrounded ? 
                    this._attributes.GetMovementSpeed() * this._attributes.GetRunningMultiplier() : 
                    this._attributes.GetMovementSpeed()) * Time.deltaTime
            );
        }

        private void HandleControllerGroundCheckAndFallPhysics() {
            // Physics.CheckSphere will create a small sphere to check if the FPSController is on the ground.
            // For more info please check https://docs.unity3d.com/ScriptReference/Physics.CheckSphere.html
            this._controllerIsGrounded = Physics.CheckSphere(
                this._groundCheck.position, this._groundDistanceRadius, this._groundLayerMask
            );
            
            if (this._controllerIsGrounded && _controllerVelocity.y < 0) {
                this._controllerVelocity.y = -2f;
            }

            // The Physics behind this are vary basic. You just need to calculate the velocity delta using
            // Dy = ½g * t². This will define the 'fall velocity'
            this._controllerVelocity.y += this._attributes.GetGraivityForce() * Time.deltaTime;
            this._characterController.Move(_controllerVelocity * Time.deltaTime);
        }

        private void HandleControllerJump() {
            // Controller can jump using simple physics too. Just use v = sqrRoot(h * -2 * g)
            if (this._controllerIsJumping && this._controllerIsGrounded) {
                if(this._usePersonalAudioManager) this._personalSoundManager.PlayJumpSound();

                this._controllerVelocity.y = Mathf.Sqrt(
                    this._attributes.GetJumpHeight() * -2f * this._attributes.GetGraivityForce()
                );
            }
            _characterController.Move(this._controllerVelocity * Time.deltaTime);
        }

        public void Start() {
            this.InitializePlayerControllerMovementConfig();
        }

        public void Update() {
            // This is the main mathod. It will handle all the movement functions. 
            // Also, it uses some helpers to handle all the movement states.
            this.PerformPlayerControllerMovement();
        }
    }
}