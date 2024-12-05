using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] int healingAmount;

    private AudioSource source;

    [SerializeField] AudioClip healClip;
    // Start is called before the first frame update
    void Start()
    {
        source = GameObject.FindGameObjectWithTag("sfxManager").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Pirate team" || other.gameObject.tag == "Marine team")
        {
            if (other.gameObject.GetComponentInChildren<HealthController>().playerHealth < 10)
            {
                other.GetComponentInChildren<HealthController>().playerHealth += healingAmount;
                source.PlayOneShot(healClip, 1);
                Destroy(gameObject);
            }
        }
    }
}
