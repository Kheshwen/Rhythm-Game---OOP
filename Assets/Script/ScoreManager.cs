using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    public void AddPoint()
    {
        score += 1;
        scoreText.text = score.ToString();
    }

    public static ScoreManager instance;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
    }
    public int score = 0;
    

    public TMP_Text scoreText;

    // Update is called once per frame
    void Update()
    {

    }


}


    

