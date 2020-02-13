using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingController : MonoBehaviour
{
    // Class used to creat GameObject pools for objects that need to be created and destroyed very quicky.
    [SerializeField]
    public List<ObjectPooler> pools;
    private static ObjectPoolingController _instance;
    public static ObjectPoolingController Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetPooledObject(GameObject objectToGet){
        foreach(ObjectPooler oP in pools){
            if(oP.objectToPool == objectToGet){
                return oP.GetPooledObject();
            }
        }
        Debug.Log("Shouldnt reach here!");
        return null;
    }

    // private void InitializeGameObjects(){
    //     foreach(KeyValuePair<GameObject,int> g in prefabDictionary){
    //         GameObject parentStorageObject = new GameObject(g.Key.name);
    //         parentStorageObject.transform.parent = this.transform;
    //         prefabStorageGameObjects.Add(parentStorageObject);
    //         for(int i = 0; i < g.Value; i++){
    //             GameObject temp = Instantiate(g.Key);
    //             temp.name = g.Key.name;
    //             temp.transform.parent = parentStorageObject.transform;
    //             temp.SetActive(false);
    //         }
    //     }
    // }

    // public GameObject RemovePrefabFromPool(GameObject prefab, Vector3 positionToSpawn, Quaternion rotationToSpawn){
    //     foreach(GameObject pool in prefabStorageGameObjects){
    //         if(pool.name == prefab.name){
    //             return ActivateNextObjectInPool(pool,positionToSpawn,rotationToSpawn);
    //         }
    //     }
    //     Debug.Log("Cannot find prefab!!!");
    //     return new GameObject();
    // }

    // private GameObject ActivateNextObjectInPool(GameObject pool, Vector3 positionToSpawn, Quaternion rotationToSpawn){
    //     if(pool.transform.childCount > 0){
    //         GameObject temp = pool.transform.GetChild(0).gameObject;
    //         temp.transform.parent = transform.root;
    //         temp.gameObject.transform.position = positionToSpawn;
    //         temp.gameObject.transform.rotation = rotationToSpawn;
    //         temp.SetActive(true);
    //         return temp;
    //     } else {
    //         return null;
    //     }
    // }

    // public void AddPrefabToPool(GameObject prefabToAdd){
    //     foreach(GameObject pool in prefabStorageGameObjects){
    //         if(pool.name == prefabToAdd.name){
    //             prefabToAdd.transform.parent = pool.transform;
    //             prefabToAdd.SetActive(false);
    //         }
    //     }
    // }

    // public bool HasPoolForGameObject(GameObject g){
    //     foreach(GameObject pool in prefabStorageGameObjects){
    //         if(pool.name == g.name){
    //             return true;
    //         }
    //     }
    //     Debug.Log("No Pool!!");
    //     return false;
    // }
}
