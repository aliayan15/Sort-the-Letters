using UnityEngine;

public static class AnimatorExtansion
{
    public static void ResetAllAnimatorTriggers(this Animator animator)
    {
        foreach (var trigger in animator.parameters)
        {
            if (trigger.type == AnimatorControllerParameterType.Trigger)
            {
                animator.ResetTrigger(trigger.name);
            }
        }
    }
}
