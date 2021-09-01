using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.Common {
   public class PersonalSoundManager : MonoBehaviour {
        [Header("Define the Personal Audio Source component")]
        [SerializeField] private bool _autoDetectPersonalAudioSource = true;
        [SerializeField] private AudioSource _personalAudioSource;

        [Header("Define player general sounds. Dynamic sounds are not yet implemented.")]
        [SerializeField] private List<AudioClip> _listOfJumpSounds;
        [SerializeField] private List<AudioClip> _listOfPainSounds;
        [SerializeField] private List<AudioClip> _listOfDeathSounds;

        public void PlayJumpSound() { 
            if(!this._personalAudioSource.isPlaying) {
                this._personalAudioSource.clip = this._listOfJumpSounds[Random.Range(0, this._listOfJumpSounds.Count)];
                this._personalAudioSource.loop = false;
                this._personalAudioSource.Play();
            }
        }

        public void PlayPainSound() {
            if(!this._personalAudioSource.isPlaying) {
                this._personalAudioSource.clip = this._listOfPainSounds[Random.Range(0, this._listOfPainSounds.Count)];
                this._personalAudioSource.loop = false;
                this._personalAudioSource.Play();
            }
        }

        public void PlayDeathSound() {
            if(!this._personalAudioSource.isPlaying) {
                this._personalAudioSource.clip = this._listOfDeathSounds[Random.Range(0, this._listOfDeathSounds.Count)];
                this._personalAudioSource.loop = false;
                this._personalAudioSource.Play();
            }
        }

        private void Start() {
            if (this._autoDetectPersonalAudioSource) {
                this._personalAudioSource = this.GetComponent<AudioSource>();
            }    
        }
    } 
}

