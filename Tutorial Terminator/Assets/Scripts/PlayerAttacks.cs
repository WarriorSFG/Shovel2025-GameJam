using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    [SerializeField] private float attackCD;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float CDTimer = Mathf.Infinity;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && CDTimer > attackCD && playerMovement.canAttack())
        {
            Attack();
        }
        CDTimer += Time.deltaTime;
    }
    private void Attack()
    {
        anim.SetTrigger("attack");
        CDTimer = 0;

        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));

    }
    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
    
}
