using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    [SerializeField] List<GameObject> itemList;
    [SerializeField] List<Transform> spawnList;
    [SerializeField] Transform middleTrans;    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnItems());
        Instantiate(itemList[Random.Range(0, itemList.Count)],middleTrans);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnItems()
    {
        WaitForSeconds wait = new WaitForSeconds(15);
        
        while (true) {
            yield return wait;
            Instantiate(itemList[Random.Range(0, itemList.Count)], spawnList[Random.Range(0, spawnList.Count)].transform.position,Quaternion.identity);
        }
    }
}
