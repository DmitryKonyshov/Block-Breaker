using UnityEngine;

public class Block : MonoBehaviour
{
    // Config params
    [SerializeField] AudioClip breakSound;
    [SerializeField] private GameObject blockSparklesVFX;
    [SerializeField] private Sprite[] hitSprites;

    // Cached reference
    Level _level;
    
    // State variables
    [SerializeField] private int timesHit;
    private void Start()
    {
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        _level = FindObjectOfType<Level>();
        if (CompareTag("Breakable"))
        {
            _level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D()
    {
        if (CompareTag("Breakable"))
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        timesHit++;
        var maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        var spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError($"Block sprite is missing from array{gameObject.name}");
        }
    }

    private void DestroyBlock()
    {
        PlayBlockDestroySfx();
        Destroy(gameObject);
        _level.BlockDestroyed();
        TriggerSparklesVFX();
    }

    private void PlayBlockDestroySfx()
    {
        FindObjectOfType<GameSession>().AddToScore();
        if (!(Camera.main is null)) AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
    }

    private void TriggerSparklesVFX()
    {
        var transformSparkles = transform;
        var sparkles = Instantiate(blockSparklesVFX, transformSparkles.position, transformSparkles.rotation);
        Destroy(sparkles, 1f);
    }
}
