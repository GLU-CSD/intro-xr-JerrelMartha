using UnityEngine;
using System.Collections;

public class FirstPersonMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float lookSpeedX = 2f;
    public float lookSpeedY = 2f;
    public float explosionDelay = 1f;
    public float explosionRadius = 3f;
    public Transform playerCamera;

    // Grenade throwing variables
    public GameObject grenadePrefab;  // Grenade prefab
    public Transform throwPoint;      // Point from where the grenade is thrown
    public float throwForce = 10f;    // Throw force for the grenade
    public GameObject explosionEffectPrefab;  // Particle effect to spawn on grenade explosion

    private float rotationX = 0f;

    void Start()
    {
        // Hide and lock the cursor at the start of the game
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Handle Grenade Throwing
        if (Input.GetKeyDown(KeyCode.G))  // You can change the key here
        {
            ThrowGrenade();
        }

        // Check if Shift key is held down to increase speed
        float currentMoveSpeed = moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            currentMoveSpeed *= 2f; // Double the movement speed
        }

        // Handle Movement (Only when pressing WASD)
        float moveX = 0f;
        float moveZ = 0f;

        // Check for movement input (WASD keys)
        if (Input.GetKey(KeyCode.W)) moveZ = 1f;
        if (Input.GetKey(KeyCode.S)) moveZ = -1f;
        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        if (Input.GetKey(KeyCode.D)) moveX = 1f;

        // Normalize to prevent faster diagonal movement
        Vector3 forwardDirection = playerCamera.forward;
        Vector3 rightDirection = playerCamera.right;

        forwardDirection.y = 0f; // Prevent vertical movement in the world space
        rightDirection.y = 0f;

        // Create a movement vector based on the input and camera's orientation
        Vector3 movement = (playerCamera.forward * moveZ) + (rightDirection * moveX);

        // Normalize the movement vector
        movement.Normalize();

        // Apply the movement vector to the player
        transform.Translate(movement * currentMoveSpeed * Time.deltaTime, Space.World);

        // Handle Looking around (mouse movement)
        float mouseX = Input.GetAxis("Mouse X") * lookSpeedX;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeedY;

        // Rotate the player horizontally
        transform.Rotate(0, mouseX, 0);

        // Handle vertical rotation (clamping to prevent over-rotation)
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        // Apply the vertical rotation to the camera
        playerCamera.localRotation = Quaternion.Euler(rotationX, 0, 0);
    }

    void ThrowGrenade()
    {
        if (grenadePrefab != null && throwPoint != null)
        {
            // Instantiate the grenade
            GameObject grenade = Instantiate(grenadePrefab, throwPoint.position, throwPoint.rotation);

            // Get the Rigidbody component and apply force
            Rigidbody rb = grenade.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(throwPoint.forward * throwForce, ForceMode.VelocityChange);
            }

            // Start the explosion coroutine after the grenade is thrown
            StartCoroutine(Explode(grenade)); // Pass the grenade object itself
        }
    }

    private IEnumerator Explode(GameObject grenade)
    {
        // Wait for the grenade to explode after the delay
        yield return new WaitForSeconds(explosionDelay);

        // Get the final position of the grenade before explosion
        Vector3 grenadePosition = grenade.transform.position;

        // Spawn the particle effect at the grenade's position
        Instantiate(explosionEffectPrefab, grenadePosition, Quaternion.identity);

        // Check for any colliders within the explosion radius
        Collider[] enemiesInRange = Physics.OverlapSphere(grenadePosition, explosionRadius);

        // Debug log the amount of enemies within the explosion radius
        Debug.Log("Enemies hit: " + enemiesInRange.Length);

        // Optionally, apply damage or effects to the enemies here

        // Destroy the grenade after explosion
        Destroy(grenade);
    }
}

