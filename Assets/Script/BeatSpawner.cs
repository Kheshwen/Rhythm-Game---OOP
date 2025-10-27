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
    [Tooltip("Movement direction for each corresponding beat.")]
    public MovementDirection[] moveDirections = { MovementDirection.Static, MovementDirection.Static, MovementDirection.Static, MovementDirection.Static, MovementDirection.Static };
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
            // 1. Get the direction from our new "playbook" array
            MovementDirection dir = moveDirections[i];

            // 2. Get the Hit script
            Hit hitScript = newbeat.GetComponent<Hit>();

            // 3. Pass ALL data to the note in one go
            if (hitScript != null)
            {
                hitScript.Initialize(hp, dir);
            }

            // 4. The "miss" timer
            Destroy(newbeat, 1.5f);


            // Update our elapsed time to this beat's spawn time
            elapsedTime = targetSpawnTime;
        }

        Debug.Log("Beatmap finished!");
    }
}