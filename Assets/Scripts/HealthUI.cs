using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public string prefabTag = "Enemy"; // Ensure your enemy prefab has this tag
    private Health healthScript;

    void Update()
    {
        if (healthScript == null)
        {
            Debug.Log("Attempting to find GameObject with tag: " + prefabTag);
            GameObject spawnedPrefab = GameObject.FindWithTag(prefabTag);

            if (spawnedPrefab != null)
            {
                Debug.Log("GameObject found: " + spawnedPrefab.name);
                healthScript = spawnedPrefab.GetComponent<Health>();

                if (healthScript != null)
                {
                    Debug.Log("Health script successfully retrieved from " + spawnedPrefab.name);
                }
                else
                {
                    Debug.LogError("Health script not found on GameObject: " + spawnedPrefab.name);
                }
            }
            else
            {
                Debug.LogWarning("No GameObject found with the tag: " + prefabTag);
            }
        }
    }
}
