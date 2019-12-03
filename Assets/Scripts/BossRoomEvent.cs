using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class BossRoomEvent : MonoBehaviour
{
    bool IsT = false;
    public AK.Wwise.Event @event;
    public TextMeshProUGUI meshPro;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !IsT)
        {
            IsT = true;
            StartCoroutine(E());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            GetComponent<Collider>().isTrigger = false;
        }
    }

    IEnumerator E()
    {
        @event.Post(gameObject);
        yield return new WaitForSecondsRealtime(0.2f);
        meshPro.DOColor(new Color(1, 1, 1, 1), 1f).SetEase(Ease.OutQuint);
        yield return new WaitForSecondsRealtime(0.05f);
        yield return new WaitForSecondsRealtime(3.5f);
        meshPro.DOColor(new Color(0, 0, 0, 0), 1f);
        yield return 0;
    }
}
