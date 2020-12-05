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
                grid[c][r] = new Square(c,r);
            }
        }
    }


    public int GetColumnCount() => COLUMN_SIZE;

    public int GetRowCount() => ROW_SIZE;


    /// <summary>
    /// Returns the square matrix
    /// </summary>
    public Square[][] GetGridMatrix() => grid;



    


}
