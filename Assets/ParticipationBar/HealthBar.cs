using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthSlider : MonoBehaviour
{
    [Header("Health Variables")]
    public PlayButton otherScript;
    public Slider slider;
    public float maxHealth = 100f;
    private float currentHealth;
    public float multi = 1;

    public AudioSource audiosource;
    public AudioSource error;

    [Header("Arrow Stuff")]
    public GameObject arrowPanel; // Assign in inspector
    private List<KeyCode> arrowSequence;
    private int currentArrowIndex;
    public bool ArrowActive = false;
    public Text arrowText;

    [Header("Timer")]
    public PlayButton pb;

    private int sequenceLength = 3; // Initial sequence length
    private int maxLength = 10; // Maximum length of sequence, adjust as needed
    private float sequenceTimer = 0f; // Timer to track time for increasing sequence length
    private float increaseInterval = 10f;

    void Start()
    {
        currentHealth = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = maxHealth;

        // Start the decreasing health coroutine
        StartCoroutine(DecreaseHealthCoroutine());
        arrowSequence = new List<KeyCode> { KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow };
        arrowPanel.SetActive(false);
        
    }

    IEnumerator DecreaseHealthCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            // Checks other script for isPlaying bool
            if (otherScript.isPlaying)
            {

                // Decrease health by 2
                ModifyHealth(-2f* multi);
            }
        }


    }

    public void ModifyHealth(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        slider.value = currentHealth;

        if(currentHealth<=0){
            float highScore = PlayerPrefs.GetFloat("highScore",0);
            float lastScore = pb.GetTime();
            if(lastScore>highScore){
                PlayerPrefs.SetFloat("highScore",lastScore);
            }
            PlayerPrefs.SetFloat("lastScore",lastScore);
            SceneManager.LoadScene("EndScene");
        }
    }

    private void Update()
    {

        

        //Increases amount of letter spawneds
        if (sequenceTimer >= increaseInterval)
        {
            // Increase sequence length if it's less than the maximum length
            if (sequenceLength < maxLength)
            {
                sequenceLength++;
                Debug.Log("Sequence Length Increased to: " + sequenceLength);
            }

            // Reset the timer
            sequenceTimer = 0f;
        }


        //Increases HP Depletion + Checks for Space
        if (otherScript.isPlaying)
        {
            sequenceTimer += Time.deltaTime;
            // Increase multi by a small amount each frame
            multi += 0.001f * Time.deltaTime; // Modify 0.01f to adjust the rate of increment

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!ArrowActive)
                {
                    Debug.Log("Game Ready");
                    StartArrowSequence();
                }

            }

            if (ArrowActive)
            {
                CheckArrowInput();
            }
        }
    }

    void StartArrowSequence()
    {
        arrowSequence = GenerateRandomArrowSequence(sequenceLength);
        UpdateArrowText();
        arrowPanel.SetActive(true); // Show the arrow UI
        ArrowActive = true;
        currentArrowIndex = 0;
    }

    void CheckArrowInput()
    {
        // First, check if the current expected arrow key is pressed down.
        if (Input.GetKeyDown(arrowSequence[currentArrowIndex]))
        {
            currentArrowIndex++;  // Move to the next key in the sequence
            Debug.Log("Correct Key Pressed: " + KeyToString(arrowSequence[currentArrowIndex - 1]));
            //Remove pressed key from text so update text box:

            if (currentArrowIndex < arrowSequence.Count)
            {
                UpdateRemoveArrowText();  // Update the text to show remaining keys
            }

            else
            {
                // If all keys were pressed correctly
                Debug.Log("Sequence Completed Successfully");
                audiosource.Play();
                ArrowActive = false;
                arrowPanel.SetActive(false); // Hide the arrow UI
                ModifyHealth(20f);
            }
        }
        else
        {
            // Check for any incorrect key press from the relevant set
            if (arrowSequence.Exists(k => Input.GetKeyDown(k)))
            {
                Debug.Log("Wrong Key Pressed: Expected " + KeyToString(arrowSequence[currentArrowIndex]));
                error.Play();
                ArrowActive = false;
                arrowPanel.SetActive(false); // Reset and hide the arrow UI
            }
        }
    }


    void UpdateArrowText()
    {
        string text = "";
        foreach (KeyCode key in arrowSequence)
        {
            text += KeyToString(key) + " ";
        }
        arrowText.text = text;
    }

    void UpdateRemoveArrowText()
    {
        string text = "";
        // Only include the keys that haven't been pressed yet:
        for (int i = currentArrowIndex; i < arrowSequence.Count; i++)
        {
            text += KeyToString(arrowSequence[i]) + " ";
        }
        arrowText.text = text;
    }

    string KeyToString(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.LeftArrow:
                return "←";  // Unicode for left arrow
            case KeyCode.RightArrow:
                return "→";  // Unicode for right arrow
            case KeyCode.UpArrow:
                return "↑";  // Unicode for up arrow
            case KeyCode.DownArrow:
                return "↓";  // Unicode for down arrow
            default:
                return key.ToString();
        }
    }

    List<KeyCode> GenerateRandomArrowSequence(int length)
    {
        List<KeyCode> newSequence = new List<KeyCode>();
        KeyCode[] possibleKeys = { KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow };

        for (int i = 0; i < length; i++)
        {
            KeyCode randomKey = possibleKeys[UnityEngine.Random.Range(0, possibleKeys.Length)];
            newSequence.Add(randomKey);
        }

        return newSequence;
    }

}
