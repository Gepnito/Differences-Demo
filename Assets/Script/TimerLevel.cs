using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerLevel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    private float timeRemaining;//Оставшееся время 
    [HideInInspector] public bool timerRunning;//Остановка или запуск таймера 
    [SerializeField] GameManager gameManager;

    private void Start()
    {
        timerRunning = true;
        timeRemaining = gameManager.levelTime;
    }
    private void FixedUpdate()
    {
        if (timerRunning && !gameManager.endLevel) 
        {
            timeRemaining -= Time.fixedDeltaTime;
            if (timeRemaining <= 0)
            {
                timerRunning = false;
                timeRemaining = 0;
                gameManager.LevelOver();
            }
            timerText.text = string.Format("{0:0}:{1:00}", ((int)timeRemaining / 60), (int)timeRemaining % 60);
        }


    }
}
