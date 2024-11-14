using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyData enemyData;

    private float currentHealth;
    private float attackTimer;

    private Animator animator;
    private Transform playerTransform;
    private PlayerController playerController;

    private Rigidbody2D rb2D; // For 2D movement
    private Vector2 movementDirection;
    private bool isFacingRight = true;

    private void Start()
    {
        if (enemyData != null)
        {
            InitializeEnemy();
        }
        else
        {
            Debug.LogError("Enemy Data is missing!");
        }

        // Find the player by tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
            playerController = player.GetComponent<PlayerController>();
            if (playerController == null)
            {
                Debug.LogError("PlayerController component not found on player!");
            }
        }
        else
        {
            Debug.LogError("Player not found! Ensure the player GameObject is tagged 'Player'.");
        }
    }

    private void InitializeEnemy()
    {
        currentHealth = enemyData.health;

        // Set up sprite
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && enemyData.enemySprite != null)
        {
            spriteRenderer.sprite = enemyData.enemySprite;
        }

        // Set up animations
        animator = GetComponent<Animator>();
        if (animator != null && enemyData.animatorController != null)
        {
            animator.runtimeAnimatorController = enemyData.animatorController;
        }

        // Get Rigidbody2D component
        rb2D = GetComponent<Rigidbody2D>();
        if (rb2D == null)
        {
            Debug.LogError("Rigidbody2D component missing from enemy!");
        }
    }

    private void Update()
    {
        if (playerTransform == null) return;

        // Calculate movement direction
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        movementDirection = direction;

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= enemyData.attackRange)
        {
            // Stop moving and attack
            movementDirection = Vector2.zero;

            // Update animator to idle
            animator.SetFloat("Speed", 0);

            // Attack the player
            Attack();
        }
        else
        {
            // Update animator with movement speed
            animator.SetFloat("Speed", movementDirection.magnitude);
        }

        // Flip sprite based on direction
        FlipSprite();

        // Handle attack cooldown
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (playerTransform == null) return;

        // Move towards the player
        rb2D.MovePosition(rb2D.position + movementDirection * enemyData.speed * Time.fixedDeltaTime);
    }

    private void FlipSprite()
    {
        if ((movementDirection.x > 0 && !isFacingRight) || (movementDirection.x < 0 && isFacingRight))
        {
            isFacingRight = !isFacingRight;
            Vector3 scaleFactor = transform.localScale;
            scaleFactor.x *= -1;
            transform.localScale = scaleFactor;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            // Play hurt animation
            animator?.SetTrigger("Hurt");
        }
    }

    private void Die()
    {
        // Play death animation
        animator?.SetTrigger("Die");

        // Drop loot
        DropLoot();

        // Disable enemy components
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

        // Destroy after death animation
        Destroy(gameObject, 1f);
    }

    private void DropLoot()
    {
        for (int i = 0; i < enemyData.lootItems.Length; i++)
        {
            if (Random.value <= enemyData.lootDropChances[i])
            {
                Instantiate(enemyData.lootItems[i], transform.position, Quaternion.identity);
            }
        }
    }

    public void Attack()
    {
        if (attackTimer <= 0)
        {
            // Play attack animation
            animator?.SetTrigger("Attack");

            // Reset attack timer
            attackTimer = enemyData.attackCooldown;

            // Implement attack logic here
            if (playerController != null)
            {
                playerController.TakeDamage(enemyData.damage);
                Debug.Log($"{enemyData.enemyName} attacked the player for {enemyData.damage} damage.");
            }
        }
    }

    public void UseSpecialAbility()
    {
        if (enemyData.hasSpecialAbility)
        {
            Debug.Log($"{enemyData.enemyName} used {enemyData.abilityName}!");
            // Add special ability logic here
        }
    }
}
