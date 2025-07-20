using UnityEngine;

public class bullet : MonoBehaviour
{
    public float lifetime = 5f; 
    
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("target") || other.CompareTag("Target"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject); 
            return;
        }
        
        if (other.gameObject.name.Contains("enemy") || other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<MonoBehaviour>();
            if (enemy != null)
            {
                enemy.SendMessage("TakeDamage", SendMessageOptions.DontRequireReceiver);
            }
            Destroy(gameObject); 
            return;
        }
        
        if (other.gameObject.layer != LayerMask.NameToLayer("Player")) // Don't destroy on player collision
        {
            Destroy(gameObject);
        }
    }
}
