using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class SerializeConfig : MonoBehaviour
{
    [MenuItem("Data/Clear Data")]
    private static void ClearData()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "data.json");
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}
