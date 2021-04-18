using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperFunctions
{
    public static Vector3 Lerp(Vector3 start, Vector3 end, float timeStartedLerping, float lerpTime = 3.0f)
    {
        float timeSinceStarted = Time.time - timeStartedLerping;
        float percentageComplete = timeSinceStarted / lerpTime;
        Vector3 result = Vector3.Lerp(start, end, percentageComplete);

        return result;
    }
}
