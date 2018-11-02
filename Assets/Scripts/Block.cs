using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blocksVFX;

    //Cached reference
    Level level;

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
            DestroyBlock();
        }
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
