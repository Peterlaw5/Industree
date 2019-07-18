using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour
{
    public void TriggerFade()
    {
        GetComponent<Animator>().SetTrigger("fade");
    }
}
