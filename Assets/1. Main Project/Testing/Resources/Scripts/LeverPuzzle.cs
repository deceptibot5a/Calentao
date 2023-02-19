using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPuzzle : MonoBehaviour
{
    // lista de palancas
    public GameObject[] levers;
    // secuencia de palancas
    public int[] leverSequence = { 0, 1, 2, 3};

    // posicion en la secuencia
    private int currentPosition = 0;

    public bool PuzzleUsed;

    // Update lever states when player interacts with them
    private void Update()
    {
        for (int i = 0; i < levers.Length; i++)
        {
            if (PuzzleUsed == true)
            {
                levers[i].transform.Rotate(0, 0, 90);
                levers[i].GetComponent<AudioSource>().Play();
                levers[i].GetComponent<Lever>().isPulled = true;
                CheckSequence(i);
            }
        }
    }

    // Check if the player's input matches the current position in the leverSequence array
    private void CheckSequence(int leverIndex)
    {
        if (leverIndex == leverSequence[currentPosition])
        {
            currentPosition++;
            if (currentPosition == leverSequence.Length)
            {
                OpenDoor();
            }
            else
            {
                ResetLevers(leverIndex);
            }
        }
        else
        {
            ResetLevers(-1);
            currentPosition = 0;
        }
    }

    // Reset the state of all levers except the one that was just interacted with
    private void ResetLevers(int excludeIndex)
    {
        for (int i = 0; i < levers.Length; i++)
        {
            if (i != excludeIndex)
            {
                levers[i].transform.rotation = Quaternion.identity;
                levers[i].GetComponent<Lever>().isPulled = false;
            }
        }
    }

    // Open the door or perform some other action to indicate that the puzzle has been solved
    private void OpenDoor()
    {
        Debug.Log("Puzzle solved! Door opening...");
        // Code to open door goes here
    }
}
