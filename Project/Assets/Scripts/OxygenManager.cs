using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OxygenManager : MonoBehaviour
{
    #region Static

    public static OxygenManager Instance;

    #endregion

    #region Fields

    public Image progressBar;
    public AudioSource bobble;

    private float oxygen = 0;
    private Coroutine autoOxygenCoroutine;
    private Coroutine autoRemoveOxygenCoroutine;

    [SerializeField]
    private int autoOxygenAmount = 2;

    [SerializeField]
    private float autoOxygenCooldown = 8f;

    [SerializeField]
    private bool useAutoOxygen = true;

    [SerializeField]
    private float autoRemoveOxygenAmount = 0.01f;

    [SerializeField]
    private float autoRemoveOxygenCooldown = 3f;

    [SerializeField]
    private bool useRemoveOxygen = true;

    [SerializeField]
    private float startOxygen = 0f;
    [SerializeField]
    private float endPhaseOxygen = 1f;
    [SerializeField]
    private GameObject oxygenPrefab;
    [SerializeField]
    private Vector4 spawnArea;

    private WaterController waterController;

    #endregion

    #region Methods

    private void Awake()
    {
        Instance = this;
        waterController = FindObjectOfType<WaterController>();
    }

    void Start()
    {
        if (useAutoOxygen)
            PlayAutoOxygen();

        if (useRemoveOxygen)
            PlayAutoRemoveOxygen();

        AddOxygen(startOxygen);
    }

    public void SpawnBalls(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(oxygenPrefab, new Vector2(Random.Range(spawnArea.x, spawnArea.y), Random.Range(spawnArea.z, spawnArea.w)), Quaternion.identity);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(new Vector2(spawnArea.x, spawnArea.z), new Vector2(spawnArea.y, spawnArea.z));
        Gizmos.DrawLine(new Vector2(spawnArea.x, spawnArea.w), new Vector2(spawnArea.y, spawnArea.w));
        Gizmos.DrawLine(new Vector2(spawnArea.x, spawnArea.z), new Vector2(spawnArea.x, spawnArea.w));
        Gizmos.DrawLine(new Vector2(spawnArea.y, spawnArea.z), new Vector2(spawnArea.y, spawnArea.w));
    }

    public void AddOxygen(float amount)
    {
        if(bobble != null)
            bobble.Play();

        oxygen += amount;
        progressBar.fillAmount = oxygen;

        if (oxygen >= endPhaseOxygen)
        {
            TreeHumorManager.Instance.GoToSleep();
        }
    }

    public void RemoveOxygen(float amount)
    {
        if (oxygen - amount > startOxygen)
        {
            oxygen -= amount;
            progressBar.fillAmount = oxygen;
        }
    }

    private IEnumerator AutoAddOxygen()
    {
        //yield return new WaitForSeconds(3.5f);
        while (true)
        {
            yield return new WaitForSeconds(autoOxygenCooldown);
            SpawnBalls(autoOxygenAmount);
        }
    }

    public void StopAutoOxygen()
    {
        if (autoOxygenCoroutine != null)
            StopCoroutine(autoOxygenCoroutine);
    }

    public void PlayAutoOxygen()
    {
        if (autoOxygenCoroutine != null)
            StopCoroutine(autoOxygenCoroutine);

        autoOxygenCoroutine = StartCoroutine(AutoAddOxygen());
    }

    private IEnumerator AutoRemoveOxygen()
    {
        while (true)
        {
            yield return new WaitForSeconds(autoRemoveOxygenCooldown);
            Debug.Log("removing oxygen");
            RemoveOxygen(autoRemoveOxygenAmount);
        }
    }

    public void StopAutoRemoveOxygen()
    {
        if(autoRemoveOxygenCoroutine != null)
            StopCoroutine(autoRemoveOxygenCoroutine);
    }

    public void PlayAutoRemoveOxygen()
    {
        if(autoRemoveOxygenCoroutine != null)
            StopCoroutine(autoRemoveOxygenCoroutine);

        autoRemoveOxygenCoroutine = StartCoroutine(AutoRemoveOxygen());
    }

    private void OnDestroy()
    {
        StopAutoRemoveOxygen();
        StopAutoOxygen();
    }

    #endregion

}