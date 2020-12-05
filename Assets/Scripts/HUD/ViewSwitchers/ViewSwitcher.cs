using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThePathfinder;

public class ViewSwitcher : MonoBehaviour
{
    private List<View> views = new List<View>();
    private List<Vector2> positions = new List<Vector2>();
    private View start_view;

    private RectTransform rectTransform;

    void Awake()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            View view = transform.GetChild(i).GetComponent<View>();
            views.Add(view);
            positions.Add(view.GetComponent<RectTransform>().anchoredPosition);
            view.onActivate += Prioritize;
            view.onDeactivate += Prioritize;
        }
        if(views.Count == 0) Debug.LogError("There are no views!");
        start_view = views[0];
        HideViews();
    }


    void OnEnable()
    {
        HideViews();
        start_view.gameObject.SetActive(true);
    } 

    void OnDisable()
    {
        HideViews();
    }

    private void Prioritize()
    {
        for(int i = views.Count - 1; i >= 0; i--)
        {
            if(views[i].gameObject.activeSelf)
            {
                views[i].GetComponent<RectTransform>().anchoredPosition = positions[i];
                // for(int j = i-1; j >= 0; j--) views[j].transform.position = Const.OCCLUSION_POS2D;
                return;
            }
        }
    }


    private void HideViews()
    {
        for(int i = 0; i < views.Count; i++)
            views[i].gameObject.SetActive(false);
    }


    public RectTransform GetRectTransform()
    {
        if(rectTransform == null) rectTransform = GetComponent<RectTransform>();
        return rectTransform;
    }




}
