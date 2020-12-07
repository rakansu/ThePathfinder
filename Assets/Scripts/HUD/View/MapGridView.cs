using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThePathfinder;

[RequireComponent(typeof(MapGrid))]
public class MapGridView : View
{
    private ObjectPool tilePool;
    private MapGrid mapGrid;

    [SerializeField] GameObject tilePrefab;




    void Start()
    {
        mapGrid = GetComponent<MapGrid>();

        tilePool = new ObjectPool(tilePrefab, transform);

        Square[][] matrix = mapGrid.GetGridMatrix();
        int columns = AppConfig.COLUMN_SIZE;
        int rows = AppConfig.ROW_SIZE;

        for(int c = 0; c < columns; c++)
        {
            for(int r = 0; r < rows; r++)
            {
                TileView tile = tilePool.GetInstance().GetComponent<TileView>();
                // Position in pixels:
                Vector2 position = Utility.GetPositionInPixel(c,r);
                tile.Initialize(matrix[c][r], position);
                tile.gameObject.SetActive(true);
            }
        }
    }
















}
