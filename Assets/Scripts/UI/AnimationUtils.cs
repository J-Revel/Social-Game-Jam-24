using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
public static class AnimationUtils
{
    public static IEnumerator DoCoroutine(Action<float> progressiveAction, AnimationConfig animationConfig, Action callback = null)
     => DoCoroutine(progressiveAction, animationConfig.Duration, value => animationConfig.Ease.Evaluate(value),animationConfig.Delay, callback);
    public static IEnumerator DoCoroutine(Action<float> progressiveAction, float duration, Func<float,float> easingFunction, float delay = 0, Action callback = null)
    {
        if(delay>0) yield return new WaitForSeconds(delay);

        float animationTime = 0f;
        while(animationTime < duration)
        {
            float timeRatio = animationTime / duration;
            progressiveAction.Invoke(easingFunction(timeRatio));
            
            yield return null;
            animationTime += Time.deltaTime;
        }

        // Apply last frame value
        progressiveAction.Invoke(1f);
        callback?.Invoke();
    }

    public static Color WithAlpha(this Color color, float alpha)
    {
        return new Color(color.r, color.g, color.b, alpha);
    }
}