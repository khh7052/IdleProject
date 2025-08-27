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
    public void SetBool(int paramHash, bool value) => animator?.SetBool(paramHash, value);
    public void SetTrigger(int paramHash) => animator?.SetTrigger(paramHash);
}
