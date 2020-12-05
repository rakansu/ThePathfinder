using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonView : View
{
    public delegate void ButtonEvent();
    public delegate void ButtonEventFloat(float elapsed);
    public event ButtonEvent onTap;
    public event ButtonEvent onClick;
    public event ButtonEvent onEnter;
    public event ButtonEventFloat onHolding;
    public event ButtonEventFloat onRelease;


    private TouchButton button;

    void Awake()
    {
        button = gameObject.AddComponent<TouchButton>();
        
        // Event Subscribtion:
        button.onEnter += OnEnter;
        button.onTap += OnTap;
        button.onHolding += OnHolding;
        button.onRelease += OnRelease;
        button.onClick += OnClick;
    }



    private void OnTap()
    {
        onTap?.Invoke();
    }

    private void OnClick()
    {
        onClick?.Invoke();
    }

    private void OnEnter()
    {
        onEnter?.Invoke();
    }

    private void OnHolding(float elapsed)
    {
        onHolding?.Invoke(elapsed);
    }

    private void OnRelease(float elapsed)
    {
        onRelease?.Invoke(elapsed);
    }




}
