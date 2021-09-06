using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FPS.ScriptComponents.Character {
    [RequireComponent(typeof(CharacterMovement))]
    public class CharacterRun : MonoBehaviour {
        [Header("Define the run input")]
        [SerializeField] private InputAction _runInput;

        public event EventHandler<bool> OnRun;

        private void Start() {
            this._runInput.Enable();
        }

        private void Update() {
            this.OnRun?.Invoke(this, this._runInput.ReadValue<float>() > 0);
        }
    }
}

