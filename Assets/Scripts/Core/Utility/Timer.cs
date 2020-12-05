using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{

    private float time;

    public Timer()
    {
        time = 0;
    }


    public void Update()
    {
        time += Time.deltaTime;
    }

    public void Update(float scale)
    {
        time += Time.deltaTime * scale;
    }

    public void Regress()
    {
        time -= Time.deltaTime;
        if(time < 0) time = 0;
    }

    public void Reset()
    {
        time = 0;
    }

    public void Reset(float sec)
    {
        time = sec;
    }

    public float GetTime()
    {
        return time;
    }

    public bool HasPastInSec(float seconds)
    {
        return (time >= seconds);
    }

    public bool HasPastInMin(float min)
    {
        const int SECONDS_PER_MIN = 60;
        return HasPastInSec(min * SECONDS_PER_MIN);
    }






}
