using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class TimeScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Vector3 churchTimeSpan;
    //[SerializeField] TextMeshProUGUI todayDate;
    [SerializeField] TextMeshProUGUI nextSundayDate;
    [SerializeField] TextMeshProUGUI timeLeft;
    int totalSeconds;
    void Start()
    {
        DateTime currentDate = DateTime.Now;
        //todayDate.text = currentDate.ToString(new CultureInfo("en-us"));

        //https://stackoverflow.com/questions/6346119/compute-the-datetime-of-an-upcoming-weekday
        int daysUntilSunday = ((int) DayOfWeek.Sunday - (int) currentDate.DayOfWeek + 7) % 7;
        DateTime nextSunday = currentDate.AddDays(daysUntilSunday);
        nextSunday = nextSunday.Date.Add(new TimeSpan((int)churchTimeSpan.x,(int)churchTimeSpan.y,(int)churchTimeSpan.z));
        nextSundayDate.text = nextSunday.ToString(new CultureInfo("en-us"));


        TimeSpan difference = nextSunday-currentDate;
        //https://learn.microsoft.com/en-us/dotnet/standard/base-types/custom-timespan-format-strings
        //Debug.Log(difference.ToString(@"dd\:hh\:mm\:ss"));

        totalSeconds = (int) difference.TotalSeconds;

        StartCoroutine(StartCountdown());
    }

    // Update is called once per frame
    public bool mouseOver = false;
    bool clickedOver = false;
    bool speedUp = false;
    float timePassed = 0;
    void Update()
    {
        Debug.Log(clickedOver);
        if(clickedOver&&Input.GetMouseButton(0)){
            speedUp = true;
        }
        if(Input.GetMouseButtonUp(0)){
            speedUp = false;
            clickedOver = false;
        }
        //Debug.Log((mouseOver,speedUp,timePassed));
        SpeedUpTime();
        if(totalSeconds>0){
            timeLeft.text = SecondToString(totalSeconds);
        }
        if(totalSeconds<0){
            timeLeft.text = SecondToString(0);
        }
    }

    public void SetMouseOver(bool state){
        mouseOver = state;
    }
    public void CheckClickedOver(){
        if(mouseOver){
            clickedOver = true;
        }
    }

    string SecondToString(int sec){
        int days = sec/86400;
        int hours = (sec%86400)/3600;
        int mins = (sec%3600)/60;
        int secs = sec%60;
        return string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D2}",days,hours,mins,secs);
    }

    IEnumerator StartCountdown(){
        while(totalSeconds>0){
            totalSeconds -=1;
            yield return new WaitForSeconds(1f);
        }
        yield return null;
    }

    void SpeedUpTime(){
        if(speedUp == true){
            timePassed += Time.deltaTime;
        }
        else{
            timePassed = 0f;
        }
        totalSeconds -= (int) Mathf.Pow((timePassed),4);
        
    }
}
