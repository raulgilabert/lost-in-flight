using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class LoadSceneOnFadeOut : MonoBehaviour
    {
        [SerializeField] private int sceneToLoad;

        private void Start()
        {
            GetComponent<Fade>().onFadeOut.AddListener(OnFadeOut);
        }

        private void OnFadeOut()
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}