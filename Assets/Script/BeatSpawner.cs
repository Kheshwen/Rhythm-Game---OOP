using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatSpawner : MonoBehaviour
{
    [Tooltip("Drag your Beatmap asset file here")]
    public BeatmapData currentBeatmap;
    public GameObject beat;

    
    // Start is called before the first frame update
    void Start()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = currentBeatmap.song;
        audio.Play();
        StartCoroutine(StartSpawn());

    }

    public IEnumerator StartSpawn()
    {
        // Check for invalid BPM
        if (currentBeatmap.bpm <= 0)
        {
            Debug.LogError("BPM must be greater than 0!");
            yield break; // Stop the coroutine
        }

        // --- New Calculation ---
        // Calculate the duration of a single beat in seconds
        // (60 seconds / beats per minute) = seconds per beat
        float secondsPerBeat = 60.0f / currentBeatmap.bpm;

        float elapsedTime = 0f; // Tracks the time in seconds

        // Loop through every beat number in our beatmap
        for (int i = 0; i < currentBeatmap.beatsToSpawn.Length; i++)
        {
            float beatNumber = currentBeatmap.beatsToSpawn[i];
            int hp = currentBeatmap.hitPoints[i];
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
            MovementDirection dir = currentBeatmap.moveDirections[i];

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