using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CutSceneDirector : MonoBehaviour
{
    GameObject UI;
    [Header("OpenScene")]
    public GameObject CameraOne;
    public GameObject CameraTwo;
    public GameObject ActingPlayer;
    public Image BlackPanel;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(OpenScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator OpenScene()
    {
        CameraOne.transform.DOMove(new Vector3(32.11f, 0.855f, 15.917f), 10f).SetEase(Ease.InOutSine);
        yield return new WaitForSecondsRealtime(9.5f);
        CameraOne.SetActive(false);
        CameraTwo.SetActive(true);
        CameraTwo.transform.DOMove(new Vector3(38.882f, 2.793f, 16.012f), 7f);
        ActingPlayer.GetComponent<Animator>().SetTrigger("StandUp");
        yield return new WaitForSecondsRealtime(6f);
        BlackPanel.DOColor(new Color(0, 0, 0, 1), 2f);
        yield return 0;
    }
}
