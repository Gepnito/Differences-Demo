using System;
using UnityEngine;

public class ManagerSave : MonoBehaviour
{
    private Save sv = new Save();
    [Tooltip("Удаляет сохранения")]
    public bool clear = false;
    private void Start()
    {
        if (clear)
        {
            ClearSave(); //Стираем сейв!
        }
        CheckPrefSave();
    }
    public void CheckPrefSave()
    {
        if (PlayerPrefs.HasKey("Save"))
        {
            sv = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("Save")); //загружаем класс Save со всеми его переменными  
            Debug.Log("Load file save");
        }
        else
        {
            CreateNewSave();   
        }
    }
    private void CreateNewSave() 
    {
        sv.level = 1;
        Debug.Log("Create new file save");
        SaveFileLocal();
    }
    public void ClearSave()
    {
        PlayerPrefs.DeleteKey("Save");
        Debug.Log("Delete file save");
    }
    public void SaveFileLocal()
    {
        PlayerPrefs.SetString("Save", JsonUtility.ToJson(sv)); //сохраняем класс Save со всеми его переменными
        Debug.Log("Save local file");
    }
    //--------------------------------------
    #region SAVE LV COMPLETE Сохранение уровня
    public void SaveLevel()
    {
        sv.level++;
        SaveFileLocal();
    }
    public int LoadLevel()
    {
        return sv.level;
    }
    #endregion
    //--------------------------------------
}

[Serializable]
public class Save
{
    public int level;//Пройденные уровни
}
