using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    public PlayButton otherScript;
    public Slider slider;
    public float maxHealth = 100f;
    private float currentHealth;

    public AudioSource audiosource;

    void Start()
    {
        currentHealth = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = maxHealth;

        // Start the decreasing health coroutine
        StartCoroutine(DecreaseHealthCoroutine());
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
                ModifyHealth(-2f);
            }
        }
    }

    public void ModifyHealth(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        slider.value = currentHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Says Amen when space pressed, won't play if already playing
            if (!audiosource.isPlaying)
            {
                audiosource.Play();
            }

            // Increase health by 20
            ModifyHealth(20f);
        }
    }
}
