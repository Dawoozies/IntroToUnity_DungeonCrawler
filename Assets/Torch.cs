using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Torch : MonoBehaviour
{
    private Light light;
    public AnimationCurve intensityMultiplierCurve;
    private float t;
    private float defaultIntensity;
    public float shadowStrengthVariation;
    private float defaultShadowStrength;
    public float shadowNearPlaneVariation;
    private float defaultShadowNearPlane;
    public float frequencySpeed;
    private float endTime => intensityMultiplierCurve.keys[Index.FromEnd(1)].time;
    public float smoothingTime;
    private float vel_intensity;
    private float vel_shadowStrength;
    private float vel_shadowNearPlane;
    void Start()
    {
        light = GetComponent<Light>();
        defaultIntensity = light.intensity;
        defaultShadowStrength = light.shadowStrength;
        defaultShadowNearPlane = light.shadowNearPlane;
    }
    void Update()
    {
        if (t < intensityMultiplierCurve.keys[Index.FromEnd(1)].time)
        {
            t += Time.deltaTime*frequencySpeed;
        }
        else
        {
            t = Random.Range(0f, endTime);
        }

        light.intensity = Mathf.SmoothDamp(light.intensity, defaultIntensity*intensityMultiplierCurve.Evaluate(t), ref vel_intensity, smoothingTime);
        light.shadowStrength = Mathf.SmoothDamp(light.shadowStrength, (defaultShadowStrength*shadowStrengthVariation*intensityMultiplierCurve.Evaluate(t)), ref vel_shadowStrength, smoothingTime);
        
        light.shadowNearPlane = Mathf.SmoothDamp(light.shadowNearPlane, (defaultShadowNearPlane - shadowNearPlaneVariation*intensityMultiplierCurve.Evaluate(t)), ref vel_shadowNearPlane, smoothingTime);
    }
}
