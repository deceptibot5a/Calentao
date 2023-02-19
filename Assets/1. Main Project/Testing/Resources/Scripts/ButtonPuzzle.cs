using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPuzzle : MonoBehaviour
{
    // lista de secuencia correcta:
    public int[] solution;
    // Lista de secuencia de usuario:
    public int[] currentSequence;
    // Numero del boton presionado:
    public int sequenceIndex;


    public GameObject door;

    void Start()
    {
        currentSequence = new int[solution.Length];
    }

    public void ButtonPressed(int buttonNumber)
    {
        currentSequence[sequenceIndex] = buttonNumber;

        if (currentSequence[sequenceIndex] != solution[sequenceIndex])
        {
            Debug.Log("sequencia incorrecta reiniciando..");

            ResetSequence();
        }
        else if (sequenceIndex == solution.Length - 1)
        {
            PuzzleComplete();
        }
        else
        {
            sequenceIndex++;
        }
    }

    void ResetSequence()
    {
        sequenceIndex = 0;
        for (int i = 0; i < currentSequence.Length; i++)
        {
            currentSequence[i] = 0;
        }
    }

    void PuzzleComplete()
    {
        Debug.Log("Puzzle completado!");
        door.SetActive(false);
    }
}
