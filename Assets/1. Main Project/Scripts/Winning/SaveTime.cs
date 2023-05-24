using System.IO;
using UnityEngine;

public class SaveTime : MonoBehaviour
{
    private string overwriteFile;
    
    void Start() {
        overwriteFile = Application.dataPath + "/overwrite.txt";
    }

    public void AppendText(string txtInput, string appendFile) {
        if (!File.Exists(appendFile)) {
            File.WriteAllText(appendFile, txtInput);
        } else {
            using (var writer = new StreamWriter(appendFile, true)) {
                writer.WriteLine(txtInput);
            }
        }
    }
    
    public void OverwriteText(string txtInput) {
        using (var writer = new StreamWriter(overwriteFile, false)) { 
            writer.WriteLine(txtInput);
        }
    }
}
