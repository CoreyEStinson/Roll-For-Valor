using UnityEngine;

public class GrowAndShrink : MonoBehaviour
{
    public float growSpeed = 1f; // Speed at which the object grows
    public float maxSize = 2f; // Maximum size of the object
    public float minSize = 0.5f; // Minimum size of the object

    private Vector3 initialScale; // Initial scale of the object

    private void Start()
    {
        initialScale = transform.localScale; // Store the initial scale of the object
    }

    private void Update()
    {
        // Calculate the new scale based on the growth speed
        float newScale = Mathf.PingPong(Time.time * growSpeed, maxSize - minSize) + minSize;

        // Update the object's scale
        transform.localScale = initialScale * newScale;
    }
}
