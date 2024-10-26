using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSpawner : MonoBehaviour
{
    public GameObject dicePrefab; // Reference to the dice prefab
    public int numberOfDice = 5;  // Number of dice to spawn
    public float xSpacing = 2.0f; // Spacing between dice on the x-axis
    public float yPosition = 0.0f; // Y position for all dice
    public float rollDelay = 2.0f; // Delay between rolls in seconds
    public List<GameObject> diceList = new List<GameObject>();

    private bool canRoll = true; // Flag to check if dice can be rolled

    void Start()
    {
        SpawnDice();
    }

    void SpawnDice()
    {
        Vector3 spawnPosition = transform.position; // Get the position of the object this script is attached to

        for (int i = 0; i < numberOfDice; i++)
        {
            float xPosition = spawnPosition.x + (i * xSpacing);
            Vector3 dicePosition = new Vector3(xPosition, spawnPosition.y, spawnPosition.z);
            GameObject newDice = Instantiate(dicePrefab, dicePosition, Quaternion.identity);
            diceList.Add(newDice);
        }
    }

    public void RollAllDice()
    {
        if (canRoll)
        {
            StartCoroutine(RollDiceWithDelay());
        }
    }

    private IEnumerator RollDiceWithDelay()
    {
        canRoll = false;

        foreach (GameObject dice in diceList)
        {
            dice.GetComponent<DiceRoll>().RollDice();
        }

        yield return new WaitForSeconds(rollDelay);
        canRoll = true;
    }
}
