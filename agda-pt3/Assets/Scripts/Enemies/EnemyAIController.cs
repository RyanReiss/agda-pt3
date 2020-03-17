using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAIController : MonoBehaviour
{

    public List<BaseEnemyAI> currentEnemies;

    // Singleton setup
    private static EnemyAIController _instance;
    public static EnemyAIController Instance { get { return _instance; } }


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
        currentEnemies = new List<BaseEnemyAI>();
        SceneManager.sceneLoaded += AddEnemiesToController;
        AddEnemiesToController();
    }
    void FixedUpdate()
    {
        foreach(BaseEnemyAI b in currentEnemies){
            b.UpdateEnemy();
        }
    }

    void AddEnemiesToController(Scene scene, LoadSceneMode mode){
        currentEnemies.Clear();
        AddEnemiesToController();
    }

    public void AddEnemiesToController(){
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Enemy")){
            if(g.GetComponent<BaseEnemyAI>() && !currentEnemies.Contains(g.GetComponent<BaseEnemyAI>())){
                currentEnemies.Add(g.GetComponent<BaseEnemyAI>());
            }
        }
    }
    
}
