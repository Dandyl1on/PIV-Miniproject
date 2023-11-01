
using UnityEngine;

public class Target : MonoBehaviour
{
    public float Health = 50f;

    public void takeDamage(float amount)
    {
        Health -= amount;
        if (Health <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
   
