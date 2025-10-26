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

            SpawnBeat(hitPoints[i]);
            elapsedTime = targetTime;
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