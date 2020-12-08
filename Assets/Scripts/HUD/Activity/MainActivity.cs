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
    [SerializeField] ButtonView algorithmButton;

    [Header("Views")]
    [SerializeField] PathDrawerView pathDrawer;
    [SerializeField] AlgorithmSelectView algorithmSelectView;

    [Header("Systems")]
    [SerializeField] MapGrid mapGrid;


    void Awake()
    {
        pointA_button.onTap += () => {GridBoard.current.SetDrawState(GridBoard.DrawState.PointA);};
        pointB_button.onTap += () => {GridBoard.current.SetDrawState(GridBoard.DrawState.PointB);};
        wall_button.onTap   += () => {GridBoard.current.SetDrawState(GridBoard.DrawState.Wall);};
        erase_button.onTap  += () => {GridBoard.current.SetDrawState(GridBoard.DrawState.Erase);};
        algorithmButton.onTap += () => {algorithmSelectView.gameObject.SetActive(!algorithmSelectView.gameObject.activeSelf);};
        visualize_button.onTap += OnVisualize;
    }


    private void OnVisualize()
    {
        if(!GridBoard.current.IsPathSet()) return;
        pathDrawer.Reset();
        List<Coord> path = GetPath(GridBoard.current.GetPointACoord(), GridBoard.current.GetPointBCoord(), mapGrid, true);
        JobAction scheduledAction = (float timeStamp, bool isCompleted) =>
        {
            pathDrawer.DrawPath(path);
        };
        JobSystem.ScheduleUntil(scheduledAction, AppSystem.path_delay);
    }


    private List<Coord> GetPath(Coord A, Coord B, MapGrid mapGrid, bool isVisualize)
    {
        switch(AppConfig.searchAlgorithm)
        {
            case Algorithm.DFS: return DepthFirstSearch.GetPath(A, B, mapGrid, isVisualize);
            case Algorithm.BFS: return BreadthFirstSearch.GetPath(A, B, mapGrid, isVisualize);
            case Algorithm.Dijkstra: return DijkstraSearch.GetPath(A, B, mapGrid, isVisualize);
            case Algorithm.AStar: return AStarSearch.GetPath(A, B, mapGrid, isVisualize);
            default: return AStarSearch.GetPath(A, B, mapGrid, isVisualize);
        }
    }






}
