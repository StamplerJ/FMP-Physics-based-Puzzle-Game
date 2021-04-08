using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowingTextureRotation : MonoBehaviour
{
    [SerializeField] private Color glowColor;
    
    private new Renderer renderer;
    private ParticleSystem.MainModule particleSettings;

    private float shaderRotation;
    
    private void Awake()
    {
        particleSettings = GetComponentInChildren<ParticleSystem>().main;
        renderer = GetComponentInChildren<Renderer>();
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

    public Color GlowColor
    {
        get => glowColor;
        set
        {
            glowColor = value;
            particleSettings.startColor = new ParticleSystem.MinMaxGradient(glowColor);
            renderer.material.SetColor("_Color", glowColor);
        }
    }
}
