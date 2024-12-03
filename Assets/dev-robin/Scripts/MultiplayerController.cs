using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MultiplayerController : MonoBehaviour
{
    [SerializeField] Transform spawnPointPirate;
    [SerializeField] Transform spawnPointMarine;

    [SerializeField] GameObject player2Text;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    { 

        if (GameObject.FindGameObjectWithTag("Marine team") == null)
        {
            Time.timeScale = 0;
            player2Text.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            player2Text.SetActive(false);
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
