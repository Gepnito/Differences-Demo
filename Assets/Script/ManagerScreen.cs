using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ManagerScreen : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Camera cam;
    private Vector3 minScreenSize;
    private Vector3 maxScreenSize;
    private float targetSizeWorldX;
    private float targetSizeWorldY;
    private GameManager _gameManager;

    [Inject]
    private void Construct(GameManager gameManager)
    {
        //Debug.Log("Inject GameManager");
        _gameManager = gameManager;
    }
    void Start()
    {
        cam = _gameManager.cam;
        float spriteWidth = spriteRenderer.bounds.size.x; // Получаем ширину SpriteRenderer в его локальных координатах
        float spriteHeight = spriteRenderer.bounds.size.y; // Получаем ширину SpriteRenderer в его локальных координатах
        targetSizeWorldX = (spriteWidth * transform.lossyScale.x) + 1; // Преобразуем ширину в глобальные координаты, учитывая масштаб объекта
        targetSizeWorldY = ((spriteHeight * transform.lossyScale.y)*2.5f); // Преобразуем ширину в глобальные координаты, учитывая масштаб объекта

        minScreenSize = cam.ViewportToWorldPoint(new Vector2(0, 0));
        maxScreenSize = cam.ViewportToWorldPoint(new Vector2(1, 1));
        float nowSizeWorldX = maxScreenSize.x - minScreenSize.x;
        float nowSizeWorldY = maxScreenSize.y - minScreenSize.y;
        float newOrtoSizeX = (targetSizeWorldX * cam.orthographicSize) / nowSizeWorldX;
        float newOrtoSizeY = (targetSizeWorldY * cam.orthographicSize) / nowSizeWorldY;
        //Debug.Log("Global width of SpriteRenderer: " + targetSizeWorldX + " ortosizeX = " + newOrtoSizeX + " ortosizeY = " + newOrtoSizeY);
        if (newOrtoSizeX > newOrtoSizeY)
        {
            cam.orthographicSize = newOrtoSizeX;
        }
        else
        {
            cam.orthographicSize = newOrtoSizeY;
        }
    }
}
