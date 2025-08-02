using System.Collections.Generic;
using UnityEngine;

namespace Fantasy.Utilities
{
    public static class AnimatorExtensions
    {
        public static void OverrideAnimationClip(this Animator animator, AnimatorOverrideController overrideController,
            AnimationClip animationToReplace, AnimationClip newAnimationClip)
        {
            List<KeyValuePair<AnimationClip, AnimationClip>> overrides = new List<KeyValuePair<AnimationClip, AnimationClip>>();
            
            overrideController.GetOverrides(overrides);

            for (int i = 0; i < overrides.Count; i++)
            {
                if (overrides[i].Key == animationToReplace)
                {
                    overrides[i] = new KeyValuePair<AnimationClip, AnimationClip>(overrides[i].Key, newAnimationClip);
                }
            }

            overrideController.ApplyOverrides(overrides);
            
            animator.runtimeAnimatorController = overrideController;
        }
    }
}
