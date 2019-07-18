using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaterController : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private Image waterGFX;

    [SerializeField]
    private AudioSource waterup;

    [SerializeField]
    private float waterStartLevel = 0.75f;

    [SerializeField]
    private float downSpeed = 0.05f;

    private float upSpeed = 0.15f;

    [SerializeField]
    private float dryLevel = 0.20f;

    public event Action OnDry = delegate { };
    public event Action OnEndDry = delegate { };

    private float currentLevel;
    private bool hasCalledEventDry;
    private bool hasCalledEventEndDry;

    #endregion

    #region Methods

    private void Awake()
    {
        waterGFX.fillAmount = waterStartLevel;
        currentLevel = waterStartLevel;
    }

    private void Update()
    {
        if (currentLevel < dryLevel && !hasCalledEventDry)
        {
            OnDry();
            hasCalledEventDry = true;
            hasCalledEventEndDry = false;
            Debug.Log("OnDry");
        }

        if (currentLevel > 0)
        {
            currentLevel -= downSpeed * Time.deltaTime;
            waterGFX.fillAmount = currentLevel;
        }
    }

    public void IncreaseWaterlevel(float amount)
    {
        currentLevel += amount;
        waterup.Play();
        if (currentLevel >= dryLevel && !hasCalledEventEndDry)
        {
            hasCalledEventDry = false;
            hasCalledEventEndDry = true;
            Debug.Log("OnEndDry");
            OnEndDry();
        }

        if (currentLevel > 1)
            currentLevel = 1;

        StopAllCoroutines();
        StartCoroutine(UpdateWaterLevelGFX());
    }

    private IEnumerator UpdateWaterLevelGFX()
    {
        while (waterGFX.fillAmount < currentLevel)
        {
            waterGFX.fillAmount += upSpeed * Time.deltaTime;

            yield return null;
        }
        
        waterGFX.fillAmount = currentLevel;

        yield return null;
    }

    #endregion
}
