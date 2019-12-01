using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ButtonHighLight : MonoBehaviour
{
    Image image;
    public float HighLightSpeed = 1;
    public float FadeOutSpeed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void _HighLight()
    {
        StartCoroutine(HighLight());
    }

    IEnumerator HighLight()
    {
        while(image.color.a < 1)
        {
            image.color += new Color(0, 0, 0, HighLightSpeed / 4);
            yield return new WaitForEndOfFrame();
        }
        while (image.color.a > 0)
        {
            image.color += new Color(0, 0, 0, -FadeOutSpeed / 4);
            yield return new WaitForEndOfFrame();
        }
        yield return 0;
    }
}
