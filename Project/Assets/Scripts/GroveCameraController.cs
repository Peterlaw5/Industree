using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GroveCameraController : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private float lerpSpeed = 5f;

    [SerializeField]
    private float lerpAmount = 19.2f;

    [SerializeField]
    private Button rightLerpButton;

    [SerializeField]
    private Button leftLerpButton;

    private bool isLerping = false;

    #endregion

    #region Methods

    private void Start()
    {
        rightLerpButton.onClick.AddListener(LerpToRight);
        leftLerpButton.onClick.AddListener(LerpToLeft);

        rightLerpButton.gameObject.SetActive(true);
        leftLerpButton.gameObject.SetActive(false);
    }

    public void LerpToRight()
    {
        if (isLerping)
            return;

        StartCoroutine(LerpTo(true));
    }

    public void LerpToLeft()
    {
        if (isLerping)
            return;

        StartCoroutine(LerpTo(false));
    }

    private IEnumerator LerpTo(bool isRight)
    {
        var startingPosition = transform.position;

        isLerping = true;

        rightLerpButton.gameObject.SetActive(false);
        leftLerpButton.gameObject.SetActive(false);

        if (isRight)
        {
            while (transform.position.x < startingPosition.x + lerpAmount )
            {
                transform.position += Vector3.right * lerpSpeed * Time.deltaTime;
                yield return null;
            }
            transform.position = startingPosition + Vector3.right * lerpAmount;
            leftLerpButton.gameObject.SetActive(true);

        }
        else
        {
            while (transform.position.x > startingPosition.x - lerpAmount)
            {
                transform.position -= Vector3.right * lerpSpeed * Time.deltaTime;
                yield return null;
            }
            transform.position = startingPosition - Vector3.right * lerpAmount;
            rightLerpButton.gameObject.SetActive(true);

        }

        isLerping = false;

    }

    #endregion
}

