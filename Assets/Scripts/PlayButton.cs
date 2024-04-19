using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class PlayButton : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] public GameObject MainMenu;
    [SerializeField] public GameObject Checker;
    [SerializeField] private bool Sunday;
    [SerializeField] private bool SinnerSundayBypass;
    [SerializeField] public bool isPlaying;
    
    [SerializeField] TextMeshProUGUI timerText;
    float currentTime;
    bool timeIsRunning;
    // Start is called before the first frame update
    void Start()
    {
        Sunday = DateTime.Now.DayOfWeek == DayOfWeek.Sunday;
        Debug.Log("Is today Sunday? " + Sunday);
        isPlaying = false;


    }

    void Update(){
        if (timeIsRunning){
            currentTime += Time.deltaTime;
            DisplayTime(currentTime);
        }
    }

    public void onPlay()
    {
        //If being Played on Sunday
        if (Sunday || SinnerSundayBypass)
        {
            isPlaying = true;
            timeIsRunning = true;
            timerText.enabled = true;
            MainMenu.SetActive(false);
        }
        else
        {
            //Disable Menu & Enable Checker
            MainMenu.SetActive(false);
            Checker.SetActive(true);
        }
    }

    public void ToggleChanged(bool isOn)
    {
        SinnerSundayBypass = isOn;
        Debug.Log("Toggle state is now: " + isOn);
    }

    void DisplayTime(float timeToDisplay){
        timerText.text = string.Format("{0:F2}",currentTime);
    }

    public float GetTime(){
        return currentTime;
    }
    public void ResetScores(){
        PlayerPrefs.DeleteAll();
    }
}
