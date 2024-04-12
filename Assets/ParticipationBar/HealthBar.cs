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
            yield return new WaitForSeconds(1f); // Wait for 1 second

            // Check if isPlaying is true (replace this condition with your actual condition)
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
            // Add audio things here so when player press Space,
            // they heal and also say stuff like amen or something.

            // Increase health by 20
            ModifyHealth(20f);
        }
    }
}
