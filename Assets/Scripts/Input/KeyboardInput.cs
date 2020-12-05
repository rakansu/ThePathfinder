using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThePathfinder;

public class KeyboardInput : MonoBehaviour
{
    public delegate void Event(KeyCode keyCode);
    public static KeyboardInput current;

    public event Event onKeyEnter;
    public event Event onKeyHold;
    public event Event onKeyExit;

    private List<KeyCode> keyCodes = new List<KeyCode>();



    void Awake()
    {
        current = this;
    }


    void Update()
    {
        for(int i = 0; i < keyCodes.Count; i++)
        { 
            if(Input.GetKeyDown(keyCodes[i])) onKeyEnter?.Invoke(keyCodes[i]);
            if(Input.GetKey(keyCodes[i])) onKeyHold?.Invoke(keyCodes[i]);
            if(Input.GetKeyUp(keyCodes[i])) onKeyExit?.Invoke(keyCodes[i]);
        }
    }


    public void AddKeyCode(KeyCode keyCode)
    {
        keyCodes.Add(keyCode);
    }


    


}
