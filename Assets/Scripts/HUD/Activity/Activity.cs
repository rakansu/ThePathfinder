using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activity : MonoBehaviour
{
    public delegate void ActivityEvent();
    public event ActivityEvent onStartActivity;
    public event ActivityEvent onStopActivity;

    public void StartActivity() => onStartActivity?.Invoke();
    public void StopActivity() => onStopActivity?.Invoke();

}
