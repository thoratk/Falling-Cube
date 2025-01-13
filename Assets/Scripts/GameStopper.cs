using UnityEngine;
using TMPro;

public class GameStopper : MonoBehaviour
{
    // Time limit in seconds
    public float timeLimit = 30f;
    private float timer;
    public TextMeshProUGUI gameOverText;
    public GameObject Cubecatcher;

    public TextMeshProUGUI timerText;
    void Start()
    {
        timer = timeLimit; // Set timer to the time limit at the start
        gameOverText.enabled = false;
    }

    void Update()
    {
        // Count down the timer
        timer -= Time.deltaTime;

        if (timerText != null)
        {
            timerText.text = "Time Left: " + Mathf.Ceil(timer).ToString() + "s"; // Update the text
        }

        // If time is up, stop the game
        if (timer <= 0f)
        {
            Time.timeScale = 0f; // Stop the game (pause)
            Debug.Log("Game stopped after 30 seconds!");
            gameOverText.enabled = true;
            Cubecatcher.GetComponent<CubeController>().enabled = false;
        }
    }
}
