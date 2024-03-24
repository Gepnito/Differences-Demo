using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    public ManagerSave managerSave;
    [SerializeField] private ManagerUi managerUi;
    [HideInInspector] public bool endLevel; //‘лаг завершени€ уровн€ (проигрыш или прохождение)
    [HideInInspector] public bool saveLv;//‘лаг дл€ предотвращени€ повторного сохранени€ уровн€, т.к. сохранение реализована путем инкремента ++
    [Tooltip("¬рем€ на прохождение уровн€")]
    public float levelTime = 120f; //¬сего времени на прохождени€ уровн€ в секундах
    public Camera cam;
    private void Start()
    {
        managerUi.SetCountLv(managerSave.LoadLevel());
    }

    public void LevelCompleted()
    {
        endLevel = true;
        managerSave.SaveLevel();
        saveLv = true;
        managerUi.LvCompleted();
        Debug.Log("Level Completed");
    }
    public void LevelOver()
    {
        endLevel = true;
        managerUi.LvOver();
        Debug.Log("Level Over");
    }



}
