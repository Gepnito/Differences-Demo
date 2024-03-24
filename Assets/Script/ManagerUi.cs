using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class ManagerUi : MonoBehaviour
{
    [SerializeField] GameObject menuCompleted;
    [SerializeField] GameObject menuOver;
    [SerializeField] TextMeshProUGUI countLv;
    private GameManager gameManager;
    [SerializeField] AppodealManager appodealManager;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        menuCompleted.SetActive(false);
        menuOver.SetActive(false);
    }
 
    public void LvCompleted() 
    {
        //Открываем меню победы
        StartCoroutine(DelayMenuCompleted());
    }
    IEnumerator DelayMenuCompleted() 
    {
        yield return new WaitForSeconds(0.3f);
        menuCompleted.SetActive(true);

    }
    public void LvOver()
    {
        //Открываем меню проигрыша
        menuOver.SetActive(true);
    }
    
    public void RestartLevel() 
    {
        //Рестарт уровня
        AppMetrica.Instance.ReportEvent("Restart"); //Отправка события Restart
        AppMetrica.Instance.SendEventsBuffer();//Отправить событие немедленно
        Debug.Log("Send Report Event 'Restart'");
        appodealManager.ShowInterstitial(); //в теории показываем рекламу
        if (!gameManager.saveLv) 
        {
            gameManager.managerSave.SaveLevel();
        }
        SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
    }
    public void SetCountLv(int x)
    {
        //Назначаем номер уровня
        countLv.text = x.ToString();
    }
}
