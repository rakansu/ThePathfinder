using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThePathfinder;

public class PathDrawerView : View
{
    private List<GameObject> activeDotTiles = new List<GameObject>();
    private ObjectPool dotTilePool;

    [SerializeField] GridView gridView;
    [SerializeField] GameObject dotTilePrefab;

    void Awake()
    {
        dotTilePool = new ObjectPool(dotTilePrefab, transform);
    }


    public void DrawPath(List<Coord> path)
    {
        ResetDotTiles();
        for(int i = 0; i < path.Count-1; i++)
        {
            GameObject dotTile = dotTilePool.GetInstance();
            dotTile.GetComponent<RectTransform>().localPosition = gridView.GetMappedPosition(path[i].col, path[i].row);
            dotTile.SetActive(true);
            activeDotTiles.Add(dotTile);
        }
    }

    private void ResetDotTiles()
    {
        for(int i = 0; i < activeDotTiles.Count; i++)
            activeDotTiles[i].SetActive(false);
        activeDotTiles.Clear();
    }

}
