using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MultiplayerController : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayerJoin()
    {

        if (GameObject.FindWithTag("Player 2") != null)
        {
            GameObject.FindWithTag("Player 2").gameObject.transform.position = spawnPoint.transform.position;
            Debug.Log("Spawned");
        }
      
       
    }
}
