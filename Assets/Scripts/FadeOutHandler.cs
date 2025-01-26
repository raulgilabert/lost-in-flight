using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeOutHandler : MonoBehaviour
{
    [SerializeField] private int sceneToLoad;

    public void OnFadeEnd()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
