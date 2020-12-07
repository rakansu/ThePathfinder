using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThePathfinder;

public class TileView : MonoBehaviour
{
    private RectTransform rectTransform;
    
    private MouseButton button;

    void Awake()
    {
        button = Utility.InitializeComponent<MouseButton>(gameObject);
        button.onCursorEnter += () => {print("entered!");};
        button.onCursorExit  += () => {print("left!");};
        button.onButtonTap   += (MouseKey mouseKey) => {print("Tapped!");};
        // button.onHolding   += (float elapsed) => {print("Holding! elpased:" + elapsed + " seconds");};
        // button.onRelease += (float sec) => {print("Released! after: " + sec + " seconds");};
        button.onButtonClick   += (MouseKey mouseKey) => {print("clicked!");};
        button.onButtonCancelClick += (MouseKey mouseKey) => {print("Cancelled!");};
    }

   
    public void Initialize(Vector2 position)
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.localPosition = position;
    }




}
