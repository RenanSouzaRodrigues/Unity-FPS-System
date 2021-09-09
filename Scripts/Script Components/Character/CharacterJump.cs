using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FPS.ScriptComponents.Character {
    public class CharacterJump : MonoBehaviour
    {
        [Header("Define the jump input")]
        [SerializeField] private InputAction _jumpInput;

        public event EventHandler<bool> OnJump;

         private void Start() {
            this._jumpInput.Enable();
        }

        private void Update() {
            this.OnJump?.Invoke(this, this._jumpInput.ReadValue<float>() > 0);
        }
    }
}

