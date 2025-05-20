using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NameInputHandler : MonoBehaviour
{
    public TMP_InputField nameInputField;  // Input field for typing name
    public TMP_Text displayNameText;       // Displays live input
    public Button submitButton;            // Submit button
    public GameObject stampObject;         // Object with stamp animation
    public TMP_Text finalNameText;         // Displays "PROCESSED MEAT"
    public string sceneToLoad;             // Scene to load after delay

    private bool nameSubmitted = false;

    void Start()
    {
        nameInputField.characterLimit = 12;
        nameInputField.onValueChanged.AddListener(UpdateDisplayName);
        submitButton.onClick.AddListener(SubmitName);

        stampObject.SetActive(false);
        finalNameText.gameObject.SetActive(false);
    }

    void UpdateDisplayName(string input)
    {
        if (!nameSubmitted)
            displayNameText.text = input;
    }

    void SubmitName()
    {
        if (nameSubmitted || string.IsNullOrWhiteSpace(nameInputField.text)) return;

        nameSubmitted = true;

        stampObject.SetActive(true);

        // After stamp animation finishes, override name
        Invoke(nameof(OverrideName), 1.5f);
    }

    void OverrideName()
    {
        displayNameText.gameObject.SetActive(false);
        finalNameText.gameObject.SetActive(true);
        finalNameText.text = "PROCESSED MEAT";

        // Wait 3 seconds then change scene
        Invoke(nameof(LoadNextScene), 3f);
    }

    void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("No scene name specified in 'sceneToLoad'.");
        }
    }
}
