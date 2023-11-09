using System;
using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GunShoot : MonoBehaviour
{

    public int damage = 10;

    public float Range = 10f;

    public Camera FPScam;

    public float ImpactForce = 30f;


    public GameObject Bullet;
    public float ShootForce;
    public int MagSize;
    public int bulletsLeft;
    public bool reloading;
    public Transform GunPoint;
    
    public TextMeshProUGUI AmmoCount;

    public Animator gun;

    public ParticleSystem Flash;
    public GameObject Impact;
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
            Debug.Log("Reload?");
        }
        
        if (AmmoCount != null)
        {
            AmmoCount.SetText(bulletsLeft +  " / " + MagSize);
        }
    }
    
    
    public void Shoot()
    {
        
        Flash.Play();
        RaycastHit pew;
        if (Physics.Raycast(GunPoint.position, transform.TransformDirection(Vector3.forward), out pew, 20))
        {
            Instantiate(Impact, pew.point, Quaternion.LookRotation(pew.normal));
            Debug.Log("impact");

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

            Vector3 directionWithoutSpread = targetPoint - GunPoint.position;
       
            GameObject CurrentBullet = Instantiate(Bullet, GunPoint.position, quaternion.identity);
            CurrentBullet.transform.forward = directionWithoutSpread.normalized;
        
            CurrentBullet.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * ShootForce, ForceMode.Impulse);
        }
        
        
        
        /*RaycastHit hit;
        if (Physics.Raycast(FPScam.transform.position, FPScam.transform.forward, out hit, Range))
        {
            Debug.Log(hit.transform.name);
            
            
            

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * ImpactForce);
            }


        }*/
        
        bulletsLeft--;
        AmmoCount.SetText( "Ammo: " + bulletsLeft + "/5");
    }

  
    IEnumerator Reload()
    {
        gun.SetBool("Reloading", true);
        Debug.Log("Reload?");
        reloading = true;
        yield return new WaitForSeconds(1);
        reloading = false;
        bulletsLeft = MagSize;
        gun.SetBool("Reloading", false);
    }
}
