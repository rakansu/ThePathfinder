using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ThePathfinder;

public class TouchInput : MonoBehaviour
{
    public static TouchInput current;

    public delegate void TouchInputEvent(int count);
    public delegate void TouchInputEventI(float sec);
    public delegate void TouchInputEventII(Direction touchDirection);
    public delegate void TouchInputEventIII(Touch touch);
    public delegate void Event();

    public event TouchInputEventIII onTouchEnter;
    public event TouchInputEventI onTouchEnd;
    public event TouchInputEventII onSwipe;
    public event TouchInputEvent onTap;
    public event Event onTouchUpdate;
    


    private float timeElapsed;
    private int tapCount = 0;
    private const float threshold = 10;
    private const float tapThreshold = 0.2f;
    private int fingerID;
    private bool isTouchDetected;

    private Vector2 startPos;
    private Vector2 currentPos;

    private Timer tap_timer = new Timer();




    void Awake()
    {
        current = this;
    }


    void Update()
    {
        if(tap_timer.HasPastInSec(tapThreshold)) tapCount = 0;
        else tap_timer.Update();

        if (Input.touchCount == 0) return;


        // OnTouchEnter -- Detect the unique touch
        int count = 0;
        while (!isTouchDetected && count < Input.touchCount)
        {
            Touch touch = Input.touches[count];


            if (touch.phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                OnTouchEnter(touch);
            }
            count++;
        }



        if (!isTouchDetected) return;
        

        for(int n = 0; n < Input.touchCount; n++)
        {
            Touch touch = Input.touches[n];

            if(touch.fingerId == fingerID)
            {
                timeElapsed += Time.deltaTime;
                if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    if (timeElapsed < tapThreshold) 
                    {
                        tapCount++;
                        tap_timer.Reset();
                        onTap?.Invoke(tapCount);
                    }
                    OnTouchEnd();
                }
                if (touch.phase == TouchPhase.Moved)
                {
                    currentPos = touch.position;
                    // float magnitude = (touch.position - startPos).sqrMagnitude;
                    // if (magnitude >= threshold * threshold) OnSwipe(touch);
                }
                onTouchUpdate?.Invoke();
            }
        }

    }





    private void OnTouchEnter(Touch touch)
    {
        timeElapsed = 0;
        fingerID = touch.fingerId;
        startPos = touch.position;
        currentPos = startPos;
        isTouchDetected = true;
        onTouchEnter?.Invoke(touch);
    }

    private void OnTouchEnd()
    {
        isTouchDetected = false;
        onTouchEnd?. Invoke(timeElapsed);
        timeElapsed = 0;
    }


    private void OnSwipe(Touch touch)
    {
        Vector2 vector = touch.position - startPos;

        float angle = Mathf.Atan2(vector.y, vector.x);
        angle *= Mathf.Rad2Deg;
        if (angle < 0) angle = 360 + angle;

        Direction direction;

        if ((0 <= angle && angle <= 45) || (315 <= angle && angle <= 360)) direction = Direction.Right;
        else if (45 <= angle && angle <= 135) direction = Direction.Up;
        else if (135 <= angle && angle <= 225) direction = Direction.Left;
        else direction = Direction.Down;
        
        onSwipe?.Invoke(direction);
        OnTouchEnd();
    }

    public Vector2 GetTouchStartPosition() => startPos;
    public Vector2 GetTouchCurrentPosition() => currentPos;


    public bool IsHolding()
    {
        return isTouchDetected;
    }


    public bool HasHeldFor(float sec)
    {
        return (timeElapsed >= sec);
    }


}
