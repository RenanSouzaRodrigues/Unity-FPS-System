using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.ScriptableObjects {
    [CreateAssetMenu(fileName = "GunProperties", menuName = "FPS/Gun Properties", order = 1)]
    public class GunProperties : ScriptableObject {
        [SerializeField] private string _gunName;
        [SerializeField] private float _gunFireRate = 2f;
        [SerializeField] private float _gunMinimalDamage = 10f;
        [SerializeField] private float _gunMaximalDamage = 15f;
        [SerializeField] private float _gunCriticalMultiplier = 1.3f;
        [SerializeField] private float _gunRange = 100f;
        [SerializeField] private float _gunImpactForce = 100f;
        [SerializeField] private bool _isAutomatic = false;
        [SerializeField] private float _nextTimeToFire = 0f;
        [SerializeField] private bool _canShootAgain = true;
        [SerializeField] private int _ammoInCarry = 60;
        [SerializeField] private int _maxAmmoCarryCapacity = 60;
        [SerializeField] public int ammoInClip = 0;
        [SerializeField] private int _maxAmmoInClip = 12;
        [SerializeField] private bool _isReloading = false;

        public string GetName() {
            return this._gunName;
        }

        public float GetGunFireRate() {
            return this._gunFireRate;
        }

        public float GetGunDamage() {
            return Random.Range(this._gunMinimalDamage, this._gunMaximalDamage);
        }

        public float GetGunCriticalMultiplier() {
            return this._gunCriticalMultiplier;
        }

        public float GetGunRange() {
            return this._gunRange;
        }

        public float GetGunImpactForce() {
            return this._gunImpactForce;
        }

        public bool IsAutomatic() {
            return this._isAutomatic;
        }

        public float GetNextTimeToFire() {
            return this._nextTimeToFire;
        }

        public void SetNextTimeToFire(float time) {
            this._nextTimeToFire = time;
        }

        public bool CanShootAgain() {
            return this._canShootAgain;
        }

        public void SetShootState(bool state) {
            this._canShootAgain = state;
        }

        public float GetAmmoInCarryCapacity() {
            return this._ammoInCarry;
        }

        public float GetMaxAmmoCarryCapacity() {
            return this._maxAmmoCarryCapacity;
        }

        public void DoReloadMath() {
            this.ammoInClip = this._maxAmmoInClip;
        }

        public bool isReloading() {
            return this._isReloading;
        }

        public void SetReloadingState(bool state) {
            this._isReloading = state;
        }
    }
}

