using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptUI : MonoBehaviour
{
    public GameObject Image;
    public GameObject Hint;
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
            Hint.SetActive(true);
        }
        else
        {
            Image.SetActive(false);
            Hint.SetActive(false);
        }
    }
}
