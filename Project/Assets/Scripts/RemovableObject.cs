using UnityEngine;

public class RemovableObject : InteractableObject
{
    [SerializeField]
    private int health = 1;

    [SerializeField]
    private Sprite[] healthSprites;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private ParticleSystem hitParticlesPrefab;

    protected override void Awake()
    {
        base.Awake();

        if(healthSprites.Length > 0)
            spriteRenderer.sprite = healthSprites[healthSprites.Length - 1];
    }

    protected override bool CanInteract(ToolType toolType)
    {
        return toolType == ToolType.Remover;
    }

    protected override void InteractionBegin()
    {
        health--;

        if(hitParticlesPrefab != null)
            Instantiate(hitParticlesPrefab, transform.position, Quaternion.identity);

        if (health <= 0)
        {
            DestroyInteractable();
            InteractionSuccess();

        }
        else
        {
            if (healthSprites.Length > health && healthSprites[health] != null)
            {
                spriteRenderer.sprite = healthSprites[health-1];
            }
        }
    }
}
