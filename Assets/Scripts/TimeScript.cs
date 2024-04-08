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

        StartCoroutine(subtractSecond(difference));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator subtractSecond(TimeSpan ts){
        TimeSpan currentTimeSpan = ts;
        TimeSpan second = new TimeSpan(0,0,1);
        while(TimeSpan.Compare(currentTimeSpan,TimeSpan.Zero)>0){
            currentTimeSpan = currentTimeSpan.Subtract(second);
            timeLeft.text = currentTimeSpan.ToString(@"dd\:hh\:mm\:ss");
            yield return new WaitForSeconds(1f);
        }
        yield return null;
    }
}
