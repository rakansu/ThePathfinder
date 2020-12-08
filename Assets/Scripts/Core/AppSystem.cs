using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThePathfinder;
public class AppSystem : MonoBehaviour
{
    public static float path_delay = 0f;


    void Update()
    {
        JobSystem.Update();
    }


}
