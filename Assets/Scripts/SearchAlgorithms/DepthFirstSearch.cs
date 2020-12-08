using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThePathfinder;

public static class DepthFirstSearch
{

    private static float delayInterval = 0.01f;

    public static List<Coord> GetPath(Coord A, Coord B, MapGrid map, bool isVisualize)
    {
        List<Coord> path = new List<Coord>();

        map.ResetMap();
        if(isVisualize) SearchVisualizerView.current.Reset();
        Square start  = map.GetGridMatrix()[A.col][A.row];
        Square target = map.GetGridMatrix()[B.col][B.row];
        
        float visualizeDelay = 0f;
        if(isVisualize) SearchVisualizerView.current.VisualizeVisit(start, visualizeDelay);

        DFS(path, start, target, map, isVisualize, visualizeDelay);
        
        return path;
    }


    private static void DFS(List<Coord> path, Square current_square, Square target, MapGrid map, bool isVisualize, float visualizeDelay)
    {
        if(current_square == target)
        {
            Stack<Coord> buffer = new Stack<Coord>();
            Square pointer = target;
            while(pointer != null)
            {
                buffer.Push(pointer.GetCoord());
                pointer = pointer.parent;
            }
            while(buffer.Count > 0) path.Add(buffer.Pop());
            AppSystem.path_delay = visualizeDelay;
        } else 
        {
            current_square.isVisited = true;
            // For each neighbor:
            List<Square> adjacent_squares = map.GetAdjacent8(current_square.GetCoord());
            for(int i = 0; i < adjacent_squares.Count; i++)
            {
                if(!adjacent_squares[i].isVisited) 
                {
                    adjacent_squares[i].parent = current_square;
                    if(isVisualize) SearchVisualizerView.current.VisualizeVisit(adjacent_squares[i], visualizeDelay);
                    DFS(path, adjacent_squares[i], target, map, isVisualize, visualizeDelay + delayInterval);
                    if(path.Count > 0) break;
                }
            }
        }
    }








}
