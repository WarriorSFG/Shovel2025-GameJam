using UnityEngine;

public class Heathbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        anim.SetInteger("currentHealth", playerHealth.currentHealth);
    }

    private void Update()
    {
        anim.SetInteger("currentHealth", playerHealth.currentHealth);
    }
}
