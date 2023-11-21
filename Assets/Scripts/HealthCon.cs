using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthCon : MonoBehaviour
{

    public float health = 100f;

    public Slider healthSlider;
    // Start is called before the first frame update
    void Start()
    {
        //healthSlider.onValueChanged.AddListener(UpdateHealth);
        
    }

    private void Update()
    {
        healthSlider.value = health;
    }

    // Update is called once per frame
    //public void UpdateHealth(float newHealthValue)
    //{
        // Update the health variable
     //   health = newHealthValue;
    //}
}
