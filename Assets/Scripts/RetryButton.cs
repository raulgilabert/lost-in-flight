using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryButton : MonoBehaviour
{
    private static readonly int FadeOut = Animator.StringToHash("FadeOut");
    
    [SerializeField] private Animator fadeAnimator;
    
    public void OnPressRetry()
    {
        fadeAnimator.SetTrigger(FadeOut);
    }
}
