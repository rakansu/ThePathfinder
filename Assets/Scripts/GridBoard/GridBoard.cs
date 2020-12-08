using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThePathfinder;

public class GridBoard : MonoBehaviour
{
    public static GridBoard current;

    public delegate void TileEvent(TileView tile);
    public static event TileEvent onUpdateTile;
    

    public enum DrawState
    {
        None,
        Erase,
        PointA,
        PointB,
        Wall
    }


    private DrawState current_drawstate = DrawState.None;


    private TileView pointA_tile = null;
    private TileView pointB_tile = null;


    void Awake()
    {
        current = this;
    }

    public void SetDrawState(DrawState drawState) => current_drawstate = drawState;

    public void OnTapTile(TileView tile)
    {
        switch(current_drawstate)
        {
            case DrawState.Erase:
                ProcessErasingTile(tile);
                break;
            case DrawState.PointA: 
                ProcessDrawingPoint(pointA_tile, tile, SquareData.PointA);
                pointA_tile = tile;
                break;
            case DrawState.PointB: 
                ProcessDrawingPoint(pointB_tile, tile, SquareData.PointB);
                pointB_tile = tile;
                break;
            case DrawState.Wall: 
                ProcessDrawingWall(tile);
                break;
        }
    }

    private void ProcessErasingTile(TileView tile)
    {
        tile.GetSquare().SetData(SquareData.Empty);
        onUpdateTile?.Invoke(tile);
    }

    private void ProcessDrawingPoint(TileView tilePoint, TileView newTile, SquareData data)
    {
        if(tilePoint != null) tilePoint.GetSquare().SetData(SquareData.Empty);
        newTile.GetSquare().SetData(data);
        onUpdateTile?.Invoke(tilePoint);
        onUpdateTile?.Invoke(newTile);
        current_drawstate = DrawState.None;
    }

    private void ProcessDrawingWall(TileView tile)
    {
        tile.GetSquare().SetData(SquareData.Empty);
        onUpdateTile?.Invoke(tile);
        tile.GetSquare().SetData(SquareData.Wall);
        onUpdateTile?.Invoke(tile);
    }


    public Coord GetPointACoord() => pointA_tile.GetSquare().GetCoord();
    public Coord GetPointBCoord() => pointB_tile.GetSquare().GetCoord();
    

    public bool IsPathSet() => pointA_tile != null && pointB_tile != null;







}
