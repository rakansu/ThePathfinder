using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    public delegate void Event();
    public event Event onActivate;
    public event Event onDeactivate;

    private RectTransform rectTransform;


    public RectTransform GetRectTransform()
    {
        if(rectTransform == null) rectTransform = GetComponent<RectTransform>();
        return rectTransform;
    }

  
}
