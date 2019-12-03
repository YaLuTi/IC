using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelaySlider : MonoBehaviour
{
    Slider slider;
    public float FadeSpeed = 10;
    float value = 1;
    float cooldown = 0;
    bool IsCatching = false;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= 1 * Time.deltaTime;
        }
        else
        {
            IsCatching = true;
        }

        if (value != slider.value)
        {
            if (value < slider.value)
            {
                if (IsCatching)
                {
                    slider.value -= (slider.maxValue / FadeSpeed) * Time.deltaTime;
                    if(slider.value < value)
                    {
                        slider.value = value;
                    }
                }
            }
            else
            {
                slider.value = value;
                IsCatching = false;
            }
        }
        else
        {
            IsCatching = false;
        }

    }

    public void SetValue(float v)
    {
        value = v;
        cooldown = 1.25f;
    }
    
}
