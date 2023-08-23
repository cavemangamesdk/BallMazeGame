using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CMG.BallMazeGame
{
    public class StartScene : MonoBehaviour
    {
        private bool _loadScene = false;
        private bool _sceneLoaded = false;
        
        private void Awake()
        {
            SceneManager.sceneLoaded += (arg0, mode) => { _sceneLoaded = true; };
            SceneManager.LoadSceneAsync(1);
        }

        void Update()
        {
            if (Input.anyKey)
            {
                Debug.Log("Change scene!");
                _loadScene = true;
            }

            LoadMainScene();
        }

        private void LoadMainScene()
        {
            if (_loadScene == false) return;
            if (_sceneLoaded == false) return;

            SceneManager.LoadScene(1);
        }
    }
}