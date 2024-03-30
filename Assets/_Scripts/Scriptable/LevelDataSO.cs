using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct PharseData
{
    public int PharseID;
    public EnemyEnum enemyName;
    public float startTime;
    public int MaxEnemy;
    public float SpawnInterval;
    public int SpawnAmount;
}
[CreateAssetMenu(menuName = "ScriptableObjects/LevelDataSO", fileName = "LevelDataSO")]
public class LevelDataSO : ScriptableObject
{
    public List<PharseData> pharseDatas;
}
