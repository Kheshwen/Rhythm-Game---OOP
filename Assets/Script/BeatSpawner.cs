using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatSpawner : MonoBehaviour
{
    public GameObject beat;

    [Tooltip("The Beats Per Minute (BPM) of the song.")]
    public float bpm = 120.0f;
   
    [Tooltip("Define spawns based on beat numbers. E.g., 0, 1, 2, 3, 3.5, 4")]
    // This array now represents *which beat* to spawn on, not the time in seconds.
    public float[] beatsToSpawn = { 0f, 1f, 2f, 3f, 5f };
    [Tooltip("HP for beat in respective order, example values")]
    public int[] hitPoints = { 1, 2, 3, 1, 2 };

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartSpawn());
    }

    public IEnumerator StartSpawn()
    {
        // Check for invalid BPM
        if (bpm <= 0)
        {
            Debug.LogError("BPM must be greater than 0!");
            yield break; // Stop the coroutine
        }

        // --- New Calculation ---
        // Calculate the duration of a single beat in seconds
        // (60 seconds / beats per minute) = seconds per beat
        float secondsPerBeat = 60.0f / bpm;

        float elapsedTime = 0f; // Tracks the time in seconds

        // Loop through every beat number in our beatmap
        for (int i = 0; i < beatsToSpawn.Length; i++)
        {
            float beatNumber = beatsToSpawn[i];
            int hp = hitPoints[i];
            // --- New Calculation ---
            // Calculate the target time in seconds for this specific beat number
            float targetSpawnTime = beatNumber * secondsPerBeat;

            // Calculate how long we need to wait to reach this specific spawnTime
            float waitTime = targetSpawnTime - elapsedTime;

            // Only wait if it's a positive amount of time
            if (waitTime > 0)
            {
                yield return new WaitForSeconds(waitTime);
            }

            // --- Spawn Logic ---
            Debug.Log("Spawning beat " + beatNumber + " at time: " + targetSpawnTime); // Helpful for debugging
            GameObject newbeat = Instantiate(beat);
            
            newbeat.transform.position = new Vector2(Random.value * 20 - 10, Random.value * 10 - 5);
            Hit hitScript = newbeat.GetComponent<Hit>();
            hitScript.hitPoints = hp;

            // Get the SpriteRenderer to change its color
            SpriteRenderer renderer = newbeat.GetComponent<SpriteRenderer>();

            if (renderer != null) // Always good to check if it exists
            {
                if (hp == 1)
                {
                    renderer.color = Color.red;
                }
                else if (hp == 2)
                {
                    renderer.color = Color.green;
                }
                else if (hp == 3)
                {
                    renderer.color = Color.blue;
                }
            }

            Destroy(newbeat, 1.5f);


            // Update our elapsed time to this beat's spawn time
            elapsedTime = targetSpawnTime;
        }

        Debug.Log("Beatmap finished!");
    }
}