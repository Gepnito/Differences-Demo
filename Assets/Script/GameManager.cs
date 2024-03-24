using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    public ManagerSave managerSave;
    [SerializeField] private ManagerUi managerUi;
    [HideInInspector] public bool endLevel; //���� ���������� ������ (�������� ��� �����������)
    [HideInInspector] public bool saveLv;//���� ��� �������������� ���������� ���������� ������, �.�. ���������� ����������� ����� ���������� ++
    [Tooltip("����� �� ����������� ������")]
    public float levelTime = 120f; //����� ������� �� ����������� ������ � ��������
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
