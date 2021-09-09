using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using FPS.ScriptableObjects;
using TMPro;

namespace FPS.ScriptComponents.Weapons {
    public class RaycastWepon : MonoBehaviour {
        [Header("Define the gun properties")]
        [SerializeField] private GunProperties _gunProperties;

        [Header("Define the Gun UI Elements")]
        [SerializeField] private TextMeshProUGUI _ammoText;
        [SerializeField] private TextMeshProUGUI _maxAmmoText;

        [Header("Define if the gun can auto reload")]
        [SerializeField] private bool _autoReload = true;

        [Header("Define the global sound settings")]
        [SerializeField] private GlobalSoundProperties _globalSoundProperties;

        [Header("Define the raycast Camera position")]
        [SerializeField] private Transform _fpsCameraTransform;

        [Header("Define the wepon ray cast origin and Cross Hair Target")]
        [SerializeField] private Transform _rayCastOrigin;
        [SerializeField] private Transform _rayDestination;

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
                
                this._gunAnimator.Play("ShootAnimation");
                
                this._gunProperties.SetShootState(this._gunProperties.IsAutomatic());

                RaycastHit bulletHitTarget;
                Ray gunRay = new Ray(this._rayCastOrigin.position, this._rayDestination.position - this._rayCastOrigin.position);
                bool hasHitSomething = Physics.Raycast(gunRay,out bulletHitTarget, this._gunProperties.GetGunRange());

                if(hasHitSomething) {
                    //TODO: Implement the bullet hit target and damage apply logic
                    Debug.DrawLine(gunRay.origin, bulletHitTarget.point, Color.red, 5f);
                    Debug.Log(bulletHitTarget.transform.name);

                    if(bulletHitTarget.rigidbody != null) {
                        bulletHitTarget.rigidbody.AddForce(-bulletHitTarget.normal * this._gunProperties.GetGunImpactForce());
                    }
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
            this._gunProperties.SetReloadingState(true);
            this._gunAnimator.Play("ReloadAnimation");
            this.PlayGunReloadSound();
            this._gunProperties.DoReloadMath();
            this._ammoText.text = this._gunProperties.ammoInClip.ToString();
        }

        public void EnableGun() {
            this._gunProperties.SetShootState(true);
            this._gunProperties.SetReloadingState(false);
        }

        public void DisableGun() {
            this._gunProperties.SetShootState(false);
            this._gunProperties.SetReloadingState(true);
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

        private void Start() {
            this.InitializeGunControllerConfig();
        }

        private void Update()
        {
            Ray cameraRay = new Ray(this._fpsCameraTransform.position, this._fpsCameraTransform.forward);
            RaycastHit hit;

            Physics.Raycast(cameraRay, out hit);
            this._rayDestination.position = hit.point;

            float reloadInputValue = this._gunReloadInput.ReadValue<float>();
            if(reloadInputValue > 0 && !this._gunProperties.isReloading()) {
                this.ReloadGun();
            }

            float shootInputValue = this._gunFireInput.ReadValue<float>();
            if(this._gunProperties.IsAutomatic()) {
                if(shootInputValue > 0 && Time.time >= this._gunProperties.GetNextTimeToFire()) {
                    this._gunProperties.SetNextTimeToFire(Time.time + 1f / this._gunProperties.GetGunFireRate()); 
                    this.ShootGun();
                }
            } else if(this._gunProperties.CanShootAgain() && shootInputValue > 0) {
                this.ShootGun();
            }
        }
    }

}
