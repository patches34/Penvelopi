using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    [SerializeField]
    Agent agentPrefab;

    [SerializeField]
    int agentSpawnCount;

    public List<Agent> Agents = new List<Agent>();

    [SerializeField]
    GameManagerScriptableObject gameData;

    // Start is called before the first frame update
    void Start()
    {
        gameData.AgentManager = this;

        #region Agent Spawning
        Vector3 spawnPos = Vector3.zero;

        for (int i = 0; i < agentSpawnCount; i++)
        {
            spawnPos.x = Random.Range(gameData.ScreenBounds.min.x, gameData.ScreenBounds.max.x);
            spawnPos.y = Random.Range(gameData.ScreenBounds.min.y, gameData.ScreenBounds.max.y);

            Agents.Add(Instantiate<Agent>(agentPrefab,
                spawnPos,
                Quaternion.identity));
        }
        #endregion
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Agent GetAgentAtIndex(int index)
    {
        if(index >= Agents.Count || index < 0)
        {
            return null;
        }
        
        return Agents[index];
    }

    public int GetAgentCount()
    {
        return Agents.Count;
    }
}
