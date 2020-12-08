using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ThePathfinder;

public class VisualizeTile : MonoBehaviour
{
    RectTransform rectTransform;
    private Image icon;

    private bool isVisualize = false;
    private float visualizeSpeed = 5f;

    void Awake()
    {
        icon = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
    }

    void OnEnable()
    {
        rectTransform.localScale = Vector3.zero;
        isVisualize = false;
    }

    void Update()
    {
        if(!isVisualize) return;
        rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, Vector3.one, Time.deltaTime * visualizeSpeed);
        if(rectTransform.localScale.x > 0.95f)
        {
            rectTransform.localScale = Vector3.one;
            isVisualize = false;
        }
    }


    public void Visualize(Vector2 position, Color color, float delay)
    {
        icon.color = color;
        rectTransform.localPosition = position;
        JobAction jobAction = (float timeStamp, bool isComplete) =>
        {
            isVisualize = true;   
        };
        JobSystem.ScheduleUntil(jobAction, delay);
    }






}
