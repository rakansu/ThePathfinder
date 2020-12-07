using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainActivity : Activity
{
 
    [SerializeField] ButtonView pointA_button;
    [SerializeField] ButtonView pointB_button;
    [SerializeField] ButtonView wall_button;
    [SerializeField] ButtonView visualize_button;


    void Awake()
    {
        pointA_button.onTap += () => {GridBoard.current.SetDrawState(GridBoard.DrawState.PointA);};
        pointB_button.onTap += () => {GridBoard.current.SetDrawState(GridBoard.DrawState.PointB);};
        
    }





}
