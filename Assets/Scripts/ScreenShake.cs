 using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public Transform cameraTransform;
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.1f;
    public float dampingSpeed = 1.0f;

    private Vector3 initialPosition;
    private float currentShakeDuration = 0f;

    void Awake()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void OnEnable()
    {
        initialPosition = cameraTransform.localPosition;
    }

    void Update()
    {
        if (currentShakeDuration > 0)
        {
            cameraTransform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            currentShakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            currentShakeDuration = 0f;
            cameraTransform.localPosition = initialPosition;
        }
    }

    public void TriggerShake()
    {
        currentShakeDuration = shakeDuration;
    }

    public void SetShakeDuration(float duration)
    {
        shakeDuration = duration;
    }

    public void SetShakeMagnitude(float magnitude)
    {
        shakeMagnitude = magnitude;
    }
}
