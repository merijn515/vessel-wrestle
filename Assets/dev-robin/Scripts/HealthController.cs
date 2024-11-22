using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public int playerHealth;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Awake()
    {
        RagdollPartsDisabled();
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
            RagdollActive();
            Debug.Log("dead");
        }

      
    }

    private void RagdollPartsDisabled()
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        Rigidbody[] rigids = GetComponentsInChildren<Rigidbody>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }

        foreach(Rigidbody rigid in rigids)
        {
            rigid.isKinematic = true;
        }
    }

    private void RagdollActive()
    {
        gameObject.GetComponent<Animator>().enabled = false;
        Collider[] colliders = GetComponentsInChildren<Collider>();
        Rigidbody[] rigids = GetComponentsInChildren<Rigidbody>();

        foreach (Collider collider in colliders)
        {
            collider.enabled = true;
        }

        foreach (Rigidbody rigid in rigids)
        {
            rigid.isKinematic = false;
        }

    }
}
