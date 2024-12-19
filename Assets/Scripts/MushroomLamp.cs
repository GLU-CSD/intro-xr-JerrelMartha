using UnityEngine;

public class MushroomLightTrigger : MonoBehaviour
{
    [SerializeField] private Light mushroomLight;
    private void Start()
    {
        mushroomLight.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Geef de XR Rig de tag "Player"
        {
            mushroomLight.enabled = true;
            Debug.Log("Licht aan");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mushroomLight.enabled = false;
            Debug.Log("Licht uit");
        }
    }
}