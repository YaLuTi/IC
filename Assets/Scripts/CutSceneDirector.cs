using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CutSceneDirector : MonoBehaviour
{
    GameObject UI;
    public Camera MainCamera;
    [Header("OpenScene")]
    public GameObject CameraOne;
    public GameObject CameraTwo;
    public GameObject ActingPlayer;
    public Image BlackPanel;
    [Header("Boss Scene")]
    public GameObject CameraThree;
    public GameObject CameraFour;
    public GameObject BossScenePlayer;
    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine(OpenScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void _BossScene()
    {
        StartCoroutine(BossScene());
    }

    IEnumerator BossScene()
    {
        BlackPanel.DOColor(new Color(0, 0, 0, 1), 0.5f);
        yield return new WaitForSeconds(1.75f);
        MainCamera.enabled = false;
        CameraThree.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        CameraThree.transform.DOMove(new Vector3(-164.42f, 5.28f, 34f), 8).SetEase(Ease.InCubic);
        BlackPanel.DOColor(new Color(0, 0, 0, 0), 1.5f);
        yield return new WaitForSeconds(0.25f);
        BossScenePlayer.GetComponent<Animator>().SetTrigger("Pray");
        yield return new WaitForSeconds(5f);
        CameraFour.transform.DOMove(new Vector3(-163.98f, 5.94f, 32.258f), 12f).SetEase(Ease.InCubic);
        CameraFour.transform.DORotate(new Vector3(-5.051f, -90f, 0), 12).SetEase(Ease.InCubic);
        CameraThree.SetActive(false);
        CameraFour.SetActive(true);
        yield return new WaitForSeconds(10f);
        yield return 0;
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
