using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molotovbehaviour : MonoBehaviour
{
    private playerPickup playerPickup;

    [SerializeField] float lastingTime;
    [SerializeField] float areaOfEffect;

    [SerializeField] LayerMask hitlayer;

    private bool isActive;
    private bool canBreak;
    // Start is called before the first frame update
    void Start()
    {
        playerPickup = FindAnyObjectByType<playerPickup>();
        StartCoroutine(MolotovExplosion());
    }

    // Update is called once per frame
    void Update()
    {
        if (playerPickup.objectHold == gameObject)
        {
            canBreak = true;
        }

        if (isActive)
        {
            lastingTime -= Time.deltaTime;

            if (lastingTime <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator MolotovExplosion()
    {
        while (true)
        {
            if (isActive)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, areaOfEffect, hitlayer);

                foreach (Collider collider in colliders)
                {
                    yield return new WaitForSeconds(1);
                    collider.GetComponentInChildren<HealthController>().playerHealth -= 1;
                }
               
            }

            yield return null;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ( canBreak && collision.gameObject.tag == "Ground")
        {
            gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
            isActive = true;
        }


    }

    private void OnDrawGizmos()
    {
        // the ground pound sphere for debugging -
         Gizmos.DrawWireSphere(gameObject.transform.position, areaOfEffect);
    }
 }
