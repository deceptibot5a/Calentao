using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ButtonPuzzle : MonoBehaviour
{

    // Numero del boton presionado:
    public int sequenceIndex;
    // GameObject de la puerta:
    public GameObject door;
    // Tiempo entre cambios de lista:
    public float timeBetweenLists = 12f;
    // Tiempo restante hasta el siguiente cambio de lista:
    [SerializeField] private float timeRemaining;
    

    private bool iscomplete = false;

    [SerializeField] private int RangeStart;
    [SerializeField] private int RangeEnd;
    [SerializeField] PhotonView PV;
    // lista de secuencia correcta:
    public int[] solution;
    // Lista de secuencia de usuario:
    public int[] currentSequence;

    void Start()
    {
        currentSequence = new int[solution.Length];
        timeRemaining = timeBetweenLists;
        PV.RPC("UpdateSequence", RpcTarget.All, solution);
    }

    void Update()
    {
        if (!iscomplete)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                GenerateRandomList();
                timeRemaining = timeBetweenLists;
            }
        }

    }

    public void ButtonPressed(int buttonNumber)
    {
        currentSequence[sequenceIndex] = buttonNumber;
        sequenceIndex++;

        if (sequenceIndex == solution.Length)
        {
            // El jugador ingreso la lista entonces verifica que este correcto
            bool sequenceCorrect = true;
            for (int i = 0; i < currentSequence.Length; i++)
            {
                if (currentSequence[i] != solution[i])
                {
                    sequenceCorrect = false;
                    break;
                }
            }

            if (sequenceCorrect)
            {
                PuzzleComplete();
            }
            else
            {
                Debug.Log("Sequencia incorrecta, reiniciando...");
                ResetSequence();
            }
        }
    }

    public int[] GetMySolution()
    {
        return solution;
    }

    public int[] GetMyCurrent()
    {
        return currentSequence;
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
        iscomplete = true;
        Debug.Log("Puzzle completado!");
        door.SetActive(false);
    }

    void GenerateRandomList()
    {
        int[] newSequence = new int[solution.Length];
        for (int i = 0; i < newSequence.Length; i++)
        {
            newSequence[i] = Random.Range(RangeStart, RangeEnd);
        }
        PV.RPC("UpdateSequence", RpcTarget.All, newSequence);
    }
    
    [PunRPC]
    void UpdateSequence(int[] newSequence)
    {
        solution = newSequence;
        ResetSequence();
    }
    
  
}
