using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThePathfinder;

public class MainActivity : Activity
{
    [Header("Buttons")]
    [SerializeField] ButtonView pointA_button;
    [SerializeField] ButtonView pointB_button;
    [SerializeField] ButtonView wall_button;
    [SerializeField] ButtonView erase_button;
    [SerializeField] ButtonView visualize_button;

    [Header("Views")]
    [SerializeField] PathDrawerView pathDrawer;

    [Header("Systems")]
    [SerializeField] MapGrid mapGrid;


    void Awake()
    {
        pointA_button.onTap += () => {GridBoard.current.SetDrawState(GridBoard.DrawState.PointA);};
        pointB_button.onTap += () => {GridBoard.current.SetDrawState(GridBoard.DrawState.PointB);};
        wall_button.onTap   += () => {GridBoard.current.SetDrawState(GridBoard.DrawState.Wall);};
        erase_button.onTap  += () => {GridBoard.current.SetDrawState(GridBoard.DrawState.Erase);};
        visualize_button.onTap += OnVisualize;
    }


    private void OnVisualize()
    {
        if(!GridBoard.current.IsPathSet()) return;
        pathDrawer.Reset();
        List<Coord> path = DepthFirstSearch.GetPath(GridBoard.current.GetPointACoord(), GridBoard.current.GetPointBCoord(), mapGrid, true);
        JobAction scheduledAction = (float timeStamp, bool isCompleted) =>
        {
            pathDrawer.DrawPath(path);
        };
        JobSystem.ScheduleUntil(scheduledAction, AppSystem.path_delay);
    }






}
