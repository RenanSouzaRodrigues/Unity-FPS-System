using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FPS.ScriptableObjects;
using UnityEngine.UI;

namespace FPS.Common {
    public class GameManager : MonoBehaviour {
        [Header("Define all the game HUDS")]
        [SerializeField] private Canvas _playerHUD;
        [SerializeField] private Canvas _menuHUD;
        private Dictionary<string, Canvas> _gameCanvas = new Dictionary<string, Canvas>();

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
    }
}

