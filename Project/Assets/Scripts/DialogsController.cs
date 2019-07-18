using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogsController : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private GameObject[] dialogsPrefabs;

    [SerializeField]
    private float dialogsSpawnTime = 10f;

    [SerializeField]
    private float dialogsDurationTime = 5f;

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private Transform criticalMessageSpawnPoint;

    [SerializeField]
    private bool flipDialogs = false;

    [SerializeField]
    private AudioSource talk;

    [SerializeField]
    private AudioSource alert;

    private Coroutine spawnRandomDialogsCoroutine;
   // private CriticalStateController criticalStateController;

    #endregion

    #region Methods

    private void Awake()
    {
       // criticalStateController = FindObjectOfType<CriticalStateController>();
    }

    private void Start()
    {
        //if (criticalStateController != null)
        //{
        //    criticalStateController.OnCriticalStateEnter += StopDialogs;
        //    criticalStateController.OnCriticalStateExit += PlayDialogs;
        //}

        PlayDialogs();
    }


    private void PlayDialogs()
    {
        StopDialogs();

        spawnRandomDialogsCoroutine = StartCoroutine(SpawnRandomDialogs());

    }
    public void StopDialogs()
    {
        if (spawnRandomDialogsCoroutine != null)
        {
            StopCoroutine(spawnRandomDialogsCoroutine);
            spawnRandomDialogsCoroutine = null;
        }
    }

    private IEnumerator SpawnRandomDialogs()
    {
        while (true)
        {
            yield return new WaitForSeconds(dialogsSpawnTime);
            SpawnDialog(Random.Range(0, dialogsPrefabs.Length));
        }

    }

    private void SpawnDialog(int index)
    {
        talk.Play();
        GameObject lastSpawnDialog = Instantiate(dialogsPrefabs[index], spawnPoint.position, Quaternion.identity);

        if(flipDialogs)
            lastSpawnDialog.GetComponent<SpriteRenderer>().flipX = true;

        Destroy(lastSpawnDialog, dialogsDurationTime);
    }

    private void OnDestroy()
    {
        //var criticalStateController = FindObjectOfType<CriticalStateController>();

        //if (criticalStateController != null)
        //{
        //    criticalStateController.OnCriticalStateEnter -= StopDialogs;
        //    criticalStateController.OnCriticalStateExit -= PlayDialogs;
        //}

        StopAllCoroutines();
    }

    public GameObject SpawnCriticalMessage(GameObject criticalMessagePrefab)
    {
        alert.Play();
        return Instantiate(criticalMessagePrefab, criticalMessageSpawnPoint.position, Quaternion.identity);
    }
    #endregion
}
