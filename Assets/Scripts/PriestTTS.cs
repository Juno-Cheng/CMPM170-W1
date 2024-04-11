using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriestTTS : MonoBehaviour
{
    [Header("Game Objects")]
    public Animator animator; // Reference to the Animator component
    public PlayButton otherScript; // Reference to the other script where isPlaying is defined
    private bool coroutineStarted = false;

    [Header("Values")]
    float moveDuration = 10f;
    float speed = 100f;

    void Start()
    {
        // Set both isWalking and Reading parameters to false initially
        animator.SetBool("IsWalking", false);
        animator.SetBool("Reading", false);
    }

    void Update()
    {
        // Check if the isPlaying variable from the other script is true
        if (otherScript.isPlaying && !coroutineStarted)
        {
            Debug.Log("Walking");
            StartCoroutine(WalkAndRead());
            coroutineStarted = true; // Ensure coroutine doesn't start again
        }

    }

    IEnumerator WalkAndRead()
    {
        // Start walking animation
        animator.SetBool("IsWalking", true);
        animator.SetBool("Reading", false);

        // Move to the left for an amount of seconds
        float startTime = Time.time;
        while (Time.time - startTime < moveDuration)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime); // Adjust this value as necessary for speed
            yield return null;
        }

        // Stop walking and start reading
        animator.SetBool("IsWalking", false);
        animator.SetBool("Reading", true);
    }
}
