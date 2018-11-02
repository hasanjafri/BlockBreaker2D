using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    // Config parameters 
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blocksVFX;
    [SerializeField] int maxHits;
    [SerializeField] Sprite[] damageSprites;

    //Cached reference
    Level level;

    // State variables
    [SerializeField] int timesHit; // only serialized for debugging purposes

    private void Start()
    {
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            HandleGettingHit();
        }
    }

    private void HandleGettingHit()
    {
        timesHit++;
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
        int spriteIndex = timesHit - 1;
        GetComponent<SpriteRenderer>().sprite = damageSprites[spriteIndex];
    }

    private void DestroyBlock()
    {
        Destroy(gameObject);
        level.BlockDestroyed();
        PlayBlockDestroySFX();
        TriggerVFX();
    }

    private void PlayBlockDestroySFX()
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        FindObjectOfType<GameSession>().AddToScore();
    }

    private void TriggerVFX()
    {
        GameObject VFX = Instantiate(blocksVFX, transform.position, transform.rotation);
        Destroy(VFX, 1f);
    }
}
