using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThePathfinder;

public static class BreadthFirstSearch 
{

    // 1- Search adjacent squares for the target
    // 2- if found the target, stop
    // 3- if target was not found, add the square and search its adjacent targets
    // 4- if target was not found after searching the entire map, then target is not reachable.

    public static List<Coord> GetPath(Coord A, Coord B, MapGrid map)
    {
        List<Coord> path = new List<Coord>();

        map.ResetMap();
        Square start  = map.GetGridMatrix()[A.col][A.row];
        Square target = map.GetGridMatrix()[B.col][B.row];
        HashSet<Square> visited = new HashSet<Square>();
        Queue<Square> queue = new Queue<Square>();

        // if start is the target:
        if(start == target)
        {
            path.Add(start.GetCoord());
            return path;
        }


        queue.Enqueue(start);
        visited.Add(start);
        Square next_square;

        while(queue.Count > 0)
        {
            next_square = queue.Dequeue();
            visited.Add(next_square);


            List<Square> adjacent_squares = map.GetAdjacent8(next_square.GetCoord());
            for(int i = 0; i < adjacent_squares.Count; i++)
            {
                if(adjacent_squares[i].parent == null) adjacent_squares[i].parent = next_square;   
                if(!visited.Contains(adjacent_squares[i]))
                    queue.Enqueue(adjacent_squares[i]);
            }

            // Target Found:
            if(next_square == target)
            {
                Stack<Coord> buffer = new Stack<Coord>();
                Square pointer = target;
                while(pointer != start)
                {
                    buffer.Push(pointer.GetCoord());
                    pointer = pointer.parent;
                }
                while(buffer.Count > 0) path.Add(buffer.Pop());
                return path;
            }
        }


        return path;
    }






}
