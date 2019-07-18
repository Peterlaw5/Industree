using UnityEngine;

[RequireComponent(typeof(AudioListener))]
public class AudioActivator : MonoBehaviour
{
    private void Start()
    {
        bool soundActive = PlayerPrefs.GetInt("volume", 1) == 0 ? false : true;
        AudioListener.volume = soundActive ? 1 : 0;
    }
}
