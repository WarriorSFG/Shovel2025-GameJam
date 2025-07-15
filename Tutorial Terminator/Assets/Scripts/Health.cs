using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int startingHealth = 4;
    public int currentHealth { get; private set; }
    private Animator anim;
    private bool dead;
    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();

    }
    private void TakeDamage(int _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;
            }
        }
    }
    private void Update()
    {
        //Make the player take dmg HERE.
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(1);
        }
    }
}
