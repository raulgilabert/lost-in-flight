using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    private static readonly int Fade = Animator.StringToHash("Fade");
    
    [SerializeField] private Animator fadeAnimator;
    
    public void OnPressPlay()
    {
        fadeAnimator.SetTrigger(Fade);
    }
}
