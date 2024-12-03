using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public int playerHealth;
    private Rigidbody rb;

    [SerializeField]
    private List<GameObject> hitPointsSprites;
    [SerializeField]
    private Sprite emptyHeartSprite;
    [SerializeField]
    private Sprite fullHeartSprite;

    public bool joined = false;
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
        if(playerHealth <= 0)
        {
            //ragdoll still needs to be made
            RagdollActive();
        }


        for (int i = -1; i < playerHealth; i++)
        {
            if (playerHealth < 10 && playerHealth > 0)
            {
                /*hitPointsSprites[i + 1].SetActive(true);
                hitPointsSprites[0 + playerHealth].SetActive(false);*/

                hitPointsSprites[i + 1].GetComponent<Image>().sprite = fullHeartSprite;
                hitPointsSprites[0 + playerHealth].GetComponent<Image>().sprite = emptyHeartSprite;


                //hitPoints[0].SetActive(false);
                //Debug.Log(0 + playerHealth);
                Debug.Log(i + 1);
            }
            else if(playerHealth <= 0)
            {
                //hitPointsSprites[i + 1].SetActive(false);

                hitPointsSprites[i + 1].GetComponent<Image>().sprite = emptyHeartSprite;


                //Debug.Log("0 health meer over: " + 0 + playerHealth);
                Debug.Log(i + 1);
            } else if (playerHealth == 10)
            {
                Debug.Log(i + 1);
                //Debug.Log(0 + playerHealth + ": 10 health meer over:");
                if (i < 0)
                {
                    //hitPointsSprites[i + 1].SetActive(true);

                    hitPointsSprites[i + 1].GetComponent<Image>().sprite = fullHeartSprite;
                } else
                {
                    //hitPointsSprites[i].SetActive(true);

                    hitPointsSprites[i].GetComponent<Image>().sprite = fullHeartSprite;
                }
            }
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
