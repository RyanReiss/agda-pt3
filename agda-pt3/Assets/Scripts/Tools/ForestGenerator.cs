using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ForestGenerator : MonoBehaviour
{
    public int forestSizeX; // Overall size of the forest (a square of forestSize * forestSize).
    public int forestSizeY;
    public int elementSpacing; // The spacing between element placements. Basically grid size.
    private GameObject forestParent;
    public int currentForestNumber = 0;
    [SerializeField]
    public string layerToRenderForestOn;
    public int orderInLayerToStartOn;
    private int currentOrderInLayer;

    public Element[] elements;
    SortedDictionary<float,GameObject> forestObjects = new SortedDictionary<float, GameObject>();
    
    public void GenerateNewForest(){
        if(!forestParent){
            forestParent = new GameObject("ForestParent"+currentForestNumber);
            currentForestNumber++;
            forestParent.transform.position = this.transform.position;
            forestParent.transform.parent = this.transform;
        }
        currentOrderInLayer = orderInLayerToStartOn;
        foreach(KeyValuePair<float,GameObject> g in forestObjects){
            DestroyImmediate(g.Value);
        }
        forestObjects.Clear();
        for (int x = 0; x < forestSizeX; x += elementSpacing) {
            for (int y = 0; y < forestSizeY; y += elementSpacing) {

                // For each position, loop through each element...
                for (int i = 0; i < elements.Length; i++) {

                    // Get the current element.
                    Element element = elements[i];

                    // Check if the element can be placed.
                    if (element.CanPlace()) {
                        Vector3 position = new Vector3(x, y, 0f);
                        Vector3 offset = new Vector3(Random.Range(-0.75f, 0.75f), Random.Range(-0.75f, 0.75f), 0f);
                        Vector3 scale = Vector3.one * Random.Range(1.25f, 2f);

                        // Instantiate and place element in world.
                        GameObject newElement = Instantiate(element.GetRandom());
                        newElement.transform.SetParent(forestParent.transform);
                        newElement.transform.localPosition = position + offset;
                        newElement.transform.localScale = scale;
                        forestObjects.Add(newElement.transform.position.y,newElement);
                    }
                }
            }
        }
        foreach(KeyValuePair<float,GameObject> tree in forestObjects){
            // Set the tree's layer
            tree.Value.GetComponent<SpriteRenderer>().sortingLayerName = layerToRenderForestOn;
            tree.Value.GetComponent<SpriteRenderer>().sortingOrder = currentOrderInLayer;
            currentOrderInLayer--;
        }
    }
    public void DeleteCurrentForest(){
        currentOrderInLayer = orderInLayerToStartOn;
        foreach(KeyValuePair<float,GameObject> g in forestObjects){
            DestroyImmediate(g.Value);
        }
        forestObjects.Clear();
    }

    public void CementCurrentForest(){
        forestParent.transform.parent = transform.root;
        forestParent = null;
        currentOrderInLayer = orderInLayerToStartOn;
        forestObjects.Clear();
    }
}

[System.Serializable]
public class Element {

    public string name;
    [Range(1, 10)]
    public int density;

    public GameObject[] prefabs;

    public bool CanPlace () {

        // Validation check to see if element can be placed. More detailed calculations can go here, such as checking perlin noise.

        if (Random.Range(0, 10) < density)
            return true;
        else
            return false;

    }

    public GameObject GetRandom() {

        // Return a random GameObject prefab from the prefabs array.

        return prefabs[Random.Range(0, prefabs.Length)];

    }

}
