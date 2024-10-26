using UnityEngine;

public class MoveUpAndDown : MonoBehaviour
{
    public float bobSpeed = 1f; // Speed at which the object bobs up and down
    public float bobAmount = 1f; // Amount by which the object bobs up and down

    private float startY; // Initial Y position of the object

    private void Start()
    {
        startY = transform.position.y; // Store the initial Y position of the object
    }

    private void Update()
    {
        // Calculate the new Y position based on the sine wave
        float newY = startY + Mathf.Sin(Time.time * bobSpeed) * bobAmount;

        // Update the object's position
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
