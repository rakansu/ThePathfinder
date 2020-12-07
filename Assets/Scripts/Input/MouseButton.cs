using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ThePathfinder;

public class MouseButton : MonoBehaviour, IPointerEnterHandler,  IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public delegate void MouseEvent();
    public delegate void MouseEventKey(MouseKey mouseKey);
    public delegate void MouseEventKeyFloat(MouseKey mouseKey, float sec);

    public event MouseEvent onCursorEnter;              
    public event MouseEvent onCursorExit;               
    public event MouseEventKey onButtonDown;            
    public event MouseEventKey onButtonUp;              
    public event MouseEventKey onButtonTap;             
    public event MouseEventKey onButtonClick;           
    public event MouseEventKey onButtonCancelClick;     
    public event MouseEventKeyFloat onButtonReleased;   // TO-DO
    public event MouseEventKeyFloat onButtonHolding;    // TO-DO

    private const float tapThreshold = 0.2f;
    private float elapsedTime = 0f;
    private bool isPointerOverObject = false;
    private bool isPointerDown = false;


    void Update()
    {
        if(!isPointerDown) return;

        elapsedTime += Time.deltaTime;
    }

    /// <summary>
    /// Called if pointer enters the bounds of the object
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOverObject = true;
        onCursorEnter?.Invoke();
    }

    /// <summary>
    /// Called if pointer exits the bounds of the object
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOverObject = false;
        onCursorExit?.Invoke();
    }


    /// <summary>
    /// Called if pointer was pressed when pointer is on top of the object
    /// </summary>
    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;
        elapsedTime = 0f;
        onButtonDown?.Invoke(GetMouseKey(eventData.button));
    }

    /// <summary>
    /// Called if successfully triggered OnPointerDown
    /// </summary>
    public void OnPointerUp(PointerEventData eventData)
    {
        isPointerDown = false;
        onButtonUp?.Invoke(GetMouseKey(eventData.button));
        if(!isPointerOverObject) onButtonCancelClick?.Invoke(GetMouseKey(eventData.button));
    }

    /// <summary>
    /// Called if successfully triggered OnPointerDown and pointer is within object bounds
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        if(elapsedTime < tapThreshold) onButtonTap?.Invoke(GetMouseKey(eventData.button));
        onButtonClick?.Invoke(GetMouseKey(eventData.button));
    }

  
    private MouseKey GetMouseKey(PointerEventData.InputButton pointerButton)
    {
        switch (pointerButton)
        {
            case PointerEventData.InputButton.Left :  return MouseKey.Left;
            case PointerEventData.InputButton.Right:  return MouseKey.Right;
            case PointerEventData.InputButton.Middle: return MouseKey.Middle;
            default: return MouseKey.Left;
        }
    }


    public bool IsPointerOver() => isPointerOverObject;




}
