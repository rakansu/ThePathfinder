using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThePathfinder;

public class Square
{

    private int ID;
    private int col;
    private int row;
    private SquareData data;

    public Square parent;



    public Square(int ID, int col, int row)
    {
        this.ID  = ID;
        this.col = col;
        this.row = row;
    }

    public void SetData(SquareData data) => this.data = data;

    public SquareData GetData() => data;

    public int GetID => ID;

    public int GetColumn() => col;

    public int GetRow() => row;

    public Coord GetCoord() => new Coord(col,row);

}
