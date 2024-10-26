using UnityEngine;
using System.Collections;

public class DraggableDice : MonoBehaviour
{
    private Vector3 originalPosition;    // The position where the dice started
    private bool isDragging = false;     // Is the dice currently being dragged?
    private bool validPlacement = false; // Is the dice placed in a valid area?
    private Vector3 snapPosition;        // The position to snap the dice to

    private Collider2D diceCollider;     // Reference to the dice's collider
    private Camera mainCamera;           // Reference to the main camera

    public float moveBackTime = 0.3f;    // Time it takes to move back to the original position

    private Collider2D currentValidArea; // Reference to the current valid area

    public bool isPlaced = false;
    void Start()
    {
        // Store the original position
        originalPosition = transform.position;
        diceCollider = GetComponent<Collider2D>();
        mainCamera = Camera.main;
    }

    void OnMouseDown()
    {
        if (!isDragging)
        {
            // Start dragging
            isDragging = true;
            //Debug.Log("Started dragging the dice.");
        }
    }

    void OnMouseUp()
    {
        if (isDragging)
        {
            // Stop dragging
            isDragging = false;
            //Debug.Log("Stopped dragging the dice.");

            if (validPlacement)
            {
                // Check if there is already a die in the same valid area
                DraggableDice[] allDice = FindObjectsOfType<DraggableDice>();
                bool hasDieInSameValidArea = false;

                foreach (DraggableDice dice in allDice)
                {
                    if (dice != this && dice.currentValidArea == this.currentValidArea)
                    {
                        hasDieInSameValidArea = true;
                        break;
                    }
                }

                if (!hasDieInSameValidArea)
                {
                    // Snap to the snap position and move die in front
                    transform.position = new Vector3(snapPosition.x, snapPosition.y, -1f);
                    isPlaced = true;
                }
                else
                {
                    // Move back to the original position
                    StartCoroutine(MoveBackToOriginalPosition());
                }
            }
            else
            {
                // Move back to the original position
                StartCoroutine(MoveBackToOriginalPosition());
            }
        }
    }

    void Update()
    {
        if (isDragging)
        {
            // Get mouse position in world coordinates
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = -1f; // Move die in front
            transform.position = mousePosition;
        }
    }

    IEnumerator MoveBackToOriginalPosition()
    {
        float elapsedTime = 0f;
        Vector3 startingPosition = transform.position;
        Vector3 targetPosition = new Vector3(originalPosition.x, originalPosition.y, -1f);

        while (elapsedTime < moveBackTime)
        {
            transform.position = Vector3.Lerp(startingPosition, targetPosition, (elapsedTime / moveBackTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        diceCollider.enabled = true; // Ensure the collider is enabled after moving back
        isPlaced = false;
        //Debug.Log("Moved back to the original position.");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ValidArea"))
        {
            validPlacement = true;
            // Calculate the center of the collider
            snapPosition = other.bounds.center;
            currentValidArea = other; // Store the valid area
            //Debug.Log("Dice is in a valid area!");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ValidArea"))
        {
            validPlacement = false;
            if (other == currentValidArea)
                currentValidArea = null; // Clear the reference
        }
    }
}
