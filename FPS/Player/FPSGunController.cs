using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using FPS.ScriptableObjects;
using TMPro;

namespace FPS.Player {
    public class FPSGunController : MonoBehaviour {
        [Header("Define the gun properties")]
        [SerializeField] private GunProperties _gunProperties;

        [Header("Define the Debug properties")]
        [SerializeField] private bool _showRaycastDebug;
        [SerializeField] private bool _showGunDebug;

        [Header("Define the Gun UI Elements")]
        [SerializeField] private TextMeshProUGUI _ammoText;
        [SerializeField] private TextMeshProUGUI _maxAmmoText;

        [Header("Define if the gun can auto reload")]
        [SerializeField] private bool _autoReload = true;

        [Header("Define the global sound settings")]
        [SerializeField] private GlobalSoundProperties _globalSoundProperties;

        [Header("Define the player controller camera object")]
        [SerializeField] private Camera _fpsCamera;

        [Header("Define the gun input system")]
        [SerializeField] private InputAction _gunFireInput;
        [SerializeField] private InputAction _gunReloadInput;

        [Header("Define the gun Muzze flash")]
        [SerializeField] private ParticleSystem _muzzeFlash;

        [Header("Define the gun sounds")]
        [SerializeField] private AudioSource _gunAudioSource;
        [SerializeField] private AudioSource _gunReloadAudioSource;
        [SerializeField] private List<AudioClip> _gunShootSounds;
        [SerializeField] private List<AudioClip> _gunReloadSounds;
        [SerializeField] private List<AudioClip> _gunEmptyClipSounds;

        [Header("Define the Pistol Animator")]
        [SerializeField] private Animator _gunAnimator;

        private void InitializeGunControllerConfig() {
            this._gunFireInput.Enable();
            this._gunReloadInput.Enable();
            this.EnableGun();
        }

        private void ShootGun() {
            if(this._gunProperties.ammoInClip > 0) {
                this._gunProperties.ammoInClip--;

                this._ammoText.text = this._gunProperties.ammoInClip.ToString();

                this._muzzeFlash.Play();
                
                this.PlayGunShootSound();
                
                this._gunAnimator.Play("ShotAnimation");
                
                if(!this._gunProperties.isAutomatic) this._gunProperties.canShootAgain = false;

                RaycastHit bulletHitTarget;
                bool hasHitSomething = Physics.Raycast( this._fpsCamera.transform.position, 
                                                        this._fpsCamera.transform.forward, 
                                                        out bulletHitTarget,
                                                        this._gunProperties.GetGunRange() );

                if(hasHitSomething) {
                    //TODO: Implement the bullet hit target and damage apply logic
                    Debug.Log(bulletHitTarget.transform.name);
                } else {
                    Debug.Log("No hit");
                }
            }
            else if(this._autoReload) {
                this.ReloadGun();
            } else {
                // this.PlayGunEmptyClipSound();
            }
        }

        private void ReloadGun() {
            this._gunProperties.isReloading = true;
            this._gunAnimator.Play("ReloadAnimation");
            this.PlayGunReloadSound();
            this._gunProperties.DoReloadMath();
            this._ammoText.text = this._gunProperties.ammoInClip.ToString();
        }

        public void EnableGun() {
            this._gunProperties.canShootAgain = true;
            this._gunProperties.isReloading = false;
        }

        public void DisableGun() {
            this._gunProperties.canShootAgain = false;
            this._gunProperties.isReloading = true;
        }

        private void PlayGunShootSound() {
            this._gunAudioSource.clip = this._gunShootSounds[Random.Range(0, this._gunShootSounds.Count)];
            this._gunAudioSource.loop = false;
            this._gunAudioSource.volume = this._globalSoundProperties.GetSfxVolume();
            this._gunAudioSource.Play();
        }

        private void PlayGunReloadSound() {
            this._gunReloadAudioSource.clip = this._gunReloadSounds[Random.Range(0, this._gunReloadSounds.Count)];
            this._gunReloadAudioSource.loop = false;
            this._gunReloadAudioSource.volume = this._globalSoundProperties.GetSfxVolume();
            this._gunReloadAudioSource.Play();
        }

        // private void PlayGunEmptyClipSound() {
        //     this._gunReloadAudioSource.clip = this._gunEmptyClipSounds[Random.Range(0, this._gunEmptyClipSounds.Count)];
        //     this._gunReloadAudioSource.loop = false;
        //     this._gunReloadAudioSource.volume = this._globalSoundProperties.GetSfxVolume();
        //     this._gunReloadAudioSource.Play();
        // }

        private void Start() {
            this.InitializeGunControllerConfig();
        }

        private void Update()
        {
            float reloadInputValue = this._gunReloadInput.ReadValue<float>();
            if(reloadInputValue > 0 && !this._gunProperties.isReloading) {
                this.ReloadGun();
            }

            float shootInputValue = this._gunFireInput.ReadValue<float>();
            Debug.Log(shootInputValue);
            if(this._gunProperties.isAutomatic) {
                if(shootInputValue > 0 && Time.time >= this._gunProperties.nextTimeToFire) {
                    this._gunProperties.nextTimeToFire = Time.time + 1f / this._gunProperties.GetGunFireRate(); 
                    this.ShootGun();
                }
            } else if(this._gunProperties.canShootAgain && shootInputValue > 0) {
                this.ShootGun();
            }
        }
    }

}
