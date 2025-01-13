using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
   
    public Button startButton;  // Reference to the button

    // Start is called before the first frame update
    void Start()
    {
        // Add a listener to the button to detect clicks
        startButton.onClick.AddListener(OnStartButtonClick);
    }

    // Method called when the button is clicked
    void OnStartButtonClick()
    {
        // Load the desired scene
        SceneManager.LoadScene(1);  // Replace with your scene's name
    }
}
