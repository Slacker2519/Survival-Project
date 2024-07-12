using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EnemyWaveData
{
    public EnemyRank rank;
    public int number;
}


[System.Serializable]
public class WaveData 
{
    //[SerializeField] int _phaseID;
    //int PhaseID => _phaseID;

    [SerializeField] List<EnemyWaveData> _waveEnemyData;
    public List<EnemyWaveData> WaveEnemyData => _waveEnemyData;

    //[SerializeField] float _waveDuration;
    //public float WaveDuration => _waveDuration;

    [SerializeField] int _maxEnemy;
    public int MaxEnemy => _maxEnemy;

    [SerializeField] float _spawnInterval;
    public float SpawnInterval => _spawnInterval;

    [SerializeField] int _spawnAmount;
    public int SpawnAmount => _spawnAmount;
}

[Serializable]
public class LevelData
{
    [SerializeField] private List<WaveData> _waveDataList;
    public List<WaveData> WaveDataList => _waveDataList;
}
