using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathUI : MonoBehaviour
{
    public Image BlackPanel;
    public Image BlackPanelTwo;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void _DeathEvent()
    {
        StartCoroutine(DeathEvent());
    }

    public IEnumerator DeathEvent()
    {
        yield return new WaitForSecondsRealtime(3.2f);
        while(BlackPanel.color.a < 0.9f)
        {
            BlackPanel.color += new Color(0, 0, 0, 0.02f);
            yield return new WaitForFixedUpdate();
        }
        while(text.color.a < 1 && BlackPanel.color.a > 0.9f)
        {
            text.color += new Color(0, 0, 0, 0.03f);
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSecondsRealtime(2f);
        while (BlackPanelTwo.color.a < 1f)
        {
            BlackPanelTwo.color += new Color(0, 0, 0, 0.005f);
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene(0);
        yield return 0;
    }
}
