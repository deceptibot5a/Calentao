using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class FileManager : MonoBehaviour {
    //public InputField fileNameInputField;
    [SerializeField] private string fileNameInputField;

    public void Save() {
        //string fileName = fileNameInputField.text;
        string fileName = fileNameInputField;
        string filePath = Application.dataPath + "/" + fileName + ".txt";

        SaveFile newSaveFile = new SaveFile();
        string jsonString = JsonUtility.ToJson(newSaveFile);
        File.WriteAllText(filePath, jsonString);
    }

    public void Load() {
        //string fileName = fileNameInputField.text;
        string fileName = fileNameInputField;
        string filePath = Application.dataPath + "/" + fileName + ".txt";

        if (File.Exists(filePath)) {
            string jsonString = File.ReadAllText(filePath);
            SaveFile newSaveFile = JsonUtility.FromJson<SaveFile>(jsonString);
        } else {
            Debug.Log("No savefile with that name");
        }
    }
}
