using TMPro;
using UnityEngine.UI;
using NUnit.Framework;
using Unity.VisualScripting;    
using UnityEngine;
using System;

public class PlayerBehavior : MonoBehaviour
{
    [Header("Propriedades de movimento")]
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float jumpForce = 3;

    [Header("Propriedades de ataque")]
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private Transform attackPosition;
    [SerializeField] private LayerMask attackLayer;

    public int emeraldsCollected = 0;
    public TextMeshProUGUI emeraldsText;

    public GameObject spit;
    private Rigidbody2D rigidbody;
    private IsGroundedChecker isGroundedChecker;

    private void Awake()
    {        
        rigidbody = GetComponent<Rigidbody2D>();
        isGroundedChecker = GetComponent<IsGroundedChecker>();
        GetComponent<Health>().OnDead += HandlePlayerDeath;
    }

    private void Start()
    {
        GameManager.Instance.InputManager.OnJump += HandleJump;
    }

    private void Update()
    {
        float moveDirection = GameManager.Instance.InputManager.Movement;
        transform.Translate(moveDirection * Time.deltaTime * moveSpeed, 0, 0);

        if (moveDirection < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (moveDirection > 0)
        {
            transform.localScale = Vector3.one;
        }

        if (isGroundedChecker.IsGrounded() == false)
        {
            moveSpeed = 8; 
        }
        else
        {
            moveSpeed = 10; 
        }
      
    }

    private void HandleJump()
    {
        if (isGroundedChecker.IsGrounded() == false) return;
        rigidbody.linearVelocity += Vector2.up * jumpForce;
    }

    private void HandlePlayerDeath()
    {
        GetComponent<Collider2D>().enabled = false;
        rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        GameManager.Instance.InputManager.DisablePlayerInput();
    }

    private void Attack()
    {
        Collider2D[] hittedEnemies = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, attackLayer);

        foreach (Collider2D hittedEnemy in hittedEnemies)
        {
            print("Checking Enemy");
            if (hittedEnemy.TryGetComponent(out Health enemyHealth))
            {
                print("Getting Damage");
                enemyHealth.TakeDamage();
            }
        }
    }

    private void Shoot()
    {
        if (spit == null)
        {
            Debug.LogError("Spit prefab is not assigned in the inspector.");
            return;
        }

        GameObject newSpit = Instantiate(spit, attackPosition.position, Quaternion.identity);
        Projectile spitScript = newSpit.GetComponent<Projectile>();
        float llamaDirection = transform.localScale.x;

        spitScript.Launch(llamaDirection);
    }

    public void EmeraldCollected()
    {
        emeraldsCollected++;
        emeraldsText.text = emeraldsCollected.ToString();
        Debug.Log("Esmeralda coletada! Total: " + emeraldsCollected);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPosition.position,attackRange);
    }

}
