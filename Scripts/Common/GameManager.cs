using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FPS.ScriptableObjects;

namespace FPS.Common {
    public class GameManager : MonoBehaviour {
        [Header("Define all the game HUDS")]
        [SerializeField] private Canvas _playerHUD;
        [SerializeField] private Canvas _menuHUD;
        private Dictionary<string, Canvas> _gameCanvas = new Dictionary<string, Canvas>();

        [Header("Define the Global Sound Settings")]
        [SerializeField] private GlobalSoundProperties _globalSoundProperties;

        [Header("Define the game manager sound manager")]
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _mapBGMSong;

        // ------------ HUD METHODS ------------
        private void SetGameCanvas() {
            this._gameCanvas.Add("PlayerHUD", this._playerHUD);
            this._gameCanvas.Add("MenuHUD", this._menuHUD);
        }

        public void ChangeScene(string sceneName, bool loadAsync = false) {
            if (SceneManager.GetActiveScene().name == sceneName) return;

            if (loadAsync) {
                SceneManager.LoadSceneAsync(sceneName);
            } else {
                SceneManager.LoadScene(sceneName);
            }
        }

        public void ShowHUD(string hudName) {
            if (this._gameCanvas.ContainsKey(hudName)) {
                foreach (KeyValuePair<string, Canvas> dictionaryEntry in this._gameCanvas) {
                    dictionaryEntry.Value.enabled = false;
                }
                this._gameCanvas[hudName].enabled = true;
            }
        }

        public void PlayMapBGM() {
            this._audioSource.clip = this._mapBGMSong;
            this._audioSource.loop = true;
            this._audioSource.volume = this._globalSoundProperties.GetMusicVolume();
            this._audioSource.Play();
        }

        private void Start() {
            this.PlayMapBGM();
        }
    }
}

