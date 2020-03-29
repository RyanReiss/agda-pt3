using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTableController : MonoBehaviour
{

    // DropTableController is a class that controls every characters dropTable in the entire game.

    public GameObject pistolAmmoDropPrefab;
    public GameObject shotgunAmmoDropPrefab;
    public GameObject rifleAmmoDropPrefab;
    public GameObject flamethrowerAmmoDropPrefab;
    public GameObject explosiveBulletEffectDropPrefab;

    List<DropTable> dropTables = new List<DropTable>();

    public struct DropTable {
        public string dropTableName;
        public Dictionary<Drop,float> dropTable; // Droptable is a list of all drops, represented by <Drop(*see Drop Struct below), float dropChance>
                                                 // dropChance is a number between 0f and 100f. dropChance represents the drop's probability of dropping on rolls when added to a drop table.
                                                 // For example. a Drop with a dropChance = 15f would have a 15% chance of dropping when added to a drop table.
    }
    public struct Drop {
        public string dropName; // The name of the drop itself!
        public GameObject dropGameObject; // The actual gameObject of the drop itself
        public float amountToDrop; // The amount to set the drop to if it has the ability to do so.
                                   // ex. Setting the amountToDrop to 20 on a ShotgunAmmoDrop would drop 20 shotgun shells
    }

    private static DropTableController _instance;
    public static DropTableController Instance { get { return _instance; } }


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
        //Debug.Log("Creating Drops....");
        // Drops:
        //-------------------------------------------
        Drop shotgunAmmoDrop_5 = CreateDrop("shotgunAmmoDrop_5", shotgunAmmoDropPrefab, 5f);
        Drop shotgunAmmoDrop_10 = CreateDrop("shotgunAmmoDrop_10", shotgunAmmoDropPrefab, 10f);
        Drop pistolAmmoDrop_10 = CreateDrop("pistolAmmoDrop_10", pistolAmmoDropPrefab, 10f);
        Drop pistolAmmoDrop_20 = CreateDrop("pistolAmmoDrop_20", pistolAmmoDropPrefab, 20f);
        Drop rifleAmmoDrop_10 = CreateDrop("rifleAmmoDrop_10", rifleAmmoDropPrefab, 10f);
        Drop rifleAmmoDrop_20 = CreateDrop("rifleAmmoDrop_20", rifleAmmoDropPrefab, 20f);
        Drop explosiveDrop = CreateDrop("explosiveDrop", explosiveBulletEffectDropPrefab, 1f);

        // Drop Tables:
        //-------------------------------------------

        //Test Drop Table
        DropTable test = CreateDropTable("TestDropTable");
        test.dropTable.Add(shotgunAmmoDrop_5,10f);
        test.dropTable.Add(shotgunAmmoDrop_10,10f);
        test.dropTable.Add(pistolAmmoDrop_10,10f);
        test.dropTable.Add(pistolAmmoDrop_20,10f);
        test.dropTable.Add(rifleAmmoDrop_10,10f);
        test.dropTable.Add(rifleAmmoDrop_20,10f);
        dropTables.Add(test);

        // Porcupine with explosives (Cutscene)
        DropTable PorcupineExplosives = CreateDropTable("PorcupineExplosives");
        PorcupineExplosives.dropTable.Add(explosiveDrop,100f);
        dropTables.Add(PorcupineExplosives);
        //Debug.Log("Amount of dropTables: " + dropTables.Count);
    }

    private DropTable CreateDropTable(string name){
        DropTable temp = new DropTable();
        temp.dropTableName = name;
        temp.dropTable = new Dictionary<Drop, float>();
        return temp;
    }
    private Drop CreateDrop(string name, GameObject drop, float amount){
        Drop temp = new Drop();
        temp.amountToDrop = amount;
        temp.dropName = name;
        temp.dropGameObject = drop;
        return temp;
    }

    private DropTable FindDropTable(string dTName){
        //Debug.Log("Trying to find droptable: " + dTName);
        foreach(DropTable d in dropTables){
            //Debug.Log("Looking at DropTable: " + d.dropTableName);
            if(d.dropTableName == dTName){
                return d;
            }
        }
        Debug.Log("Shouldnt Reach Here!!! Incorrect Drop Table Name Given");
        return new DropTable();
    }

    private Drop RollTable(DropTable dropTable){
        float roll = Random.Range(0f,100f);
        float temp = 0;
        foreach(KeyValuePair<Drop, float> d in dropTable.dropTable){
            temp += d.Value;
            if(temp >= roll){ // If the temp value is more than the roll, pick the current item, as it means that the current dropChance was rolled
                return d.Key;
            }
        }
        //Debug.Log("Return empty drop if no drop was selected");
        return new Drop();
    }

    public void RollDropTable(string nameOfDropTable, Transform positionToSpawnDrop){
        //Debug.Log("DropTableName: " + nameOfDropTable);
        DropTable tableToRoll = FindDropTable(nameOfDropTable);
        Debug.Log(tableToRoll.dropTableName);
        Drop dropToInstantiate = RollTable(tableToRoll);
        if(dropToInstantiate.dropGameObject != null){
            GameObject drop = Instantiate(dropToInstantiate.dropGameObject, positionToSpawnDrop.position, Quaternion.Euler(Vector3.zero));
            if(drop.GetComponent<Pickup>()){
                drop.GetComponent<Pickup>().SetPickupValue(dropToInstantiate.amountToDrop);
            }
        }
    }
}
