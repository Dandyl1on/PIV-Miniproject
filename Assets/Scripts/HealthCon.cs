using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class HealthCon : MonoBehaviour
{
    
    public float health = 100f;
    
    public Slider healthSlider;

    public TextMeshProUGUI Deathtext;

    private void Start()
    {
        Deathtext.enabled = false;
    }

    private void Update()
    {
        healthSlider.value = health;
        if (healthSlider.value == 0)
        {
            Deathtext.enabled = true;
            Time.timeScale = 0;
        }
    }
    
    public void TakeDamage(float Damage)
    {
        health -= Time.deltaTime * Damage;
    }
    
}
