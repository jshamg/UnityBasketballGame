﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public Player player;

    public float resetTimer = 5f;

    public int scoreValue;

// Start is called before the first frame update
void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (player.holdingBall == false)
        {
            resetTimer -= Time.deltaTime;
            if (resetTimer <= 0)
            {
                SceneManager.LoadScene("Game");
            }
        }

        if (ScoreCounter.scoreValue == 3)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
