using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileView : MonoBehaviour
{
    private RectTransform rectTransform;
   
    public void Initialize(Vector2 position)
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.localPosition = position;
    }







}
