using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FPS {
    public class GameManager : MonoBehaviour {
        [SerializeField] private float _skyboxRotationSpeed = 1f;
        [SerializeField] private Canvas _playerHUD;

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
    }
}

