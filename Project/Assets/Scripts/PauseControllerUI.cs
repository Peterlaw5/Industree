using UnityEngine;

public class PauseControllerUI : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private GameObject pausePanel;
    private bool isPaused = false;

    #endregion

    #region Methods

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        pausePanel.SetActive(isPaused);
    }

    #endregion

}
