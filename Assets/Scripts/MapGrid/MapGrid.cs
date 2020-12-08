using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThePathfinder;

public class MapGrid : MonoBehaviour
{
    private Square[][] grid;



    void Awake()
    {
        // Initialize Grid:
        grid = new Square[AppConfig.COLUMN_SIZE][];
        for(int i = 0; i < AppConfig.COLUMN_SIZE; i++) 
            grid[i] = new Square[AppConfig.ROW_SIZE];

        for(int c = 0; c < AppConfig.COLUMN_SIZE; c++)
        {
            for(int r = 0; r < AppConfig.ROW_SIZE; r++)
            {
                int id = c + r * AppConfig.COLUMN_SIZE;
                grid[c][r] = new Square(id,c,r);
            }
        }
    }



    /// <summary>
    /// Returns the square matrix
    /// </summary>
    public Square[][] GetGridMatrix() => grid;


    /// <summary>
    /// Retruns true if the coordinate within the bounds of the matrix
    /// </summary>
    public bool IsValid(Coord coord) => IsValid(coord.col,coord.row);
    
    /// <summary>
    /// Retruns true if the coordinate within the bounds of the matrix
    /// </summary>
    public bool IsValid(int col, int row) =>  (0 <= col && col < AppConfig.COLUMN_SIZE) && (0 <= row && row < AppConfig.ROW_SIZE);


    /// <summary>
    /// Returns the adjacent vertical and horizontal squares
    /// </summary>
    public List<Square> GetAdjacent4(Coord current_coord)
    {
        List<Square> adjacent_coords = new List<Square>();

        int c = current_coord.col;
        int r = current_coord.row;

        // Top:
        if(IsValid(c, r - 1) && grid[c][r].IsWalkable()) adjacent_coords.Add(grid[c][r - 1]);
        // Bottom:
        if(IsValid(c, r + 1) && grid[c][r].IsWalkable()) adjacent_coords.Add(grid[c][r + 1]);
        // Left:
        if(IsValid(c - 1, r) && grid[c][r].IsWalkable()) adjacent_coords.Add(grid[c - 1][r]);
        // Right:
        if(IsValid(c + 1, r) && grid[c][r].IsWalkable()) adjacent_coords.Add(grid[c + 1][r]);
        return adjacent_coords;
    }


    /// <summary>
    /// Returns the adjacent 8 squares
    /// </summary>
    public List<Square> GetAdjacent8(Coord current_coord)
    {
        List<Square> adjacent_coords = new List<Square>();
        for(int c = current_coord.col - 1; c <= current_coord.col + 1; c++)
        {
            for(int r = current_coord.row - 1; r <= current_coord.row + 1; r++)
            {
                if(c == current_coord.col && r == current_coord.row) continue;
                if(!IsValid(c,r)) continue;
                if(grid[c][r].IsWalkable()) adjacent_coords.Add(grid[c][r]);
            }
        }
        return adjacent_coords;
    }


    public void ResetMap()
    {
        for(int c = 0; c < AppConfig.COLUMN_SIZE; c++)
        {
            for(int r = 0; r < AppConfig.ROW_SIZE; r++)
            {
                grid[c][r].parent = null;
            }
        }
    }



}
