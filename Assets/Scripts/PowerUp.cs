using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{
    public GameObject Gun;
    public Slider HealthSlider;
    
    public bool Mag;
    public bool Health;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && Mag)
        {
            Debug.Log("Magsize");
            Gun.GetComponent<GunShoot>().MagSize++;
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Player") && Health)
        {
            Debug.Log("Health");
            Debug.Log(HealthSlider.GetComponent<HealthCon>().health);
            HealthSlider.GetComponent<HealthCon>().health += 10;
            Debug.Log(HealthSlider.GetComponent<HealthCon>().health);
            Destroy(gameObject);
        }

        
    }
}
