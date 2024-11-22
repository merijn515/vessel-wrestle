using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public int playerHealth;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        HealthCheck();
    }

    private void HealthCheck()
    {
        if(playerHealth == 0)
        {
            //ragdoll still needs to be made
            Debug.Log("dead");
        }

      
    }
}
