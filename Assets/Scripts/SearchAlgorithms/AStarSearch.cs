using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThePathfinder;

public static class AStarSearch
{

    public static List<Coord> GetPath(Coord A, Coord B, MapGrid map, bool isVisualize)
    {

        map.ResetMap();
        if(isVisualize) SearchVisualizerView.current.Reset();
        Square start  = map.GetGridMatrix()[A.col][A.row];
        Square target = map.GetGridMatrix()[B.col][B.row];

        List<Square> openNodes = new List<Square>();          // Nodes that are scheduled to be processes
        HashSet<Square> closedNodes = new HashSet<Square>();  // Nodes that have been processes - Duplicets aren't allowed' Hence the hash set
        
        openNodes.Add(start);
        float visualizeDelay = 0f;
        float delayInterval = 0.01f;

        if(isVisualize) SearchVisualizerView.current.VisualizeVisit(start, visualizeDelay);

        while(openNodes.Count > 0)
        {
            // Set Current Node to the lowest F Cost
            Square current = openNodes[0];
            for(int i = 1; i < openNodes.Count; i++)
            {
                if(openNodes[i].GetF_Cost() < current.GetF_Cost() || (openNodes[i].GetF_Cost() == current.GetF_Cost() && openNodes[i].H_cost < current.H_cost))
                {
                    current = openNodes[i];
                }
            }

            openNodes.Remove(current);
            closedNodes.Add(current);

            // Check if we found the target
            if(current == target) 
            {
                AppSystem.path_delay = visualizeDelay;
                return ReturnPath(start, target);
            }

            // Else:
            // List<Square> neighbors = (isManhattan) ? GetManhattanNeighborSquares(current) : GetNeighborSquares(current);
            List<Square> neighbors = map.GetAdjacent8(current.GetCoord());

            for(int i = 0; i < neighbors.Count; i++)
            {
                if(!closedNodes.Contains(neighbors[i]))
                {
                    int movement_cost = current.G_cost + CalculateDistance(current, neighbors[i]);
                    if(movement_cost < neighbors[i].G_cost || !openNodes.Contains(neighbors[i]))
                    {
                        neighbors[i].G_cost = movement_cost;
                        neighbors[i].H_cost = CalculateDistance(neighbors[i], target);
                        neighbors[i].parent = current;

                        if(!openNodes.Contains(neighbors[i])) openNodes.Add(neighbors[i]);
                    }
                    if(isVisualize) SearchVisualizerView.current.VisualizeVisit(neighbors[i], visualizeDelay);
                }
            }
            
            visualizeDelay += delayInterval;
        } 
        

        // No Path Was Found
        return new List<Coord>();
    }


    private static List<Coord> ReturnPath(Square start, Square end)
    {
        List<Coord> path = new List<Coord>();

        Square pointer = end;
        while(pointer != start)
        {
            path.Add(pointer.GetCoord());
            pointer = pointer.parent;
        }
        path.Reverse();
        return path;
    }



    private static int CalculateDistance(Square A, Square B)
    {
        Vector2Int difference = new Vector2Int(Mathf.Abs(A.GetColumn() - B.GetColumn()), Mathf.Abs(A.GetRow() - B.GetRow()));

        int DM = (difference.x < difference.y) ? difference.x : difference.y;
        int MM = Mathf.Abs(difference.x - difference.y);

        return DM * 14 + MM * 10;
    }


}
