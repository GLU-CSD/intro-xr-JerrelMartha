using UnityEngine;

public class SpawnPrefab : MonoBehaviour
{
    public GameObject prefabToSpawn; // The prefab to spawn
    public Transform spawnPoint;     // The transform that defines the spawn location
    public float spawnDelay = 2f;    // Delay before spawning the prefab (optional)

    void Start()
    {
        // Optionally, spawn the prefab right when the game starts
        Invoke("Spawn", spawnDelay);
    }

    void FixedUpdate()
    {
        Invoke("Spawn", spawnDelay);
    }

    void Spawn()
    {
        if (prefabToSpawn != null && spawnPoint != null)
        {
            // Instantiate the prefab at the spawn point's position and rotation
            Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogError("Prefab or Spawn Point is not assigned.");
        }
    }
}
