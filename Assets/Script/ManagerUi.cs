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
        //��������� ���� ������
        StartCoroutine(DelayMenuCompleted());
    }
    IEnumerator DelayMenuCompleted() 
    {
        yield return new WaitForSeconds(0.3f);
        menuCompleted.SetActive(true);

    }
    public void LvOver()
    {
        //��������� ���� ���������
        menuOver.SetActive(true);
    }
    
    public void RestartLevel() 
    {
        //������� ������
        AppMetrica.Instance.ReportEvent("Restart"); //�������� ������� Restart
        AppMetrica.Instance.SendEventsBuffer();//��������� ������� ����������
        Debug.Log("Send Report Event 'Restart'");
        appodealManager.ShowInterstitial(); //� ������ ���������� �������
        if (!gameManager.saveLv) 
        {
            gameManager.managerSave.SaveLevel();
        }
        SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
    }
    public void SetCountLv(int x)
    {
        //��������� ����� ������
        countLv.text = x.ToString();
    }
}
