using System;
using UnityEngine;
using UnityEngine.InputSystem;   
using FPS.ScriptableObjects;

namespace FPS.ScriptComponents.Character {
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovement : MonoBehaviour {
        [Header("Define the 2D movment inputs")]
        [SerializeField] private InputAction _movementInputs; 
        [Space]
        [Header("Define the ground check position and the layer thats marks the floor")]
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private LayerMask _groundLayerMask;
        [Space]
        [Header("Player body and face references")]
        [SerializeField] private bool _displayPlayerBodyAndFace = true;
        [SerializeField] private GameObject _playerCapsuleBody;
        [SerializeField] private GameObject _PlayerCubeFace; 
        [Space]
        [Header("Define the character attributes")]
        [SerializeField] private CommonAttributes _attributes;

        private CharacterController _characterController;
        private bool _isGrounded = true;
        private readonly float _groundDistanceRadius = 0.5f;
        private Vector3 _characterVelocity;
        private CharacterRun _characterRunEventSubscriber;
        private CharacterJump _characterJumpEventSubscriber;
        private bool _isRunning = false;
        private bool _isJumping = false;

        private void Initialize() {
            this._movementInputs.Enable();
            this._characterController = this.GetComponent<CharacterController>();
            if (!this._displayPlayerBodyAndFace) {
                this._playerCapsuleBody.GetComponent<MeshRenderer>().enabled = false;
                this._PlayerCubeFace.GetComponent<MeshRenderer>().enabled = false;
            }
            this._characterRunEventSubscriber = GetComponent<CharacterRun>();
            if(this._characterRunEventSubscriber != null) {
                this._characterRunEventSubscriber.OnRun += (sender, isRunning) => {  this._isRunning = isRunning; };
            }
            this._characterJumpEventSubscriber = GetComponent<CharacterJump>();
            if(this._characterJumpEventSubscriber != null) {
                this._characterJumpEventSubscriber.OnJump += (sender, isJumping) => {  this._isJumping = isJumping; };
            }
        }

        private void HandleMovement() {
            Vector2 movementDirection = this._movementInputs.ReadValue<Vector2>();
            Vector3 movement = transform.right * movementDirection.x + transform.forward * movementDirection.y;
            float finalSpeed = this._isRunning ? this._attributes.GetFullSpeed() : this._attributes.GetMovementSpeed();
            this._characterController.Move(movement * (finalSpeed) * Time.deltaTime);
        }

        private void HandleGroundCheckAndPhysics() {
            this._isGrounded = Physics.CheckSphere(this._groundCheck.position, this._groundDistanceRadius, this._groundLayerMask);
            if(this._isGrounded && this._characterVelocity.y < 0) this._characterVelocity.y = -2f;
            this._characterVelocity.y += this._attributes.GetGraivityForce() * Time.deltaTime;
            this._characterController.Move(this._characterVelocity * Time.deltaTime);
        }

        private void HandleJump() {
            // Controller can jump using simple physics too. Just use v = sqrRoot(h * -2 * g)
            if (this._isJumping && this._isGrounded) {
                this._characterVelocity.y = Mathf.Sqrt(
                    this._attributes.GetJumpHeight() * -2f * this._attributes.GetGraivityForce()
                );
            }
            _characterController.Move(this._characterVelocity * Time.deltaTime);
        }

        private void Start() {
            this.Initialize();
        }

        private void Update() {
            this.HandleGroundCheckAndPhysics();
            this.HandleMovement();
            this.HandleJump();
        }
    } 
}

