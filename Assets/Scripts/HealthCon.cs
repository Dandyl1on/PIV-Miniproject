using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthCon : MonoBehaviour
{
    
    public float health = 100f;
    
    public Slider healthSlider;
   
    private void Update()
    {
        healthSlider.value = health;
    }
    
    public void TakeDamage(float Damage)
    {
        health -= Time.deltaTime * Damage;
    }
    
}
