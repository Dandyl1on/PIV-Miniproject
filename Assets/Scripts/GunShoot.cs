using System;
using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GunShoot : MonoBehaviour
{

    public int damage;


    public Camera FPScam;


    
    public int MagSize;
    public int bulletsLeft;
    public bool reloading;
    public Transform GunPoint;
    
    public TextMeshProUGUI AmmoCount;

    public Animator gun;

    public ParticleSystem Flash;
    public ParticleSystem Glow;
    public ParticleSystem Sparks;
    public GameObject Impact;
    
    public float delay = 1.0f;

    private void Awake()
    {
        bulletsLeft = MagSize;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !reloading && bulletsLeft > 0)
        {
            Shoot();
        }
        if (Input.GetKey(KeyCode.R) && bulletsLeft < MagSize && !reloading)
        {
            StartCoroutine(Reload());
        }
        
        if (AmmoCount != null)
        {
            AmmoCount.SetText("Ammo: " + bulletsLeft +  " / " + MagSize);
        }
    }
    
    
    public void Shoot()
    {
        
        Flash.Play();
        Glow.Play();
        Sparks.Play();
        RaycastHit pew;
        if (Physics.Raycast(GunPoint.position, transform.TransformDirection(Vector3.forward), out pew, 20))
        {
            
            //Impact.Play();
            Instantiate(Impact, pew.point, Quaternion.LookRotation(pew.normal));
            delay -= Time.deltaTime;
            
            Target target = pew.transform.GetComponent<Target>();

            if (target != null)
            {
                target.takeDamage(damage);
                
            }
            
            Vector3 targetPoint;
            Ray ray = FPScam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
            if (Physics.Raycast(ray, out pew))
            {
                targetPoint = pew.point;
            }
            else
            {
                targetPoint = ray.GetPoint(50);
            }

         
        }
        
        bulletsLeft--;
        AmmoCount.SetText( "Ammo: " + bulletsLeft + "/" + MagSize);
    }

  
    IEnumerator Reload()
    {
        gun.SetBool("Reloading", true);
        reloading = true;
        yield return new WaitForSeconds(1);
        reloading = false;
        bulletsLeft = MagSize;
        gun.SetBool("Reloading", false);
    }
}
