using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace WEI
{
    public class LevelLoader : MonoBehaviour
    {
        public GameObject loadingScreen;
        public Slider slider;

        public void LoadLevel(int sceneIndex)
        {
            StartCoroutine(LoadAsynchromously(sceneIndex));
        }
        IEnumerator LoadAsynchromously(int sceneIndex)
        { 
            AsyncOperation operation =  SceneManager.LoadSceneAsync(sceneIndex);

            loadingScreen.SetActive(true);

            while (!operation.isDone)
            {
                float propress = Mathf.Clamp01(operation.progress / 0.9f);

                slider.value = propress;

                yield return null;
            }
        }
    }
}