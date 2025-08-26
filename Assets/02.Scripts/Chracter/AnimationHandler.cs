using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Awake()
    {
        if(animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    public void PlayIdle() => animator?.Play(AnimatorHash.IdleHash);
    public void PlayChase() => animator?.Play(AnimatorHash.ChaseHash);
}
