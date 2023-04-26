using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CalibrateMenu : MonoBehaviour
{
    [SerializeField]
    Text mouseDeltarangeLabel;
    const string k_MOUSE_DELTA_STR = "Mouse Delta Average = {0:0.0}";

    [SerializeField]
    GameManagerScriptableObject gameManager;

    Vector2 deltaAvr = Vector2.zero;
    int deltaCount = 0;

    bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive)
        {
            deltaAvr += Mouse.current.delta.ReadValue();

            ++deltaCount;
        }

        mouseDeltarangeLabel.text = string.Format(k_MOUSE_DELTA_STR, deltaAvr);
    }

    public void StartCalibrate()
    {
        
    }

    public void EndCalibrate()
    {
        
    }

    public void Toggle()
    {
        if(isActive)
        {
            isActive = false;

            deltaAvr.x /= deltaCount;
            deltaAvr.y /= deltaCount;
        }
        else
        {
            isActive = true;

            deltaAvr = Vector2.zero;

            deltaCount = 0;
        }
    }
}
