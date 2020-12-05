using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThePathfinder;

public class MapGrid : MonoBehaviour
{
    private Square[][] grid;

    [SerializeField] int COLUMN_SIZE = 20;
    [SerializeField] int ROW_SIZE    = 15;


    void Awake()
    {
        // Initialize Grid:
        grid = new Square[COLUMN_SIZE][];
        for(int i = 0; i < COLUMN_SIZE; i++) 
            grid[i] = new Square[ROW_SIZE];

        for(int c = 0; c < COLUMN_SIZE; c++)
        {
            for(int r = 0; r < ROW_SIZE; r++)
            {
                int id = c + r * COLUMN_SIZE;
                grid[c][r] = new Square(id,c,r);
            }
        }
    }


    public int GetColumnCount() => COLUMN_SIZE;

    public int GetRowCount() => ROW_SIZE;


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
    public bool IsValid(int col, int row) =>  (0 <= col && col < COLUMN_SIZE) && (0 <= row && row < ROW_SIZE);


    /// <summary>
    /// Returns the adjacent vertical and horizontal squares
    /// </summary>
    public List<Square> GetAdjacent4(Coord current_coord)
    {
        List<Square> adjacent_coords = new List<Square>();
        // Top:
        if(IsValid(current_coord.col, current_coord.row - 1)) adjacent_coords.Add(grid[current_coord.col][current_coord.row - 1]);
        // Bottom:
        if(IsValid(current_coord.col, current_coord.row + 1)) adjacent_coords.Add(grid[current_coord.col][current_coord.row + 1]);
        // Left:
        if(IsValid(current_coord.col - 1, current_coord.row)) adjacent_coords.Add(grid[current_coord.col - 1][current_coord.row]);
        // Right:
        if(IsValid(current_coord.col + 1, current_coord.row)) adjacent_coords.Add(grid[current_coord.col + 1][current_coord.row]);
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
                adjacent_coords.Add(grid[c][r]);
            }
        }
        return adjacent_coords;
    }


    public void ResetMap()
    {
        for(int c = 0; c < COLUMN_SIZE; c++)
        {
            for(int r = 0; r < ROW_SIZE; r++)
            {
                grid[c][r].parent = null;
            }
        }
    }



}
