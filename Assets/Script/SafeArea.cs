using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeArea : MonoBehaviour
{
    private RectTransform myRecTransform;
    private Rect safeArea;

    void Start()
    {
        myRecTransform = GetComponent<RectTransform>();
        safeArea = Screen.safeArea;
        var anchorMin = safeArea.position;
        var anchorMax = safeArea.position + safeArea.size;
        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        //anchorMin.y = 0;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;
        myRecTransform.anchorMin = anchorMin;
        myRecTransform.anchorMax = anchorMax;
    }

   
}
