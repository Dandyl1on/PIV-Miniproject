using UnityEngine;

public class GunShoot : MonoBehaviour
{

    public int damage = 10;

    public float Range = 10f;

    public Camera FPScam;

    public float ImpactForce = 30f;

    public GameObject Corsair;
    // Update is called once per frame
    void Update()
    {
        Aim();
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    public void Aim()
    {
        RaycastHit Casting;
        if (Physics.Raycast(FPScam.transform.position, FPScam.transform.forward, out Casting, Range))
        {
            Corsair.transform.position = Casting.point + Casting.normal * Corsair.transform.localScale.x/2f;
        }
    }
    public void Shoot()
    {
        RaycastHit hit;
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
        
    }
}
