using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    // This variable will be set by your BeatSpawner
    public int hitPoints;

    private SpriteRenderer renderer;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // This runs on every click
    private void OnMouseDown()
    {
        // 1. First, we reduce the HP
        hitPoints--;

        // 2. Now, we check the NEW HP value

        if (hitPoints == 2)
        {
            // It was 3, now it's 2. Change color to Green.
            renderer.color = Color.green;
        }
        else if (hitPoints == 1)
        {
            // It was 2, now it's 1. Change color to Red.
            renderer.color = Color.red;
        }
        else if (hitPoints <= 0)
        {
            // It was 1, now it's 0. Destroy it.
            Destroy(gameObject);
        }
    }
}