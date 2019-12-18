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
    public bool SkipOpen;
    [Header("OpenScene")]
    public GameObject CameraOne;
    public GameObject CameraTwo;
    public GameObject ActingPlayer;
    public GameObject PlayerLight;
    public GameObject TextOne;
    public GameObject TextTwo;
    public GameObject TextThree;
    public GameObject TextFour;
    public AK.Wwise.Event FoundSound;
    public Image BlackPanel;
    public TextMeshProUGUI OpenText;
    [Header("Boss Scene")]
    public GameObject CameraThree;
    public GameObject CameraFour;
    public GameObject CameraFive;
    public AK.Wwise.Event BossSound;
    public AK.Wwise.Event BossMusic;
    public GameObject BossScenePlayer;
    public GameObject CutSceneBoss;
    public GameObject Boss;
    public GameObject BossSlider;
    [Header("End Scene")]
    public RectTransform EndingText;
    public Light light;
    public AK.Wwise.Event EndingSong;

    public GameObject TextFive;
    public GameObject TextSix;
    public GameObject TextSeven;
    public GameObject TextEight;
    public GameObject TextNine;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerBackpackData.FirstDeath && !SkipOpen)
        {
            StartCoroutine(OpenScene());
        }
        else
        {
            StartCoroutine(NormalDeath());
        }
        // StartCoroutine(End());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void _End()
    {
        StartCoroutine(End());
    }

    public void _BossScene()
    {
        StartCoroutine(BossScene());
    }

    IEnumerator End()
    {
        IsOnMovie = true;
        BlackPanel.DOColor(new Color(1, 1, 1, 1), 10f);
        Good_UI.SetActive(false);
        while (light.intensity < 18)
        {
            light.intensity += 0.2f;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.5f);
        BlackPanel.DOColor(new Color(0, 0, 0, 1), 0.5f);
        EndingSong.Post(gameObject);
        yield return new WaitForSeconds(2.5f);
        TextFive.SetActive(true);
        yield return new WaitForSeconds(3.1f);
        TextFive.SetActive(false);
        TextSix.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        TextSix.SetActive(false);
        TextSeven.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        TextSeven.SetActive(false);
        TextEight.SetActive(true);
        yield return new WaitForSeconds(2f);
        TextEight.SetActive(false);
        TextNine.SetActive(true);
        yield return new WaitForSeconds(3f);
        TextNine.SetActive(false);
        yield return new WaitForSeconds(2f);
        EndingText.DOAnchorPosY(1325, 75);
        yield return 0;
    }

    IEnumerator NormalDeath()
    {
        ActingPlayer.SetActive(false);
        BlackPanel.DOColor(new Color(0, 0, 0, 1), 0f);
        BlackPanel.DOColor(new Color(0, 0, 0, 0), 0.5f);
        yield return 0;
    }

    IEnumerator BossScene()
    {
        IsOnMovie = true;
        BlackPanel.DOColor(new Color(0, 0, 0, 1), 0.5f);
        yield return new WaitForSeconds(1.75f);
        BlackBar.SetActive(true);
        Good_UI.SetActive(false);
        MainCamera.gameObject.SetActive(false);
        BossScenePlayer.SetActive(true);
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
        CutSceneBoss.SetActive(true);
        CameraFive.transform.DOMove(new Vector3(-168.249f, 5.55f, 31.58f), 4f).SetEase(Ease.InOutSine);
        BossScenePlayer.GetComponent<Animator>().SetTrigger("Stand");
        CameraFive.transform.DORotate(new Vector3(0, 86.23f, 0), 4f).SetEase(Ease.InOutSine);
        yield return new WaitForSeconds(3.5f);
        BlackPanel.DOColor(new Color(0, 0, 0, 1), 1f);
        yield return new WaitForSeconds(0.5f);
        Player.transform.position = new Vector3(-166.97f, 3.751f, 32.2f);
        Player.transform.eulerAngles = new Vector3(0, -270, 0);
        Cine.transform.eulerAngles = new Vector3(13, 90, 0);
        yield return new WaitForSeconds(3.5f);
        CutSceneBoss.SetActive(false);
        BossSlider.SetActive(true);
        Boss.SetActive(true);
        Good_UI.SetActive(true);
        BlackBar.SetActive(false);
        CameraFive.SetActive(false);
        MainCamera.gameObject.SetActive(true);
        BossMusic.Post(gameObject);
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
        PlayerLight.SetActive(false);
         yield return new WaitForSecondsRealtime(0.1f);
        Good_UI.SetActive(false);
        BlackPanel.DOColor(new Color(0, 0, 0, 1), 0);
        TextOne.SetActive(true);
        yield return new WaitForSecondsRealtime(3f);
        TextOne.SetActive(false);
        TextTwo.SetActive(true);
        yield return new WaitForSecondsRealtime(5.5f);
        TextTwo.SetActive(false);
        TextThree.SetActive(true);
        yield return new WaitForSecondsRealtime(4f);
        TextThree.SetActive(false);
        TextFour.SetActive(true);
        yield return new WaitForSecondsRealtime(4.5f);
        TextFour.SetActive(false);
        yield return new WaitForSecondsRealtime(1f);
        BlackBar.SetActive(true);
        BlackPanel.DOColor(new Color(0, 0, 0, 0), 1f);
        MainCamera.gameObject.SetActive(false);
        CameraOne.SetActive(true);
        CameraOne.transform.DOMove(new Vector3(32.11f, 0.855f, 15.917f), 10f).SetEase(Ease.InOutSine);
        yield return new WaitForSecondsRealtime(1f);
        yield return new WaitForSecondsRealtime(3f);
        yield return new WaitForSecondsRealtime(5.5f);
        CameraOne.SetActive(false);
        CameraTwo.SetActive(true);
        CameraTwo.transform.DOMove(new Vector3(38.677f, 0.65f, 16.012f), 14.954f);
        ActingPlayer.GetComponent<Animator>().SetTrigger("StandUp");
        yield return new WaitForSecondsRealtime(4f);
        yield return new WaitForSecondsRealtime(2f);
        BlackPanel.DOColor(new Color(0, 0, 0, 1), 2f);
        yield return new WaitForSecondsRealtime(2f);
        yield return new WaitForSecondsRealtime(1f);
        CameraTwo.SetActive(false);
        MainCamera.gameObject.SetActive(true);
        ActingPlayer.SetActive(false);
        PlayerLight.SetActive(true);
        BlackPanel.DOColor(new Color(0, 0, 0, 0), 2f);
         IsOnMovie = false;
        BlackBar.SetActive(false);
        Good_UI.SetActive(true);
        yield return new WaitForSecondsRealtime(0.7f);
        FoundSound.Post(gameObject);
        yield return new WaitForSecondsRealtime(0.2f);
        OpenText.DOColor(new Color(1, 1, 1, 1), 1f).SetEase(Ease.OutQuint);
        yield return new WaitForSecondsRealtime(0.05f);
        yield return new WaitForSecondsRealtime(3.5f);
        OpenText.DOColor(new Color(0, 0, 0, 0), 1f);
        yield return 0;
    }
}
