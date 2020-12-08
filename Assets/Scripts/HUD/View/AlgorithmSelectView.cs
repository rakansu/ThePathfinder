using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ThePathfinder;

public class AlgorithmSelectView : View
{
    [SerializeField] TextMeshProUGUI current_algorithm;
    [Space(15)]
    [SerializeField] TextMeshProUGUI DFS;
    [SerializeField] TextMeshProUGUI BFS;
    [SerializeField] TextMeshProUGUI Dijkstra;
    [SerializeField] TextMeshProUGUI Astar;

    void Awake()
    {
        DFS.GetComponent<TouchButton>().onTap += () => { SetAlgorithm(Algorithm.DFS, DFS.text);};
        BFS.GetComponent<TouchButton>().onTap += () => {SetAlgorithm(Algorithm.BFS, BFS.text);};
        Dijkstra.GetComponent<TouchButton>().onTap += () => {SetAlgorithm(Algorithm.Dijkstra, Dijkstra.text); };
        Astar.GetComponent<TouchButton>().onTap += () => {SetAlgorithm(Algorithm.AStar, Astar.text); };
    }


    private void SetAlgorithm(Algorithm algorithm, string text)
    {
        AppConfig.searchAlgorithm = algorithm;
        current_algorithm.text = text;
        gameObject.SetActive(false);
    }


}
