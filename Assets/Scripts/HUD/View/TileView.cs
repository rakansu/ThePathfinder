using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThePathfinder;

public class TileView : MonoBehaviour
{
    private Square square;
    private RectTransform rectTransform;
    private RectTransform icon;

    private MouseButton button;



    void Awake()
    {
        button = Utility.InitializeComponent<MouseButton>(gameObject);
        // button.onCursorEnter += () => {print("entered: " + square.GetCoord());};
        // button.onCursorExit  += () => {print("left!");};
        // button.onHolding   += (float elapsed) => {print("Holding! elpased:" + elapsed + " seconds");};
        // button.onRelease += (float sec) => {print("Released! after: " + sec + " seconds");};
        // button.onButtonClick   += (MouseKey mouseKey) => {print("clicked!");};
        // button.onButtonCancelClick += (MouseKey mouseKey) => {print("Cancelled!");};
    
    
        button.onButtonTap   += (MouseKey mouseKey) => {GridBoard.current.OnTapTile(this);};
    }

   
    public void Initialize(Square square, Vector2 position)
    {
        this.square = square;
        rectTransform = GetComponent<RectTransform>();
        rectTransform.localPosition = position;
    }

    public void SetIcon(RectTransform newIcon) => icon = newIcon;

    public RectTransform GetIcon() => icon;

    public Square GetSquare() => square;

    public Vector2 GetPixelPosition() => rectTransform.localPosition;


}
