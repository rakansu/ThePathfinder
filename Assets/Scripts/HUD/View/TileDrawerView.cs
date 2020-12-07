using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThePathfinder;

public class TileDrawerView : MonoBehaviour
{
    private ObjectPool wallTilePool;

    [SerializeField] RectTransform pointA;
    [SerializeField] RectTransform pointB;
    [Space(10)]
    [SerializeField] GameObject wallTilePrefab;

    void Awake()
    {
        wallTilePool = new ObjectPool(wallTilePrefab, transform);
    }

    void Start()
    {
        GridBoard.onUpdateTile += OnUpdateTile;
    }



    private void OnUpdateTile(TileView tile)
    {
        if(tile == null) return;
        switch (tile.GetSquare().GetData())
        {
            case SquareData.Empty: 
                tile.GetIcon()?.gameObject.SetActive(false);
                tile.SetIcon(null);
                break;
            case SquareData.PointA:
                pointA.gameObject.SetActive(true);
                pointA.localPosition = tile.GetPixelPosition();
                break;
            case SquareData.PointB:
                pointB.gameObject.SetActive(true);
                pointB.localPosition = tile.GetPixelPosition();
                break;
            case SquareData.Wall:
                break;
        }
    }





}
