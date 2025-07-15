using System.Collections;
using System.Threading.Tasks;
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
            StartCoroutine(Attack());
        }
        CDTimer += Time.deltaTime;
    }
    IEnumerator Attack()
    {
        anim.SetTrigger("attack");

        // Wait until animator switches to Attack state
        while (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            yield return null;

        // Wait for animation to complete (normalizedTime goes to a position which can fire attack)
        while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.85f)
            yield return null;

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
