using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class VolumeButton : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private Sprite volumeActive;

    [SerializeField]
    private Sprite volumeInactive;

    private Image image;

    #endregion


    #region Methods

    private void Awake()
    {
        image = GetComponent<Image>();

        bool soundActive = PlayerPrefs.GetInt("volume", 1) == 0 ? false : true;

        image.sprite = soundActive ? volumeActive : volumeInactive;
        AudioListener.volume = soundActive ? 1 : 0;
    }

    public void ToggleVolume()
    {
        bool soundActive = PlayerPrefs.GetInt("volume", 1) == 0 ? false : true;
        soundActive = !soundActive;

        PlayerPrefs.SetInt("volume", soundActive ? 1: 0);

        image.sprite = soundActive ? volumeActive : volumeInactive;
        AudioListener.volume = soundActive ? 1 : 0;

    }

    #endregion

}
