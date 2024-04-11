using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] audioClips; 
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayAudioSequentially());
    }

    IEnumerator PlayAudioSequentially()
    {
        // Wait a small amount of time to ensure AudioSource has initialized
        yield return null;

        // Iterate through each audio clip and play them sequentially
        for (int i = 0; i < audioClips.Length; i++)
        {
            // Set the clip to the current index and play
            audioSource.clip = audioClips[i];
            audioSource.Play();

            // Wait for the clip to finish playing
            while (audioSource.isPlaying)
            {
                yield return null; // Wait until the clip has finished
            }

            yield return new WaitForSeconds(1);
        }

        // All audio clips have finished playing
        Debug.Log("Finished playing all clips.");
    }
}
