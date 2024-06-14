using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class DifficultySlider : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI textMesh;
    public float[] thresholds;
    public string[] difficultyNames;
    void Update()
    {
        int highestThreshold = 0;
        for (int i = 0; i < thresholds.Length; i++)
        { 
            if(slider.value > thresholds[i])
            {
                highestThreshold = i;
            }
        }
        textMesh.text = $"Difficulty: {difficultyNames[highestThreshold]}";

    }
}
