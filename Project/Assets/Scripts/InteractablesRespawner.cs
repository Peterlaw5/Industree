using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablesRespawner : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private Vector4 spawnArea;

    [SerializeField]
    private int maxInteractablesInScene = 3;

    [SerializeField]
    private float delayBetweenSpawn = 5;

    [SerializeField]
    private List<InteractableObject> interactablesPrefabs;

    [SerializeField]
    private float fixedYSpawn = 0;

    [SerializeField]
    private bool useFixedYSpawn = true;

    [SerializeField]
    private AudioSource spawn;

    [SerializeField]
    private AudioSource elimination;

    private int interactablesCount = 0;

    #endregion

    #region Methods

    private void Start()
    {
        StartCoroutine(SpawnInteractable());
    }

    private IEnumerator SpawnInteractable()
    {
        while (true)
        {
            if ((interactablesCount + 1) <= maxInteractablesInScene)
            {
                
                yield return new WaitForSeconds(delayBetweenSpawn);
                spawn.Play();
                interactablesCount++;

                var interactableObject = Instantiate(interactablesPrefabs[Random.Range(0, interactablesPrefabs.Count)],
                    new Vector2(Random.Range(transform.position.x + spawnArea.x, transform.position.x + spawnArea.y), useFixedYSpawn ? transform.position.y + fixedYSpawn : Random.Range(spawnArea.z, spawnArea.w)),
                    Quaternion.identity) as InteractableObject;

                interactableObject.SetRespawner(this);

            }

            yield return null;
        }
        
    }

    public void RemoveInteractable()
    {
        if (elimination != null)
            elimination.Play();
        interactablesCount--;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector2(transform.position.x + spawnArea.x, transform.position.y + spawnArea.z), new Vector2(transform.position.x + spawnArea.y, transform.position.y + spawnArea.z));
        Gizmos.DrawLine(new Vector2(transform.position.x + spawnArea.x, transform.position.y + spawnArea.w), new Vector2(transform.position.x + spawnArea.y, transform.position.y + spawnArea.w));
        Gizmos.DrawLine(new Vector2(transform.position.x + spawnArea.x, transform.position.y + spawnArea.z), new Vector2(transform.position.x + spawnArea.x, transform.position.y + spawnArea.w));
        Gizmos.DrawLine(new Vector2(transform.position.x + spawnArea.y, transform.position.y + spawnArea.z), new Vector2(transform.position.x + spawnArea.y, transform.position.y + spawnArea.w));
    }

    #endregion
}
