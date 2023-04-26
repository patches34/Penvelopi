using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameManagerScriptableObject", order = 1)]
public class GameManagerScriptableObject : ScriptableObject
{
    public Player player;

    public float trailLength;

    public AgentManager AgentManager;

    Bounds screenBounds;
    public Bounds ScreenBounds
    {
        get
        {
            if(screenBounds.size.sqrMagnitude <= 0)
            {
                CalcScreenBounds();
            }

            return screenBounds;
        }
    }

    public void CalcScreenBounds()
    {
        screenBounds = new Bounds(Camera.main.transform.position,
            new Vector3(Camera.main.orthographicSize * Camera.main.aspect * 2f, Camera.main.orthographicSize * 2f));
    }
}
