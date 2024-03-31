using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(menuName = "ScriptableObjects/LevelDataSO", fileName = "LevelDataSO")]
public class LevelDataSO : ScriptableObject
{
    public List<WaveData> pharseDatas;
}
