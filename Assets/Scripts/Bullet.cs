using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject enemy;
    public float timer;
        
    
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("BANG");
    }

    // Update is called once per frame
    void Update()
    {
        Dead();
        if (timer >= 4)
        {
            Destroy(gameObject);
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemy.GetComponent<Target>().takeDamage(10);
            Destroy(gameObject);
            Debug.Log("destroyed?");
        }
    }

    private void Dead()
    {
        

        
    }
}
