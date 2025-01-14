using System;
using System.Diagnostics;
using System.Numerics;
using UnityEngine;

public class ShapeDropper : MonoBehaviour
{
    public GameObject[] shapes; // Assign different shapes in the inspector
    public Transform spawnPoint; // Position at the top of the screen for spawning shapes
    public float spawnInterval = 2f; // Time between shape spawns
    public int positiveScore = 10; // Score for catching positive shapes
    public int negativeScore = -5; // Score for catching negative shapes
    public int currentScore = 0; // Starting score

    private float nextSpawnTime;

    void Update()
    {
        // Spawn shapes at intervals
        if (Time.time >= nextSpawnTime)
        {
            SpawnShape();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnShape()
    {
        int randomIndex = UnityEngine.Random.Range(0, shapes.Length);
        Instantiate(shapes[randomIndex], spawnPoint.position, UnityEngine.Quaternion.identity);
    }

    public void UpdateScore(bool isPositive)
    {
        currentScore += isPositive ? positiveScore : negativeScore;
        UnityEngine.Debug.Log("Current Score: " + currentScore);
    }
}
