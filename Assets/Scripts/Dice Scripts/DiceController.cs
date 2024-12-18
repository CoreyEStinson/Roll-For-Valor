using UnityEngine;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;

public class DiceRoll : MonoBehaviour
{
    public Sprite[] diceSides;
    public float initialSpeed = 0.05f;     // Starting speed of the sprite change
    public float speedIncrement = 0.02f;   // How much the speed decreases each step
    public float maxSpeed = 0.2f;          // Maximum delay between sprite changes (slowest speed)
    public float shakeIntensity = 0.1f;    // Intensity of the shake effect
    public float shakeFrequency = 0.01f;   // Time between shakes
    public ParticleSystem rollParticles;   // Optional particle effect
    private SpriteRenderer rend;           // Reference to the SpriteRenderer component
    private bool isRolling = false;        // Flag to prevent multiple rolls at once
    private Vector3 originalPosition;      // Original position of the dice
    private Quaternion originalRotation;   // Original rotation of the dice

    private float rollsLeft;
    public float maxRolls = 15;
    public DraggableDice draggableDice;

    public AudioSource diceRoll;

    int randomSide;

    void Start()
    {
        rollsLeft = maxRolls;
        rend = GetComponent<SpriteRenderer>();
        rend.sprite = diceSides[6];
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        draggableDice.isEmptyDice = true;
        diceRoll = GameObject.Find("Die Roll").GetComponent<AudioSource>();

    }

    public void RollDice()
    {
        if (!isRolling && !draggableDice.isPlaced && rollsLeft > 0)
        {
            diceRoll.gameObject.GetComponent<SoundRandomiser>().PlaySound();
            rollsLeft--;
            draggableDice.isEmptyDice = false;
            StartCoroutine(RollTheDice());
            StartCoroutine(ShakeDice());
            
        }
    }

    IEnumerator RollTheDice()
    {
        isRolling = true;
        randomSide = 0;
        float currentSpeed = initialSpeed;

        // Start particle effect
        if (rollParticles != null)
        {
            rollParticles.Play();
        }

        // Play rolling animation (sprite changes)
        while (currentSpeed < maxSpeed)
        {
            randomSide = Random.Range(0, diceSides.Length-1);
            rend.sprite = diceSides[randomSide];
                
            yield return new WaitForSeconds(currentSpeed);
            currentSpeed += speedIncrement;
        }

        // Stop on a final random side
        randomSide = Random.Range(0, diceSides.Length-1);
        rend.sprite = diceSides[randomSide];

        // Stop particle effect
        if (rollParticles != null)
        {
            rollParticles.Stop();
        }

        isRolling = false;
    }

    IEnumerator ShakeDice()
    {
        while (isRolling)
        {
            // Apply shaking effects using shakeIntensity

            // Position shake
            float shakeX = Random.Range(-shakeIntensity, shakeIntensity);
            float shakeY = Random.Range(-shakeIntensity, shakeIntensity);
            transform.position = new Vector3(originalPosition.x + shakeX, originalPosition.y + shakeY, originalPosition.z);

            // Rotation shake
            float shakeRot = Random.Range(-shakeIntensity * 10f, shakeIntensity * 10f);
            transform.rotation = Quaternion.Euler(0, 0, shakeRot);

            yield return new WaitForSeconds(shakeFrequency);
        }

        // Reset transformations
        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }
    public int GetRollNumber()
    {
        return randomSide+1;
    }
}
