using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    [SerializeField] List<GameObject> itemList;
    [SerializeField] List<Transform> spawnList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnItems()
    {
        WaitForSeconds wait = new WaitForSeconds(15);
        yield return wait;

    }
}
