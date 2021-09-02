using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FPS.Player {
    public class FPSGunController : MonoBehaviour {
        [Header("Define the player controller camera object")]
        [SerializeField] private Camera _fpsCamera;

        [Header("Define the gun input system")]
        [SerializeField] private InputAction _gunFireInput;

        [Header("Define the gun Muzze flash")]
        [SerializeField] private ParticleSystem _muzzeFlash;

        [Header("Define the gun properties")]
        [SerializeField] private float _gunFireRate = 2f;
        [SerializeField] private float _gunDamage = 10f;
        [SerializeField] private float _gunCriticalMultiplier = 1.3f;
        [SerializeField] private float _gunRange = 100f;
        [SerializeField] private float _gunImpactForce = 100f;
        [SerializeField] private bool _gunIsAutomatic = false;
        [SerializeField] private float _nextTimeToFire = 0f;
        [SerializeField] private bool _gunCanShootAgain = true;
        [SerializeField] private int _ammoInCarryCapacity = 60;
        [SerializeField] private int _maxAmmoCarryCapacity = 60;
        [SerializeField] private int _ammoInClip = 0;
        [SerializeField] private int _maxAmmoInClip = 12;

        [Header("Define the gun sounds")]
        [SerializeField] private AudioSource _gunAudioSource;
        [SerializeField] private List<AudioClip> _gunShotSounds;
        [SerializeField] private List<AudioClip> _gunReloadSounds;

        [Header("Define the Pistol Animator")]
        [SerializeField] private Animator _gunAnimator;

        private void InitializeGunControllerConfig() {
            this._gunFireInput.Enable();
        }

        private void ShootGun() {
            if(this._ammoInClip > 0) {
                this._ammoInClip--;

                this._muzzeFlash.Play();
            
                this._gunAudioSource.clip = this._gunShotSounds[Random.Range(0, this._gunShotSounds.Count)];
                this._gunAudioSource.loop = false;
                this._gunAudioSource.Play();

                this._gunAnimator.Play("ShotAnimation");

                RaycastHit bulletHitTarget;
                bool hasHitSomething = Physics.Raycast(
                    this._fpsCamera.transform.position, 
                    this._fpsCamera.transform.forward, 
                    out bulletHitTarget,
                    this._gunRange
                );

                if(hasHitSomething) {
                    Debug.Log(bulletHitTarget.transform.name);
                } else {
                    Debug.Log("No hit");
                }
            }
            else {
                this.ReloadGun();
            }
            
        }

        private void ReloadGun() {
            this._gunAudioSource.clip = this._gunReloadSounds[Random.Range(0, this._gunReloadSounds.Count)];
            this._gunAudioSource.loop = false;
            this._gunAudioSource.Play();

            this._gunAnimator.Play("ReloadAnimation");

            this._ammoInClip = this._maxAmmoInClip;
        }

        private void Start() {
            this.InitializeGunControllerConfig();
        }

        private void Update()
        {
            float shootInputValue = this._gunFireInput.ReadValue<float>();
            if(!this._gunIsAutomatic && !this._gunCanShootAgain && shootInputValue <= 0f) {
                this._gunCanShootAgain = true;
            }

            if(this._gunIsAutomatic) {
                if(shootInputValue > 0 && Time.time >= this._nextTimeToFire) {
                    this._nextTimeToFire = Time.time + 1f / this._gunFireRate; 
                    this.ShootGun();
                }
            } 
            
            if(!this._gunIsAutomatic && this._gunCanShootAgain && shootInputValue > 0) {
                this._gunCanShootAgain = false;
                this.ShootGun();
            }
        }
    }

}
