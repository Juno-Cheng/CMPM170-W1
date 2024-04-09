using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] public GameObject MainMenu;
    [SerializeField] public GameObject Checker;
    [SerializeField] private bool Sunday;
    [SerializeField] private bool SinnerSundayBypass;
    [SerializeField] private bool isPlaying;
    // Start is called before the first frame update
    void Start()
    {
        Sunday = DateTime.Now.DayOfWeek == DayOfWeek.Sunday;
        Debug.Log("Is today Sunday? " + Sunday);
        isPlaying = false;


    }

    public void onPlay()
    {
        //If being Played on Sunday
        if (Sunday || SinnerSundayBypass)
        {
            isPlaying = true;
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


}
