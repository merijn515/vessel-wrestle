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
    public bool canExplode;
    // Start is called before the first frame update
    void Start()
    {
        playerPickup = FindAnyObjectByType<playerPickup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            
        }
        MolotovExplosion();
    }

    private void MolotovExplosion()
    {
        float wait =+ Time.time;
        Debug.Log(wait);
        if(wait == lastingTime)
        {
            Destroy(gameObject);
        }
        while (wait < lastingTime)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, areaOfEffect, hitlayer);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (canExplode && collision.gameObject.tag == "Ground")
        {
            gameObject.GetComponentInChildren<GameObject>().SetActive(false);
            isActive = true;
        }
    }
}
