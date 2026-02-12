using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Animator animator;
    private IsGroundedChecker groundedChecker;
    private Health playerHealth;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        groundedChecker = GetComponent<IsGroundedChecker>();
        playerHealth = GetComponent<Health>();

        playerHealth.OnHurt += PlayHurtAnim;
        playerHealth.OnDead += PlayDeadAnim;

        GameManager.Instance.InputManager.OnAttack += PlayAttackAnim;
        GameManager.Instance.InputManager.OnSpit += PlaySpitAnim;
    }

    private void Update()
    {
        bool isMoving = GameManager.Instance.InputManager.Movement != 0;
        animator.SetBool("isMoving", isMoving);

        bool isJumping = !groundedChecker.IsGrounded();
        animator.SetBool("isJumping", isJumping);
    }

    private void PlayHurtAnim()
    {
        animator.SetTrigger("hurt"); 
    }

     private void PlayDeadAnim()
    {
        animator.SetTrigger("dead"); 
    }

    private void PlayAttackAnim()
    {
        animator.SetTrigger("attack");
    }

    private void PlaySpitAnim()
    {
        animator.SetTrigger("spit");
    }
}
