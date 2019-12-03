using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class CutSceneDirector : MonoBehaviour
{
    GameObject UI;
    public Camera MainCamera;
    public GameObject BlackBar;
    public GameObject Player;
    public GameObject Cine;
    public GameObject Good_UI;
    public static bool IsOnMovie = false;
    [Header("OpenScene")]
    public GameObject CameraOne;
    public GameObject CameraTwo;
    public GameObject ActingPlayer;
    public AK.Wwise.Event FoundSound;
    public Image BlackPanel;
    public TextMeshProUGUI OpenText;
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
        StartCoroutine(OpenScene());
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
        BlackBar.SetActive(true);
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
        BlackBar.SetActive(false);
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
        ActingPlayer.SetActive(true);
        Good_UI.SetActive(false);
        BlackPanel.DOColor(new Color(0, 0, 0, 1), 0);
        BlackBar.SetActive(true);
        BlackPanel.DOColor(new Color(0, 0, 0, 0), 0.5f);
        CameraOne.SetActive(true);
        MainCamera.gameObject.SetActive(false);
        CameraOne.transform.DOMove(new Vector3(32.11f, 0.855f, 15.917f), 10f).SetEase(Ease.InOutSine);
        yield return new WaitForSecondsRealtime(9.5f);
        CameraOne.SetActive(false);
        CameraTwo.SetActive(true);
        CameraTwo.transform.DOMove(new Vector3(38.677f, 0.65f, 16.012f), 14.954f);
        ActingPlayer.GetComponent<Animator>().SetTrigger("StandUp");
        yield return new WaitForSecondsRealtime(6f);
        BlackPanel.DOColor(new Color(0, 0, 0, 1), 2f);
        yield return new WaitForSecondsRealtime(2f);
        BlackBar.SetActive(false);
        yield return new WaitForSecondsRealtime(1f);
        CameraOne.SetActive(false);
        MainCamera.gameObject.SetActive(true);
        ActingPlayer.SetActive(false);
        BlackPanel.DOColor(new Color(0, 0, 0, 0), 2f);
        IsOnMovie = false;
        Good_UI.SetActive(true);
        yield return new WaitForSecondsRealtime(0.7f);
        FoundSound.Post(gameObject);
        yield return new WaitForSecondsRealtime(0.5f);
        OpenText.DOColor(new Color(1, 1, 1, 1), 1f).SetEase(Ease.OutQuint);
        yield return new WaitForSecondsRealtime(0.05f);
        yield return new WaitForSecondsRealtime(3.5f);
        OpenText.DOColor(new Color(0, 0, 0, 0), 1f);
        yield return 0;
    }
}
