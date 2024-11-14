using UnityEngine;

public class DieStatDistributer : MonoBehaviour
{
    public PlayerObject playerObject;

    public bool damagePlacement = false;
    public bool healthPlacement = false;
    public bool defensePlacement = false;
    public bool speedPlacement = false;

    private int frameCounter = 0;
    private int updateInterval = 10; // Update every 10 frames

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        frameCounter++;
        if (frameCounter >= updateInterval)
        {
            frameCounter = 0;
            CheckCollisions();
        }
    }

    private void CheckCollisions()
    {
        // Reset stats to 0 before checking for collisions
        if (damagePlacement)
        {
            playerObject.damage = 0;
        }
        else if (healthPlacement)
        {
            playerObject.health = 0;
        }
        else if (defensePlacement)
        {
            playerObject.defense = 0;
        }
        else if (speedPlacement)
        {
            playerObject.speed = 0;
        }

        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.GetComponent<DiceRoll>() != null)
            {
                UpdatePlayerStats(collider);
            }
        }
    }

    private void UpdatePlayerStats(Collider2D collision)
    {
        if (damagePlacement)
        {
            playerObject.damage = collision.gameObject.GetComponent<DiceRoll>().GetRollNumber();
            Debug.Log("Damage value: " + playerObject.damage);
        }
        else if (healthPlacement)
        {
            playerObject.health = collision.gameObject.GetComponent<DiceRoll>().GetRollNumber();
            Debug.Log("Health value: " + playerObject.health);
        }
        else if (defensePlacement)
        {
            playerObject.defense = collision.gameObject.GetComponent<DiceRoll>().GetRollNumber();
            Debug.Log("Defense value: " + playerObject.defense);
        }
        else if (speedPlacement)
        {
            playerObject.speed = collision.gameObject.GetComponent<DiceRoll>().GetRollNumber();
            Debug.Log("Speed value: " + playerObject.speed);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Exited");

        if (damagePlacement)
        {
            playerObject.damage = 0;
            Debug.Log("Damage value: " + playerObject.damage);
        }
        else if (healthPlacement)
        {
            playerObject.health = 0;
            Debug.Log("Health value: " + playerObject.health);
        }
        else if (defensePlacement)
        {
            playerObject.defense = 0;
            Debug.Log("Defense value: " + playerObject.defense);
        }
        else if (speedPlacement)
        {
            playerObject.speed = 0;
            Debug.Log("Speed value: " + playerObject.speed);
        }
    }
}
