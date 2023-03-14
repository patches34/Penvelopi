using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameManagerScriptableObject", order = 1)]
public class GameManagerScriptableObject : ScriptableObject
{
    public Player player;

    public float trailLength;
}
