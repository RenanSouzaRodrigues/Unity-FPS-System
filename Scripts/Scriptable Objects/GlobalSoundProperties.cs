using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.ScriptableObjects {
    [CreateAssetMenu(fileName = "GlobalSoundProperties", menuName = "FPS/Global Sound Properties")]
    public class GlobalSoundProperties : ScriptableObject
    {
        [SerializeField] [Range(0, 1)] private float _masterVolume = 1f;
        [SerializeField] [Range(0, 1)] private float _musicVolume = 1f;
        [SerializeField] [Range(0, 1)] private float _sfxVolume = 1f;
        [SerializeField] [Range(0, 1)] private float _voiceVolume = 1f;
        [SerializeField] [Range(0, 1)] private float _ambientVolume = 1f;

        public void SetMasterVolume(float volume) {
            this._masterVolume = volume > 1f ? 1f : volume < 0f ? 0f : volume;
        }

        public float GetMasterVolume() {
            return this._masterVolume;
        }

        public void SetMusicVolume(float volume) {
            this._musicVolume = volume > 1f ? 1f : volume < 0f ? 0f : volume;
        }

        public float GetMusicVolume() {
            return this._musicVolume > this._masterVolume ? this._masterVolume : this._musicVolume;
        }

        public void SetSfxVolume(float volume) {
            this._sfxVolume = volume > 1f ? 1f : volume < 0f ? 0f : volume;
        }

        public float GetSfxVolume() {
            return this._sfxVolume > this._masterVolume ? this._masterVolume : this._sfxVolume;
        }

        public void SetVoiceVolume(float volume) {
            this._voiceVolume = volume > 1f ? 1f : volume < 0f ? 0f : volume;
        }

        public float GetVoiceVolume() {
            return this._voiceVolume > this._masterVolume ? this._masterVolume : this._voiceVolume;
        }

        public void SetAmbientVolume(float volume) {
            this._ambientVolume = volume > 1f ? 1f : volume < 0f ? 0f : volume;
        }

        public float GetAmbientVolume() {
            return this._ambientVolume > this._masterVolume ? this._masterVolume : this._ambientVolume;
        }
    }
}
