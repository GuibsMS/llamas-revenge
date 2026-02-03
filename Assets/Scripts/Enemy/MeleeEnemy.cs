using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private Transform detectPosition;
    [SerializeField] private Vector2 detectBoxSize;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float attackCooldown;

    private float coolDownTimer;
    private Animator animator;
    private Health meleeEnemyHealth;
    private bool isDead = false;

    private void Awake()
    {
      meleeEnemyHealth = GetComponent<Health>();  

      meleeEnemyHealth.OnHurt += PlayHurtAnim;
      meleeEnemyHealth.OnDead += PlayDeadAnim;
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

protected void Update()
    {
        coolDownTimer += Time.deltaTime;
        VerifyCanAttack();
    }

private void VerifyCanAttack()
    {

        if (isDead) return;

        if(coolDownTimer < attackCooldown) return;

        if (PlayerInSight())
        {
           animator.SetTrigger("attack");
           AttackPlayer();
        }
    }

    private void AttackPlayer()
    {

        if (isDead) return;

        coolDownTimer = 0f;

        if (CheckPlayerInDetectArea().TryGetComponent(out Health playerHealth))
        {
            print("Making player take damage");
            playerHealth.TakeDamage();
        }
    }

    private Collider2D CheckPlayerInDetectArea()
    {
        return Physics2D.OverlapBox
        (detectPosition.position, detectBoxSize, 0f, playerLayer);
    }
    private bool PlayerInSight()
    {
        Collider2D playerCollider = 
        CheckPlayerInDetectArea();
        return playerCollider != null;
    }

    private void PlayHurtAnim()
    {
        animator.SetTrigger("hurt");
    }

    private void PlayDeadAnim()
    {
        isDead = true;

        animator.SetTrigger("dead");

        GetComponent<Collider2D>().enabled = false;

        StartCoroutine(DestroyEnemy(3));
    }

    private IEnumerator DestroyEnemy(int time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        if (detectPosition == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(detectPosition.position, detectBoxSize);
    }
}
