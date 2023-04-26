using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudMenu : MonoBehaviour
{
    [SerializeField]
    Text trailLengthLabel;
    const string k_TRAIL_LENGTH_STR = "Length = {0:0.0}";

    [SerializeField]
    Text loggingLabel;

    [SerializeField]
    GameManagerScriptableObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        trailLengthLabel.text = string.Format(k_TRAIL_LENGTH_STR, gameManager.trailLength);

        loggingLabel.text = string.Format("Resolutions = {0}\nData = {1}", Screen.currentResolution.ToString(), Camera.main.aspect.ToString());
    }
}
