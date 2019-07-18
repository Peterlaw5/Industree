using UnityEngine;
using UnityEngine.EventSystems;

public class OxygenBall : MonoBehaviour, IPointerEnterHandler
{
    #region Fields

    [SerializeField]
    private float oxygenAmount = 0.01f;
    [SerializeField]
    private float destroyAfterSeconds = 3f;

    [SerializeField]
    private GameObject particlesExplosionPrefab;

    #endregion

    #region Methods

    private void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Instantiate(particlesExplosionPrefab, transform.position, Quaternion.identity);
        OxygenManager.Instance.AddOxygen(oxygenAmount);
        Destroy(gameObject);
    }

    #endregion
}