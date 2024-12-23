using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Molotovbehaviour : MonoBehaviour
{
    private playerPickup playerPickup;

    [SerializeField] float lastingTime;
    [SerializeField] float areaOfEffect;

    [SerializeField] LayerMask hitlayer;
    [SerializeField] GameObject fire;

    private bool isActive;
    public bool canBreak;

    private  AudioSource audioSource;
    [SerializeField] AudioClip molotvSFX;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MolotovExplosion());
        audioSource = GameObject.FindGameObjectWithTag("sfxManager").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

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
                gameObject.tag = "Untagged";
                var fireRot = new Quaternion(fire.transform.rotation.x, fire.transform.rotation.y, fire.transform.rotation.z, fire.transform.rotation.w);
                var clone = Instantiate(fire,gameObject.transform.position,fireRot);
                clone.transform.parent = gameObject.transform;
                clone.transform.localScale = fire.transform.localScale;

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
            gameObject.GetComponent<CapsuleCollider>().isTrigger = false;
            audioSource.PlayOneShot(molotvSFX, 1);
            isActive = true;
            canBreak = false;
        }


    }

    private void OnDrawGizmos()
    {
        // the ground pound sphere for debugging -
         Gizmos.DrawWireSphere(gameObject.transform.position, areaOfEffect);
    }
 }
