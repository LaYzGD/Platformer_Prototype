using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public event Action OnAnimationStarted;
    public event Action OnAnimationTriggered;
    public event Action OnAnimationCompleted;

    public void ChangeAnimationState(string triggerName) => _animator.SetTrigger(triggerName);
    public void ChangeAnimationState(string transitionName, bool value) => _animator.SetBool(transitionName, value);
    public void SetController(AnimatorOverrideController controller)
    {
        _animator.runtimeAnimatorController = controller;
    }
    public void AnimationTriggered() => OnAnimationTriggered?.Invoke();
    public void AnimationStarted() => OnAnimationStarted?.Invoke();
    public void AnimationCompleted() => OnAnimationCompleted?.Invoke();
}
