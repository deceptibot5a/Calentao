using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClaveDinamica : MonoBehaviour
{
    public int listLength = 6; // The length of the number lists
    public float timeBetweenChanges = 12.0f;
    public List<ButtonPuzzle> otherListComponents; // List of other objects' list components

    private float timeSinceLastChange = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial number list
        GenerateRandomList();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if it's time to change the number list
        timeSinceLastChange += Time.deltaTime;
        if (timeSinceLastChange >= timeBetweenChanges)
        {
            GenerateRandomList();
            timeSinceLastChange = 0.0f;
        }
    }

    // Generates a new random number list and adds it to each of the other objects' list components
    void GenerateRandomList()
    {
        // Create a new list of numbers
        List<int> numberList = new List<int>();

        // Populate the list with random numbers
        for (int i = 0; i < listLength; i++)
        {
            numberList.Add(Random.Range(0, 10));
        }

        // Add the new number list to each of the other objects' list components
        foreach (ButtonPuzzle otherListComponent in otherListComponents)
        {
            
        }
    }
}
