using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FPS {
    public class GameManager : MonoBehaviour {
        [SerializeField] private float _skyboxRotationSpeed = 1f;
        [SerializeField] private Canvas _playerHUD;
        [SerializeField] private Canvas _menuHUD;
        private Dictionary<string, Canvas> _gameCanvas = new Dictionary<string, Canvas>();

        // For each new canvas added to the game, add it to the list in order to be able to hide/show them
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

        public void RenderSkyboxRotation() {
            RenderSettings.skybox.SetFloat("_Rotation", Time.time * _skyboxRotationSpeed);
        }

        void Update() {
            this.RenderSkyboxRotation();
        }

        public void ShowHUD(string hudName) {
            if (this._gameCanvas.ContainsKey(hudName)) {
                foreach (KeyValuePair<string, Canvas> dictionaryEntry in this._gameCanvas) {
                    dictionaryEntry.Value.enabled = false;
                }
                this._gameCanvas[hudName].enabled = true;
            }
        }
    }
}

