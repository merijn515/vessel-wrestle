using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MultiplayerController : MonoBehaviour
{
    [SerializeField] Transform spawnPointPirate;
    [SerializeField] Transform spawnPointMarine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i =0; i < GameObject.FindGameObjectsWithTag("Pirate team").Length; i++)
        {
           if( GameObject.FindGameObjectsWithTag("Pirate team")[i].GetComponentInChildren<HealthController>().playerHealth <= 0)
            {
                Debug.Log("Marine Team win");

            } else if(GameObject.FindGameObjectsWithTag("Marine team")[i].GetComponentInChildren<HealthController>().playerHealth <= 0)
            {
                Debug.Log("Pirate team win");
            }
        } 
    }

    public void OnPlayerJoin()
    {

        if (GameObject.FindWithTag("Pirate team") != null && !GameObject.FindWithTag("Pirate team").GetComponentInChildren<HealthController>().joined) 
        {
            GameObject.FindWithTag("Pirate team").gameObject.transform.position = spawnPointPirate.transform.position;
            GameObject.FindWithTag("Pirate team").GetComponentInChildren<HealthController>().joined= true;
            Debug.Log("SpawnedPirate");

        }
        
        if(GameObject.FindWithTag("Marine team") != null && !GameObject.FindWithTag("Marine team" ).GetComponentInChildren<HealthController>().joined)
        {
            GameObject.FindWithTag("Marine team").gameObject.transform.position = spawnPointMarine.transform.position;
            GameObject.FindWithTag("Marine team").GetComponentInChildren<HealthController>().joined= true;
            Debug.Log("Spawned Marine");
        }
      
       
    }
}
