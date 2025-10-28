using UnityEngine;

// This line lets you right-click and create this file in the Project window
[CreateAssetMenu(fileName = "New Beatmap", menuName = "Rhythm Game/Beatmap")]
public class BeatmapData : ScriptableObject
{
    [Header("Beatmap Data")]
    public float bpm = 120.0f;
    public float[] beatsToSpawn;
    public int[] hitPoints;
    public MovementDirection[] moveDirections;
    public AudioClip song;
}