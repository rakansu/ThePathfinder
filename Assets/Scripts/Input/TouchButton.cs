using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{

    public delegate void TouchButtonEvent();
    public delegate void TouchButtonEventFloat(float sec);

    public event TouchButtonEvent onEnter;
    public event TouchButtonEvent onClick;
    public event TouchButtonEvent onTap;
    public event TouchButtonEventFloat onRelease;
    public event TouchButtonEventFloat onHolding;


    
    private const float tapThreshold = 0.2f;
    private float elapsedTime = 0;
    private int pointerID = -1;
    private bool isPointerDetected = false;



    void Update()
    {
        if (!isPointerDetected) return;

        if(IsHolding() && elapsedTime > tapThreshold) onHolding?.Invoke(elapsedTime);
        elapsedTime += Time.deltaTime;
    }




    public void OnPointerDown(PointerEventData eventData)
    {
        if (isPointerDetected) return;

        onEnter?.Invoke();
        isPointerDetected = true;
        pointerID = eventData.pointerId;
        elapsedTime = 0;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isPointerDetected) return;
        if (isPointerDetected && eventData.pointerId == pointerID) isPointerDetected = false;
        if (elapsedTime < tapThreshold) onTap?.Invoke();
        onClick?.Invoke();
        onRelease?.Invoke(elapsedTime);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isPointerDetected && eventData.pointerId == pointerID) isPointerDetected = false;
    }









    public bool HasHeldFor(float sec)
    {
        return (elapsedTime >= sec);
    }

    public bool IsHolding()
    {
        return isPointerDetected;
    }

    public float GetHoldTime()
    {
        return elapsedTime;
    }



}
