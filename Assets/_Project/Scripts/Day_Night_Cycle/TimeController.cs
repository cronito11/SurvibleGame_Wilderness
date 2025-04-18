using Surviblewilderness;
using System;
using TMPro;
using UnityEngine;

public enum TimeOfDay
{
    Day,
    Night
}   
public class TimeController : MonoBehaviour
{
    public static event Action<TimeOfDay> OnChangeTimeOfDay;
    public static event Action OnAnHourPassed;

    [Header("Time Variables")]
    [SerializeField] private float timeMultiplier; // Controls how fast time passes in the game
    [SerializeField] private float startHour; // The hour that the game starts with (e.g., 6 for 6 AM)
    [SerializeField] private TextMeshProUGUI timeText; // Reference to UI element to display the time
    private DateTime currentTime; // Holds the current in-game time as a DateTime object

    public DateTime DateTime => currentTime; // Public property to access the current in-game time

    private TimeSpan sunriseTime;
    private TimeSpan sunsetTime;
    [SerializeField]private TimeOfDay currentTimeOfDay = TimeOfDay.Day;

    [Header("Light Variables")]
    [SerializeField] private Light sunLight;
    [SerializeField] private float maxSunLightIntensity;
    [SerializeField] private Light moonLight;
    [SerializeField] private float maxMoonLightIntensity;
    [SerializeField] private Color dayAmbientLight;
    [SerializeField] private Color nightAmbientLight;
    [SerializeField] private float sunriseHour;
    [SerializeField] private float sunsetHour;


    [SerializeField] private AnimationCurve lightChangeCurve;

    private float lastHourCheck = 0f; // Used to track the last time an hour was checked
    private float hour = 1;

    private void OnEnable()
    {
        UiManager.OnGameStart += OnGameStart;
    }
    private void OnDisable()
    {
        UiManager.OnGameStart -= OnGameStart;
    }

    private void Start()
    {
        /* 
         Set the initial in-game time to the current date at the specified startHour
            DateTime.Now.Date: gives today's date with the time set to midnight (00:00:00)
            TimeSpan.FromHours(startHour): adds the specified startHour to set the time (e.g., 6 AM)
        */
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);

        // Setiing the sunrise and sunset initial hour
        sunriseTime = TimeSpan.FromHours(sunriseHour);
        sunsetTime = TimeSpan.FromHours(sunsetHour);

    }

    private void Update()
    {
        UpdateTimeOfDay();
        RotateSun();
        UpdateLightSettings();
    }

    private void OnGameStart()
    {
        OnChangeTimeOfDay?.Invoke(currentTimeOfDay);
    }

    private void AfterAnHourTriggerQuestUpdateEvent(DateTime _currentTime)
    {
        //calculate and check if one hour has been passed or not 
        //if yes then fire an event that will be used to update active quests related to survival time
        
        if (Mathf.Abs(_currentTime.Hour - lastHourCheck) >= hour)
        {
            //one hour has passed
            Debug.Log("One hour has passed");   
            lastHourCheck = _currentTime.Hour;
            OnAnHourPassed?.Invoke();
        }
    }

    private void UpdateTimeOfDay()
    {
        /*
         Add seconds to currentTime based on real-world time passed (scaled by timeMultiplier)
            Time.deltaTime: gives the time passed since the last frame (in seconds)
            Multiply by timeMultiplier to control the speed of time in the game
        */
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);

        AfterAnHourTriggerQuestUpdateEvent(currentTime);

        // Check if the timeText is not null
        if (timeText != null)
        {
            // Assign the current time to the timeText in 24 hr format 
            timeText.text = currentTime.ToString("HH:mm");
        }

        if(currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime)
        {
            if(currentTimeOfDay == TimeOfDay.Night)
            {
                OnChangeTimeOfDay?.Invoke(TimeOfDay.Day);
                currentTimeOfDay = TimeOfDay.Day;
                Debug.Log("Time of day changed to Day");    
            }
        }
        else
        {
            if(currentTimeOfDay == TimeOfDay.Day)
            {
                OnChangeTimeOfDay?.Invoke(TimeOfDay.Night);
                currentTimeOfDay = TimeOfDay.Night;
                Debug.Log("Time of day changed to Night");
            }
        }
    }

    private void RotateSun()
    {
        float sunLightRotation;
        if (currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime)
        {
            TimeSpan sunriseToSunsetDuration = CalculateTimeDifference(sunriseTime, sunsetTime);
            TimeSpan timeSinceSunrise = CalculateTimeDifference(sunriseTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(0, 180, (float)percentage);
        }
        else
        {
            TimeSpan sunsetToSunriseDuration = CalculateTimeDifference(sunsetTime, sunriseTime);
            TimeSpan timeSinceSunset = CalculateTimeDifference(sunsetTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunset.TotalMinutes / sunsetToSunriseDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(180, 360, (float)percentage);
        }

        sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.right);
    }

    private void UpdateLightSettings()
    {
        float dotProduct = Vector3.Dot(sunLight.transform.forward, Vector3.down);
        sunLight.intensity = Mathf.Lerp(0, maxSunLightIntensity, lightChangeCurve.Evaluate(dotProduct));
        moonLight.intensity = Mathf.Lerp(maxMoonLightIntensity, 0, lightChangeCurve.Evaluate(dotProduct));
        RenderSettings.ambientLight = Color.Lerp(nightAmbientLight, dayAmbientLight, lightChangeCurve.Evaluate(dotProduct));
    }

    private TimeSpan  CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan difference = toTime - fromTime;

        if (difference.TotalSeconds < 0)
        {
            difference += TimeSpan.FromHours(24);
        }

        return difference;
    }
    
    
}
