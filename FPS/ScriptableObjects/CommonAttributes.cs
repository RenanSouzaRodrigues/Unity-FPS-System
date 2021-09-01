using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.ScriptableObjects {
    [CreateAssetMenu(fileName = "CommonAttributes", menuName = "FPS / Common Attributes")]
    public class CommonAttributes : ScriptableObject {
        [SerializeField] private bool _isDead = false;
        [SerializeField] private float _health = 100f;
        [SerializeField] private float _maxHealth = 100f;
        [SerializeField] private float _armor = 0f;
        [SerializeField] private float _maxArmor = 100f;
        [SerializeField] private float _stamina = 100f;
        [SerializeField] private float _maxStamina = 100f;
        [SerializeField] private float _movementSpeed = 7.0f;
        [SerializeField] private float _runningMultiplier = 2f;
        [SerializeField] private float _gravityForce = -10f;
        [SerializeField] private float _jumpHeight = 1f;

        public bool EntityIsDead() {
            return this._isDead;
        }

        public float GetHealth() {
            return this._health;
        }

        public void AddHealth(float amount) {
            this._health = amount;
        }

        private void SubtractHealth(float amount) {
            this._health -= amount;
            this._isDead = this._health <= 0f;
        }
        
        public void ApplyDamage(float amountOfDamage) {
            if (this._armor > 0) {
                float amountOfDamageLeft = amountOfDamage - this._armor < 0 ? 0 : amountOfDamage - this._armor;
                this.SubtractHealth(amountOfDamageLeft);
            } else {
                this.SubtractHealth(amountOfDamage);
            }
        }

        public void ApplyCriticalDamage(float amountOfDamage, float criticalFactor) {
            this.ApplyDamage(amountOfDamage * criticalFactor);
        }

        public void IncreaseMaxHealth(float amount) {
            this._maxHealth += Mathf.Round(amount);
        }

        public void DecreaseMaxHealth(float amount) {
            this._maxHealth -= Mathf.Round(amount);
            if(this._health > this._maxHealth) this._health = this._maxHealth;
        }

        public void AddArmor(float amount) { 
            this._armor = amount;
        }

        public void IncreaseMaxArmor(float amount) {
            this._maxArmor += Mathf.Round(amount);
        }

        public void DecreaseMaxArmor(float amount) {
            this._maxArmor -= Mathf.Round(amount);
            if(this._armor > this._maxArmor) this._armor = this._maxArmor;
        }

        public void IncreaseMaxStamina(float amount) {
            this._maxStamina += Mathf.Round(amount);
        }

        public void DecreaseMaxStamina(float amount) {
            this._maxStamina -= Mathf.Round(amount);
            if(this._stamina > this._maxStamina) this._stamina = this._maxStamina;
        }

        public void AddStamina(float amount) {
            this._stamina = amount;
        }

        public float GetRunningMultiplier() {
            return this._runningMultiplier;
        }

        public float GetMovementSpeed() {
            return this._movementSpeed;
        }

        public float GetGraivityForce() {
            return this._gravityForce;
        }

        public float GetJumpHeight() {
            return this._jumpHeight;
        }
    }
}

