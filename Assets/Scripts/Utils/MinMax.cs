using System;
using UnityEngine;
[Serializable]
public class MinMax
{
    public float min = 0;
    public float max = 0;
    public float GetValue(float t = 0)
    {
        return Mathf.Lerp(min, max, t);
    }
    public float Clamp(float value)
    {
        return Mathf.Clamp(value, min, max);
    }
    public float GetRandomValue()
    {
        return GetValue(UnityEngine.Random.value);
    }
}