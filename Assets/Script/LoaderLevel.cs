using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using Zenject;

public class LoaderLevel : MonoBehaviour
{
    public GameObject imgFon;
    public string countLevelToLoad;
    [Inject] private DiContainer diContainer;
    private bool _isReady;
    [SerializeField]private GameObject timer;
    void Start()
    {
        timer.SetActive(false);
        LoadPrefabLevelLocal(countLevelToLoad);
    }

    private async void LoadPrefabLevelLocal(string indexLv) 
    {
        AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(indexLv);
        await handle.Task;
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject prefabInstance = handle.Result;
            diContainer.InstantiatePrefab(prefabInstance, prefabInstance.transform.position, Quaternion.identity, null);
            //Instantiate(prefabInstance, prefabInstance.transform.position, Quaternion.identity);
            Debug.Log("Load prefab " + prefabInstance.name + " success");
        }
        else
        {
            Debug.LogError("Failed to load prefab: " + handle.DebugName);
        }
        timer.SetActive(true);
        imgFon.SetActive(false);
    }

}
