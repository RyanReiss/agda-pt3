using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    void Start()
    {
        //DontDestroyOnLoad(this.gameObject);
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++) {
            GameObject obj = (GameObject)Instantiate(objectToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
            obj.transform.parent = this.transform;
        }
        
    }

    public GameObject GetPooledObject() {
        for (int i = 0; i < pooledObjects.Count; i++) {
            if (!pooledObjects[i].activeInHierarchy) {
                return pooledObjects[i];
            }
        }
        CheckSizeOfPool();
        return null;
    }

    public void CheckSizeOfPool(){
        if(pooledObjects.Count < amountToPool){
            while(pooledObjects.Count < amountToPool){
                GameObject obj = (GameObject)Instantiate(objectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
                obj.transform.parent = this.transform;
            }
        }
    }
}
