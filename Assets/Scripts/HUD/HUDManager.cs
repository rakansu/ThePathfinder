using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public static HUDManager current;

    private List<Activity> activities = new List<Activity>();
    private Activity current_activity; 

    [Header("-- HUD CONFIG --")]
    [SerializeField] Activity start_activity;
    [SerializeField] bool startWithLoadScreenTransition;



    void Awake()
    {
        current = this;
        InitializeActivities();
        HideActivities();
        if(start_activity == null) Debug.LogError("Start Activity was not set!");
        // Set the focus to the start activity:
        SwitchActivity(start_activity);
    }

    /// <summary>
    /// Ensures each activity is initialized 
    /// </summary>
    private void InitializeActivities()
    {
        Activity[] activity_set = GetComponentsInChildren<Activity>(true);
        for(int i = 0; i < activity_set.Length; i++)
        {
            activities.Add(activity_set[i]);
            activities[i].gameObject.SetActive(true);
        }
    }


    /// <summary>
    /// Switch from the current activity to the new activity
    /// </summary>
    public void SwitchActivity(Activity activity)
    {
        if(activity == null) return;
        if(current_activity != null) current_activity.StopActivity();
        HideActivities();
        current_activity = activity;
        current_activity.gameObject.SetActive(true);
        activity.StartActivity();
    }


    /// <summary>
    /// Hide all activities
    /// </summary>
    private void HideActivities()
    {
        for(int i = 0; i < activities.Count; i++) 
            activities[i].gameObject.SetActive(false);
    }


   
}
