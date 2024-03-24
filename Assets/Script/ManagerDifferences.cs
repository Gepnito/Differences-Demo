using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using Zenject;

public class ManagerDifferences : MonoBehaviour
{
    [Tooltip("Массив для спрайтов 'отличия'")]
    [SerializeField] SpriteRenderer[] spritesSource_1;
    [SerializeField] SpriteRenderer[] spritesSource_2;
    [Tooltip("Анимационный эффект после нахождения предмета")]
    [SerializeField] GameObject effect;

    private Camera mainCamera;
    private Ray ray;
    private RaycastHit2D hit; 
    private int layerId;
    private int counFindObjects;//количество найденных предметов

    private GameManager _gameManager;

    [Inject]
    private void Construct(GameManager gameManager) 
    {
        //Debug.Log("Inject GameManager");
        _gameManager = gameManager;
    }
    private void Start()
    {
        mainCamera = _gameManager.cam;
    }
    
    private void Update()
    {
#if !UNITY_EDITOR
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                ray = Camera.main.ScreenPointToRay(touch.position);
                hit = Physics2D.GetRayIntersection(ray);
                if (hit.collider != null)
                {
                    CheckDifferences(hit);
                }
            }
        }
#else
        if (Input.GetMouseButtonDown(0) && !_gameManager.endLevel)
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            hit = Physics2D.GetRayIntersection(ray);
            if (hit.collider != null)
            {
                CheckDifferences(hit);
            }
        }    
#endif
    }

    private void CheckDifferences(RaycastHit2D hit)
    {
        //Обрабатываем попадание по sprite
        layerId = hit.transform.GetComponent<SpriteRenderer>().sortingOrder; //Идентифицируем sprite по свойству Order in Layer 
        Vector3 vecSource1 = spritesSource_1[layerId - 2].transform.position; //Находим позицию для парных sprite 
        Vector3 vecSource2 = spritesSource_2[layerId - 2].transform.position;
        Instantiate(effect, vecSource1, hit.transform.rotation); //Создаем новый объект, который воспроизводит анимационный эффект после нахождения предмета
        Instantiate(effect, vecSource2, hit.transform.rotation);
        spritesSource_1[layerId - 2].GetComponent<BoxCollider2D>().enabled = false;//отключаем boxColider чтобы небыло повторного срабатывания RaycastHit2D
        spritesSource_2[layerId - 2].GetComponent<BoxCollider2D>().enabled = false;
        Debug.Log("Найден предмет " + spritesSource_1[layerId - 2].name);
        CheckLevelCompleted();

    }

    private void CheckLevelCompleted()
    {
        //Проверяем сколько предметов найдено
        counFindObjects++;
        if (counFindObjects == spritesSource_1.Length) 
        {
            _gameManager.LevelCompleted();
        }

    }
}

