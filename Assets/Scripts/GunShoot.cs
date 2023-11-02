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

    public GameObject Corsair;

    public GameObject Bullet;
    public float ShootForce;
    public int MagSize;
    public int bulletsLeft;
    public bool reloading;
    public Transform GunPoint;
    
    public TextMeshPro AmmoCount;

    public Animator gun;
    private void Awake()
    {
        bulletsLeft = MagSize;
        //gun = GetComponent<Animator>();
        //gun.SetBool("Reloading", false);
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
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
    
    public void Aim()
    {
        RaycastHit casting;
        if (Physics.Raycast(FPScam.transform.position, FPScam.transform.forward, out casting, Range))
        {
            Corsair.transform.position = casting.point + casting.normal * Corsair.transform.localScale.x/2f;
        }
    }
    // ReSharper disable Unity.PerformanceAnalysis
    public void Shoot()
    {
        Ray ray = FPScam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
        RaycastHit hit;
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(50);
        }

        Vector3 directionWithoutSpread = targetPoint - GunPoint.position;
        


        GameObject CurrentBullet = Instantiate(Bullet, GunPoint.position, quaternion.identity);
        CurrentBullet.transform.forward = directionWithoutSpread.normalized;
        
        CurrentBullet.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * ShootForce, ForceMode.Impulse);
        
        if (Physics.Raycast(FPScam.transform.position, FPScam.transform.forward, out hit, Range))
        {
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            
            
            if (target != null)
            {
                target.takeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * ImpactForce);
            }

            
        }
        
        bulletsLeft--;
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
