using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickMath : MonoBehaviour
{
    public static float Clamp0360(float eulerAngles)
    {
        float result = eulerAngles - Mathf.CeilToInt(eulerAngles / 360f) * 360f;
        if (result < 0)
        {
            result += 360f;
        }
        return result;
    }

    public static float TwoPointGetAngle(Vector3 p1, Vector3 p2)
    {
        return Mathf.Atan2(p2.y - p1.y, p2.x - p1.x) * 180 / Mathf.PI;
    }
}
