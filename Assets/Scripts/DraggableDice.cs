using UnityEngine;
using System.Collections;

public class DraggableDice : MonoBehaviour
{
    private Vector3 originalPosition;    // The position where the dice started
    private bool isDragging = false;     // Is the dice currently being dragged?
    private bool validPlacement = false; // Is the dice placed in a valid area?

    private Collider2D diceCollider;     // Reference to the dice's collider
    private Camera mainCamera;           // Reference to the main camera

    void Start()
    {
        // Store the original position
        originalPosition = transform.position;
        diceCollider = GetComponent<Collider2D>();
        mainCamera = Camera.main;
    }

    void OnMouseDown()
    {
        // Start dragging
        isDragging = true;
    }

    void OnMouseUp()
    {
        // Stop dragging
        isDragging = false;

        // Check if the placement is valid
        if (!validPlacement)
        {
            // Move back to the original position
            StartCoroutine(MoveBackToOriginalPosition());
        }
    }

    void Update()
    {
        if (isDragging)
        {
            // Get mouse position in world coordinates
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f; // Keep dice on the same plane
            transform.position = mousePosition;
        }
    }

    IEnumerator MoveBackToOriginalPosition()
    {
        float elapsedTime = 0f;
        float duration = 0.5f; // Duration of the move back animation
        Vector3 startingPosition = transform.position;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startingPosition, originalPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;
    }

    // Example method to set validPlacement based on collision
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ValidArea"))
        {
            validPlacement = true;
            // Logs a message to the console
            Debug.Log("Dice is in a valid area!");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ValidArea"))
        {
            validPlacement = false;
        }
    }

}
