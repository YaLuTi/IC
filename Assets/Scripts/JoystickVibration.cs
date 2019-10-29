using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickVibration : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Input.GetJoystickNames()[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
