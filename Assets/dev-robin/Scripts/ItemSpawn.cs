using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    [SerializeField] List<GameObject> itemList;
    [SerializeField] List<GameObject> healthList;
    [SerializeField] List<Transform> spawnList;
    [SerializeField] Transform middleTrans;

    [SerializeField] float timeForItem;
    [SerializeField] float timeForHealthPickup;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnItems());
        StartCoroutine(SpawnHealthItems());
        Instantiate(itemList[Random.Range(1, itemList.Count)],middleTrans.position,Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnItems()
    {
        WaitForSeconds wait = new WaitForSeconds(timeForItem);
        
        while (true) {
            yield return wait;
            var randomItem = Random.Range(0, itemList.Count);
            

            if (itemList[randomItem].gameObject.GetComponent<HealthPickup>())
            {
                itemList[randomItem].gameObject.transform.position = middleTrans.position;
            }
            else
            {
                Instantiate(itemList[randomItem], spawnList[Random.Range(0, spawnList.Count)].transform.position, Quaternion.identity);
            }
        }
    }

    private IEnumerator SpawnHealthItems()
    {
        WaitForSeconds wait = new WaitForSeconds(timeForHealthPickup);

        while (true)
        {
            yield return wait;
            var randomItem = Random.Range(0, healthList.Count);


            Instantiate(healthList[randomItem],middleTrans.position,Quaternion.identity);
           
        }
    }
}
