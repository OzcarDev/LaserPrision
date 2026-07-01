using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Laser Prison/Difficulty Settings")]
public class DifficultySettings : ScriptableObject
{
    public DifficultyLevel[] Levels;
}

[Serializable]
public class DifficultyLevel
{
    public float startTime;

    public float spawnInterval = 2.5f;

    public int lasersPerWave = 1;

    public int targetedLasers = 0;
}