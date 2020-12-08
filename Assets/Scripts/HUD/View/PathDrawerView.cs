using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThePathfinder;

public class PathDrawerView : View
{
    private List<GameObject> activeDotTiles = new List<GameObject>();
    private ObjectPool dotTilePool;

    [SerializeField] GameObject dotTilePrefab;

    void Awake()
    {
        dotTilePool = new ObjectPool(dotTilePrefab, transform);
    }


    public void Reset()
    {
        ResetDotTiles();
    }


    public void DrawPath(List<Coord> path)
    {
        ResetDotTiles();
        float delay = 0f;
        float intervalDelay = 0.1f;
        int index = 0;
        for(int i = 0; i < path.Count-1; i++)
        {
            JobAction scheduledAction = (float timeStampe, bool isComplete) =>
            {
                GameObject dotTile = dotTilePool.GetInstance();
                dotTile.GetComponent<RectTransform>().localPosition = Utility.GetPositionInPixel(path[index].col, path[index].row);
                dotTile.SetActive(true);
                activeDotTiles.Add(dotTile);
                index++;   
            };

            JobSystem.ScheduleUntil(scheduledAction, delay);
            delay += intervalDelay;
        }
    }



    private void ResetDotTiles()
    {
        for(int i = 0; i < activeDotTiles.Count; i++)
            activeDotTiles[i].SetActive(false);
        activeDotTiles.Clear();
    }


}
