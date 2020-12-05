using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThePathfinder;

public static class JobSystem
{

    private struct EventInfo
    {
        public EventInfo(JobAction jobAction, float stamp, float duration) 
        {
            this.jobAction = jobAction;
            this.current_stamp = stamp;
            this.duration = duration;
        }
        public JobAction jobAction;
        public float current_stamp;
        public float duration;
    }

    private static List<EventInfo> scheduled_events = new List<EventInfo>();
    private static List<EventInfo> executable_events = new List<EventInfo>();

    private static List<int> schedule_buffer = new List<int>();
    private static List<int> execute_buffer  = new List<int>();



    public static void Update()
    {
        // Updatable Events:
        for(int i = 0; i < executable_events.Count; i++)
        {
            EventInfo eventInfo = executable_events[i];
            eventInfo.current_stamp += Time.unscaledDeltaTime;
            if (eventInfo.current_stamp > eventInfo.duration)
            {
                eventInfo.jobAction(eventInfo.current_stamp, true);
                execute_buffer.Add(i);
            } else 
            {
                eventInfo.jobAction(eventInfo.current_stamp, false);
                executable_events[i] = new EventInfo(eventInfo.jobAction,eventInfo.current_stamp, eventInfo.duration);
            }
        }

        // Scheduled Events:
        for(int i = 0; i < scheduled_events.Count; i++)
        {
            EventInfo eventInfo = scheduled_events[i];
            eventInfo.current_stamp += Time.unscaledDeltaTime;
            if (eventInfo.current_stamp > eventInfo.duration)
            {
                eventInfo.jobAction(eventInfo.current_stamp, true);
                schedule_buffer.Add(i);
            } else scheduled_events[i] = new EventInfo(eventInfo.jobAction,eventInfo.current_stamp, eventInfo.duration);
        }

        for(int i = schedule_buffer.Count - 1; i >= 0; i--) scheduled_events.RemoveAt(schedule_buffer[i]);
        for(int i = execute_buffer.Count - 1; i >= 0; i--) executable_events.RemoveAt(execute_buffer[i]);
        schedule_buffer.Clear();
        execute_buffer.Clear();
    }


    /// <summary>
    /// Delays the execution of a function for [duration] in seconds
    /// </summary>
    public static void ScheduleUntil(JobAction jobAction, float duration)
    {
        EventInfo eventInfo = new EventInfo(jobAction,0,duration);
        scheduled_events.Add(eventInfo);
    }



    /// <summary>
    /// Executes a function for [duration] in seconds
    /// </summary>
    public static void ExecuteUntil(JobAction jobAction, float duration)
    {
        EventInfo eventInfo = new EventInfo(jobAction,0,duration);
        executable_events.Add(eventInfo);
    }







}
