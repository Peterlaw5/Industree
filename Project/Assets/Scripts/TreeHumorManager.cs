using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TreeHumorManager : MonoBehaviour, IPointerDownHandler
{
    #region Static

    public static TreeHumorManager Instance { get; private set; }

    #endregion

    #region Fields

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject heartPrefab;

    [SerializeField]
    private GameObject sleepPrefab;

    [SerializeField]
    private Transform heartSpawnPoint;

    [SerializeField]
    private Transform sleepSpawnPoint;

    [SerializeField]
    private float sleepTime = 3f;

    [SerializeField]
    private float fadeTime = 6f;

    private bool isSleeping = false;

    #endregion

    #region Methods

    private void Start()
    {
        MenuSound.DestroyMenuSound();
    }

    private void Awake()
    {
        Instance = this;
    }

    public void TriggerHappy()
    {
        animator.SetTrigger("happy");
    }

    public void TriggerSleep()
    {
        animator.SetTrigger("sleep");
    }

    public void ToggleSad(bool active)
    {
        animator.SetBool("sad", active);
    }

    public void GoToSleep()
    {
        if (isSleeping)
            return;

        isSleeping = true;
        StartCoroutine(CoGoToSleep());
    }

    private IEnumerator CoGoToSleep()
    {

        FindObjectOfType<DialogsController>().StopDialogs();
        TriggerSleep();

        if (sleepPrefab != null)
            Instantiate(sleepPrefab, sleepSpawnPoint.position, Quaternion.identity);

        yield return new WaitForSeconds(sleepTime);

        FindObjectOfType<FadeController>().TriggerFade();

        yield return new WaitForSeconds(fadeTime);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex != SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        var toolsController = FindObjectOfType<ToolsController>();
        if (toolsController.CurrentTool.TypeTool == ToolType.Recycler)
        {
            Instantiate(heartPrefab, heartSpawnPoint.position, Quaternion.identity);
        }
    }

    #endregion

}
