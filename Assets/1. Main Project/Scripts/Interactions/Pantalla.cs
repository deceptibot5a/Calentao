using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Pantalla : MonoBehaviour
{
    public TextMeshProUGUI Password;
    public TextMeshProUGUI Current;
    private int[] SolutionT;
    private int[] CurrentT;
    private ButtonPuzzle puzzle;

    void Start()
    {
        puzzle = GetComponent<ButtonPuzzle>();
    }

    void Update()
    {
        SolutionT = puzzle.GetMySolution();
        CurrentT = puzzle.GetMyCurrent();
        string mySolution = string.Join(", ", SolutionT);
        string myCurrent = string.Join(", ", CurrentT);
        Password.SetText(mySolution);
        Current.SetText(myCurrent);
    }
}