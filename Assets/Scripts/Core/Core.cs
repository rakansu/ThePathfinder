using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThePathfinder
{
    public delegate void GameEvent();
    public delegate void JobAction(float timeStamp, bool isCompleted);

    public static class AppConfig
    {
        public static int COLUMN_SIZE = 29;
        public static int ROW_SIZE    = 13;
        public static float tileWidth  = 25f;
        public static float tileHeight = 25f;
    }

    [System.Serializable]
    public enum SquareData
    {
        Empty,
        PointA,
        PointB,
        Wall
    }

    [System.Serializable]
    public enum MouseKey{Left, Right, Middle};

    [System.Serializable]
    public enum Direction { Down, Left, Up, Right };


    [System.Serializable]
    public struct Coord
    {
        public Coord(int c, int r) {col = c; row = r;}
        [SerializeField] public int col;
        [SerializeField] public int row;
        
        public static bool operator ==(Coord a, Coord b) {return a.Equals(b);}
        public static bool operator !=(Coord a, Coord b) {return !(a == b);}

        public override int GetHashCode() => base.GetHashCode();

        public override bool Equals(object obj)
        {
            if(obj == null) return false;
            if (!(obj is Coord)) return false;
            Coord coord = (Coord) obj;
            return col == coord.col && row == coord.row;
        }

        public override string ToString() => "[" + col + ", " + row + "]";
    }




    public static class Utility
    {

        public static Direction GetInverseDirection(Direction direction)
        {
            if(direction == Direction.Right) return Direction.Left;
            if(direction == Direction.Left) return Direction.Right;
            if(direction == Direction.Up) return Direction.Down;
            return Direction.Up;
        }
        
        public static Transform CreateFolder(Transform parent, string name)
        {
            Transform folder = new GameObject(name).transform;
            folder.SetParent(parent,false);
            folder.localPosition = Vector3.zero;
            return folder;
        }
        
        public static T InitializeComponent<T>(GameObject current) where T : Component
        {
            T component = current.GetComponent<T>();
            if(component == null) component = current.AddComponent<T>();
            return component;
        }
        
        public static Vector2 GetPositionInPixel(Coord coord) => GetPositionInPixel(coord.col,coord.row);

        public static Vector2 GetPositionInPixel(int c, int r)
        {
            int columns = AppConfig.COLUMN_SIZE;
            int rows = AppConfig.ROW_SIZE;
            float x_pos =  ( (-columns + 1) * AppConfig.tileWidth  * 0.5f) + c * AppConfig.tileWidth;
            float y_pos =  ( (rows - 1)     * AppConfig.tileHeight * 0.5f) - r * AppConfig.tileHeight;
            // Position in pixels:
            return new Vector2(x_pos,y_pos);
        }

        /// <summary>
        /// Returns true if the position within the grid bounds
        /// </summary>
        public static bool IsWithinBounds(Vector2 positon)
        {
            return true;
        }

        /// <summary>
        /// Returns a coordinate that corresponds to a position on screen.
        /// If position is out of grid bound, then coordinates are clamped to the closest coordinate
        /// </summary>
        public static Coord GetCoordOfPixelPosition(Vector2 position)
        {
            int c = 0;
            int r = 0;
            return new Coord(c,r);
        }

    }





}


