using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThePathfinder;

public class SearchVisualizerView : MonoBehaviour
{
    private List<GameObject> visualizeTiles = new List<GameObject>();
    private ObjectPool visualizeTilesPool;
    private float max_delay = 5f;

    public static SearchVisualizerView current;

    [SerializeField] GameObject visualizeTilePrefab;

    void Awake()
    {
        current = this;

        visualizeTilesPool = new ObjectPool(visualizeTilePrefab, transform);
    }

    public void Reset()
    {
        for(int i = 0; i < visualizeTiles.Count; i++) visualizeTiles[i].gameObject.SetActive(false);
        visualizeTiles.Clear();
    }



    public void VisualizeVisit(Square square, float delay)
    {
        VisualizeTile tile = visualizeTilesPool.GetInstance().GetComponent<VisualizeTile>();
        Vector2 position = Utility.GetPositionInPixel(square.GetCoord());
        float ratio = Mathf.Clamp01(delay / max_delay);
        Color color = new Color(0,1 - ratio,1,1);
        tile.Visualize(position, color, delay);
        visualizeTiles.Add(tile.gameObject);
        tile.gameObject.SetActive(true);
    }



}
