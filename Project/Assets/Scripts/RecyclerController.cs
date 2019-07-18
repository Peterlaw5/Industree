using System.Collections.Generic;
using UnityEngine;

public class RecyclerController : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private List<RecycleBin> bins;

    [SerializeField]
    private AudioSource[] plasticglasspaper;

    [SerializeField]
    private ToolsController toolsController;

    [SerializeField]
    private GameObject recyclerUI;

    [SerializeField]
    private ParticleSystem goodRecycleParticlesPrefab;

    [SerializeField]
    private Transform goodRecycleParticleSpawnPoint;

    private RecyclableObject entry;

    
    #endregion

    #region Methods

    private void Start()
    {
        CloseRecycler();
        toolsController.OnToolChanged += (x) => CloseRecycler();

        InitBins();

    }

    private void InitBins()
    {
        foreach (var bin in bins)
        {
            bin.Init(this);
        }
    }

    public void OpenRecycler(RecyclableObject _entry)
    {
        entry = _entry;
        recyclerUI.SetActive(true);
    }

    public void CloseRecycler()
    {
        entry = null;
        recyclerUI.SetActive(false);
    }

    public void Recycle(RecyclableType recyclableType)
    {
        if (entry == null)
        {
            return;
        }

        if (entry.TypeRecyclable == recyclableType)
        {
            Debug.Log("Good Recycle");
            switch (recyclableType)
            {
                case RecyclableType.plastic:
                    plasticglasspaper[0].Play();
                    break;
                case RecyclableType.glass:
                    plasticglasspaper[1].Play();
                    break;
                case RecyclableType.paper:
                    plasticglasspaper[2].Play();
                    break;
            }

            Instantiate(goodRecycleParticlesPrefab, goodRecycleParticleSpawnPoint.transform.position, Quaternion.identity);

            entry.InteractionSuccess();
            entry.DestroyInteractable();
        }
        else
        {
            Debug.Log("Bad Recycle");
        }

        CloseRecycler();

        entry = null;
    }

    #endregion
}
