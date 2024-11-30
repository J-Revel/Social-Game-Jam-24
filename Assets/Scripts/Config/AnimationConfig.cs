using System;
using UnityEngine;
using UnityEngine.Analytics;

[CreateAssetMenu(fileName = "AnimationConfig", menuName = "Scriptable Objects/AnimationConfig")]
public class AnimationConfig : ScriptableObject
{
    public float Delay = 0f;
    public float Intensity = 1f;
    public float Duration = 1f;
    public AnimationCurve Ease;
}
