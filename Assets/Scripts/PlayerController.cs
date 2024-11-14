using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float damage;
    [SerializeField]
    private float health;
    [SerializeField]
    private float defense;
    [SerializeField]
    private float speed;

    [SerializeField]
    public float speedMultiplier = 1.0f;

    public PlayerObject playerStats;

    private Camera mainCamera;
    private Vector3 cameraOffset;

    void Start()
    {
        damage = playerStats.damage;
        health = playerStats.health*20;
        defense = playerStats.defense;
        speed = playerStats.speed;
        speed = 5; // Adjust this value as needed
        health = 100; // Adjust this value as needed

        // Initialize the main camera and calculate the offset from the player
        mainCamera = Camera.main;
        cameraOffset = mainCamera.transform.position - transform.position;
    }

    void Update()
    {
        // Get input from horizontal and vertical axes
        float moveHorizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right arrow keys
        float moveVertical = Input.GetAxis("Vertical");     // W/S or Up/Down arrow keys

        // Create a movement vector (swap z and y)
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);

        // Normalize movement vector to maintain consistent speed in all directions
        if (movement.magnitude > 1)
        {
            movement = movement.normalized;
        }

        // Adjust speed logarithmically
        float adjustedSpeed = Mathf.Log(speed + 1) * speedMultiplier; // Adding 1 to avoid log(0)

        // Move the player
        transform.Translate(movement * adjustedSpeed * Time.deltaTime, Space.World);

        // Update the camera position to follow the player
        mainCamera.transform.position = transform.position + cameraOffset;
    }

    // Add this method to handle taking damage
    public void TakeDamage(float amount)
    {
        float actualDamage = Mathf.Max(amount - defense, 0); // Ensure damage doesn't go negative
        health -= actualDamage;
        Debug.Log($"Player took {actualDamage} damage after defense. Remaining health: {health}");

        if (health <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
        // Handle player death (e.g., play animation, reload level, show game over screen)
        Debug.Log("Player has died!");
        // For now, you might want to disable player controls or trigger a game over screen
        // Destroy(gameObject); // Uncomment if you want to remove the player object
    }
}
