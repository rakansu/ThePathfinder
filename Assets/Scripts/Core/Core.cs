using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThePathfinder
{
    public delegate void GameEvent();
    public delegate void JobAction(float timeStamp, bool isCompleted);


    [System.Serializable]
    public enum Direction { Down, Left, Up, Right };


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
    }

}


