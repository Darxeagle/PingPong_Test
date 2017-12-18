using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Common.Scripts
{
    public class Preloader : MonoBehaviour
    {
        private const float IMAGE_ROTATION_SPEED = 100f;
        private const float IMAGE_FADE_SPEED = 2f;

        [SerializeField] private string sceneName = "ApplicationTemplate";
        [SerializeField] private Transform imageTransform;
        [SerializeField] private CanvasGroup imageCanvas;

        private bool _sceneLoaded = false;

        // Use this for initialization
        void Start ()
        {
            imageCanvas.alpha = 0f;
            LoadScene();
        }

        void Update()
        {
            imageTransform.transform.Rotate(Vector3.forward, Time.deltaTime * IMAGE_ROTATION_SPEED);

            if (!_sceneLoaded && imageCanvas.alpha < 1)
            {
                imageCanvas.alpha += Time.deltaTime * IMAGE_FADE_SPEED;
            }
            else if (_sceneLoaded && imageCanvas.alpha > 0f)
            {
                imageCanvas.alpha -= Time.deltaTime * IMAGE_FADE_SPEED;
            }
            else if (_sceneLoaded && imageCanvas.alpha <= 0f)
            {
                SwitchScene();
            }
        }

        private void LoadScene()
        {
            StartCoroutine(LoadSceneAsync());
        }

        IEnumerator LoadSceneAsync()
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            //asyncLoad.allowSceneActivation = false;
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            _sceneLoaded = true;
        }

        private void SwitchScene()
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
            SceneManager.UnloadSceneAsync("Preloader");
        }
    }
}
