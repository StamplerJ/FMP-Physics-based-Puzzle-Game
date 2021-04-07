using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Color glowColor;
    
    private new Renderer renderer;
    private ParticleSystem.MainModule particleSettings;

    private float shaderRotation;
    
    private void Awake()
    {
        particleSettings = GetComponentInChildren<ParticleSystem>().main;
        particleSettings.startColor = new ParticleSystem.MinMaxGradient(glowColor);
        
        renderer = GetComponentInChildren<Renderer>();
        renderer.material.SetColor("_Color", glowColor);
    }

    void Update()
    {
        RotateShader();
    }

    private void RotateShader()
    {
        renderer.material.SetFloat("_Angle", shaderRotation);
        
        shaderRotation += Time.deltaTime;
        if (shaderRotation > 4.0f)
        {
            shaderRotation = 0f;
        }
    }
}
