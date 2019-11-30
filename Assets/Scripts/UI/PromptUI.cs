using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptUI : MonoBehaviour
{
    public GameObject Image;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPick.CanPickItem)
        {
            Image.SetActive(true);
        }
        else
        {
            Image.SetActive(false);
        }
    }
}
