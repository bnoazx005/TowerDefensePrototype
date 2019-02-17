using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public static class AnimationUtils
{
    public delegate void AnimationAction(float t);

    /// <summary>
    /// The method provides a simple mechanism for code based animations
    /// </summary>
    /// <param name="duration">Animation's duration</param>
    /// <param name="action">An action should be executed</param>
    /// <returns>A reference to enumerator</returns>

    public static IEnumerator Animate(float duration, AnimationAction action, UnityAction postAction = null)
    {
        if (action == null)
        {
            yield break;
        }

        float dt = 0.0f;

        for (float time = 0.0f; Mathf.Abs(time - duration) > (dt = Time.deltaTime); time += dt)
        {
            action?.Invoke(time);

            yield return null;
        }

        postAction?.Invoke();

        yield return null;
    }
}
