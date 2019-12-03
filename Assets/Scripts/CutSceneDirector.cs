using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CutSceneDirector : MonoBehaviour
{
    GameObject UI;
    public Camera MainCamera;
    public GameObject Player;
    public GameObject Cine;
    public static bool IsOnMovie = false;
    [Header("OpenScene")]
    public GameObject CameraOne;
    public GameObject CameraTwo;
    public GameObject ActingPlayer;
    public Image BlackPanel;
    [Header("Boss Scene")]
    public GameObject CameraThree;
    public GameObject CameraFour;
    public GameObject CameraFive;
    public AK.Wwise.Event BossSound;
    public GameObject BossScenePlayer;
    public GameObject Boss;
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
        IsOnMovie = true;
        BlackPanel.DOColor(new Color(0, 0, 0, 1), 0.5f);
        yield return new WaitForSeconds(1.75f);
        BossScenePlayer.SetActive(true);
        MainCamera.gameObject.SetActive(false);
        CameraThree.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        CameraThree.transform.DOMove(new Vector3(-164.42f, 5.28f, 34f), 8).SetEase(Ease.InCubic);
        BlackPanel.DOColor(new Color(0, 0, 0, 0), 1.5f);
        yield return new WaitForSeconds(0.25f);
        BossScenePlayer.GetComponent<Animator>().SetTrigger("Pray");
        yield return new WaitForSeconds(5f);
        CameraFour.transform.DOMove(new Vector3(-163.98f, 5.94f, 32.258f), 12f).SetEase(Ease.InSine);
        CameraFour.transform.DORotate(new Vector3(-5.051f, -90f, 0), 12).SetEase(Ease.InSine);
        CameraThree.SetActive(false);
        CameraFour.SetActive(true);
        yield return new WaitForSeconds(9f);
        BossSound.Post(gameObject);
        yield return new WaitForSeconds(1f);
        CameraFour.SetActive(false);
        CameraFive.SetActive(true);
        CameraFive.transform.DOMove(new Vector3(-168.249f, 5.55f, 31.58f), 4f).SetEase(Ease.InOutSine);
        BossScenePlayer.GetComponent<Animator>().SetTrigger("Stand");
        CameraFive.transform.DORotate(new Vector3(0, 86.23f, 0), 4f).SetEase(Ease.InOutSine);
        yield return new WaitForSeconds(3.5f);
        BlackPanel.DOColor(new Color(0, 0, 0, 1), 1f);
        Player.transform.position = new Vector3(-166.97f, 3.751f, 32.2f);
        Player.transform.eulerAngles = new Vector3(0, -270, 0);
        Cine.transform.eulerAngles = new Vector3(13, 90, 0);
        yield return new WaitForSeconds(3.5f);
        Boss.SetActive(true);
        CameraFive.SetActive(false);
        MainCamera.gameObject.SetActive(true);
        BlackPanel.DOColor(new Color(0, 0, 0, 0), 2f);
        BossScenePlayer.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        IsOnMovie = false;
        yield return 0;
    }

    IEnumerator OpenScene()
    {
        IsOnMovie = true;
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
