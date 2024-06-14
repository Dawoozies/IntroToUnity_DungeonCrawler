using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DifficultyChange : MonoBehaviour
{
    public Vector2 defaultAngle;
    public Vector2 easyAngle;
    public float defaultIntensity;
    public float easyIntensity;
    public float defaultRange;
    public float easyRange;
    public Light spotLight;
    public void ChangeDifficulty(float difficulty)
    {
        spotLight.innerSpotAngle = Mathf.Lerp(defaultAngle.x, easyAngle.x, difficulty);
        spotLight.spotAngle = Mathf.Lerp(defaultAngle.y, easyAngle.y, difficulty);
        spotLight.intensity = Mathf.Lerp(defaultIntensity, easyIntensity, difficulty);
        spotLight.range = Mathf.Lerp(defaultRange, easyRange, difficulty);
    }
}
