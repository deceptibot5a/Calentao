using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveFile : MonoBehaviour {
    [SerializeField] public List<string> times;
    public string timeData = "";

    public SaveFile() {
        //times.Add(timeData);
        /*times = new List<string>();

        foreach (LineRenderer lineRenderer in completionTimes) {
            times.Add(new Line(lineRenderer));
        }*/
    }
}