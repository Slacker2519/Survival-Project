using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EnemyWaveData
{
    public EnemyRank rank;
    public EnemyEnum name;
    public int number;
}


[System.Serializable]
public class LevelData 
{
    [SerializeField] int _phaseID;
    [SerializeField] int _maxEnemy;
    [SerializeField] float _spawnInterval;
    [SerializeField] int _spawnAmount;
    [SerializeField] List<EnemyWaveData> _waveEnemyData;

    int PhaseID => _phaseID;
    public int MaxEnemy => _maxEnemy;
    public float SpawnInterval => _spawnInterval;
    public int SpawnAmount => _spawnAmount;
    public List<EnemyWaveData> WaveEnemyData => _waveEnemyData;
}
