using TMPro;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public float speed = 5f; // Speed of the cube's movement
    private float leftBound, rightBound; // Boundaries for the cube's movement
    public int score = 0; // Score counter
    public TextMeshProUGUI scoreText; // UI text element to display score

    // Object falling from the top
    public GameObject fallingObjectPrefab; // Falling object prefab (e.g., cube)
    public Transform[] spawnPoints; // Array of different spawn points for falling objects
    public float fallSpeed = 5f; // Speed of the falling object
    public float spawnInterval = 1f; // Time between shape spawns
    private float nextSpawnTime;

    private void Start()
    {
        // Set boundaries for cube movement
        Camera cam = Camera.main;
        float halfWidth = cam.orthographicSize * cam.aspect; // Half of the screen width
        leftBound = -halfWidth + transform.localScale.x / 2;  // Left boundary (adjust for cube's width)
        rightBound = halfWidth - transform.localScale.x / 2;  // Right boundary (adjust for cube's width)

        // Spawn the first falling object
        SpawnFallingObject();

        // Initialize score display (if you want to show score in the UI)
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnFallingObject();
            nextSpawnTime = Time.time + spawnInterval;
        }
        
        // Move the cube to the left when the left arrow key is pressed
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Move(Vector3.left);
        }

        // Move the cube to the right when the right arrow key is pressed
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Move(Vector3.right);
        }
    }

    void Move(Vector3 direction)
    {
        // Calculate the new position of the cube
        Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;

        // Ensure the cube stays within the left and right boundaries
        newPosition.x = Mathf.Clamp(newPosition.x, leftBound, rightBound);

        // Apply the new position to the cube
        transform.position = newPosition;
    }

    // Trigger event when the cube enters a trigger zone
    private void OnTriggerEnter(Collider other)
    {
        // Check if the cube entered the correct trigger zone and collided with the falling object
        if (other.CompareTag("Shape"))
        {
            // Check the color of the falling object (either red or green)
            Renderer objectRenderer = other.GetComponent<Renderer>();
            if (objectRenderer != null)
            {
                if (objectRenderer.material.color == Color.red)
                {
                    // If the object is red, subtract points
                    score -= 5;
                    Debug.Log("Caught a red object! Score: " + score);
                }
                else if (objectRenderer.material.color == Color.green)
                {
                    // If the object is green, add points
                    score += 10;
                    Debug.Log("Caught a green object! Score: " + score);
                }
            }

            // Update the score UI
            if (scoreText != null)
            {
                scoreText.text = "Score: " + score;
            }

            // Destroy the caught falling object
            Destroy(other.gameObject);

            // Spawn a new falling object at a random spawn point
            
        }
    }

    // Spawn a new falling object at a random spawn point
    private void SpawnFallingObject()
    {
        if (spawnPoints.Length == 0) return;

        // Randomly choose a spawn point from the array
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Instantiate a new falling object at the chosen spawn point

       GameObject newFallingObject = Instantiate(fallingObjectPrefab, randomSpawnPoint.position, Quaternion.identity);

        // Randomly assign a color (red or green) to the object
        Renderer objectRenderer = newFallingObject.GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            if (Random.value > 0.5f)
            {
                objectRenderer.material.color = Color.red;
            }
            else
            {
                objectRenderer.material.color = Color.green;
            }
        }

        // Add velocity to make it fall
        Rigidbody rb = newFallingObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.down * fallSpeed; // Apply downward velocity
        }
    }
}
