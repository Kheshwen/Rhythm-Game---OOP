using System.Collections;
using UnityEngine;

public class BeatSpawner : MonoBehaviour
{
    [Header("Beat Settings")]
    public GameObject beat;
    public float bpm = 120.0f;

    [Tooltip("Define beats to spawn based on beat numbers.")]
    public float[] beatsToSpawn = { 0f, 1f, 2f, 3f, 5f };

    [Tooltip("Hit points for each beat.")]
    public int[] hitPoints = { 1, 2, 3, 1, 2 };

    [Header("Random Bonus Beat Settings")]
    public bool enableRandomBeats = true;
    public float randomSpawnInterval = 1.5f;
    public int randomBeatHP = 1;

    [Header("Spawn Area")]
    public Rect spawnArea = new Rect(-10f, -5f, 20f, 10f);

    [Tooltip("Seconds before auto-destroy beats.")]
    public float autoDestroySeconds = 1.5f;

    private void Start()
    {
        StartCoroutine(SpawnMainBeatsCoroutine());
        if (enableRandomBeats)
            StartCoroutine(SpawnRandomBeatsCoroutine());
    }

    private IEnumerator SpawnMainBeatsCoroutine()
    {
        if (beat == null || beatsToSpawn.Length != hitPoints.Length)
        {
            Debug.LogError("Check prefab and array lengths!");
            yield break;
        }

        float secondsPerBeat = 60.0f / bpm;
        float elapsedTime = 0f;

        for (int i = 0; i < beatsToSpawn.Length; i++)
        {
            float targetTime = beatsToSpawn[i] * secondsPerBeat;
            float waitTime = targetTime - elapsedTime;
            if (waitTime > 0f) yield return new WaitForSeconds(waitTime);

<<<<<<< HEAD
            SpawnBeat(hitPoints[i]);
            elapsedTime = targetTime;
=======
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
>>>>>>> 2c187b1a05f503d8586a5f5b2c5be9268f54fe7a
        }

        Debug.Log("Beatmap finished!");
    }

    private IEnumerator SpawnRandomBeatsCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(randomSpawnInterval);
            SpawnBeat(randomBeatHP);
        }
    }

    private void SpawnBeat(int hp)
    {
        GameObject newBeat = Instantiate(beat);

        // Random position inside spawn area
        float randomX = Random.Range(spawnArea.x, spawnArea.x + spawnArea.width);
        float randomY = Random.Range(spawnArea.y, spawnArea.y + spawnArea.height);
        newBeat.transform.position = new Vector3(randomX, randomY, 0f);

        Hit hitScript = newBeat.GetComponent<Hit>();
        if (hitScript != null)
        {
            hitScript.hitPoints = hp;
            hitScript.SetColorByHP();
        }

        if (autoDestroySeconds > 0f)
            Destroy(newBeat, autoDestroySeconds);
    }
}