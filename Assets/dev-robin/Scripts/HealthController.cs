using System.Collections;
using System.Collections.Generic;
using TMPro;
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

   private MultiplayerController multiplayerController;

    public bool joined = false;

    private AudioSource auidoSource;
    [SerializeField] AudioClip deadClip;
    // Start is called before the first frame update
    void Awake()
    {
        RagdollPartsDisabled();
        rb = GetComponent<Rigidbody>();
        multiplayerController = FindAnyObjectByType<MultiplayerController>();
        auidoSource = GameObject.FindGameObjectWithTag("sfxManager").GetComponent<AudioSource>();
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
            multiplayerController.gameOverText.SetActive(true);
            //ragdoll still needs to be made
            RagdollActive();
            auidoSource.PlayOneShot(deadClip, 2);
           
            if (GameObject.FindGameObjectWithTag("Pirate team").GetComponentInChildren<HealthController>().playerHealth <= 0 )
            {
                
               multiplayerController.gameOverText.GetComponentInChildren<TextMeshProUGUI>().text = "Marine team wins";
            }
            else if (GameObject.FindGameObjectWithTag("Marine team").GetComponentInChildren<HealthController>().playerHealth <= 0 )
            {

              multiplayerController.gameOverText.GetComponentInChildren<TextMeshProUGUI>().text = "Pirate team win";
            }
            
            gameObject.GetComponent<HealthController>().enabled = false;
        }


        for (int i = -1; i < playerHealth; i++)
        {
            if (playerHealth < 10 && playerHealth >= 0)
            {
                #region uiPoging
                /*                int testInt = 10 - playerHealth;
                                if (playerHealth + i > playerHealth && playerHealth + i <= 10)
                                {
                                    Debug.Log(gameObject.name + ": ||hp: " + playerHealth + " ||i: " + i + " ||hp + i: " + (playerHealth + i));
                                }

                                if (playerHealth + )
                                {
                                    Debug.Log(gameObject.name + ": ||hp: " + playerHealth + " ||i: " + i + " ||hp + i: " + (playerHealth + i));
                                }*/
                #endregion
                for (int j = 0; j < 10; j++)
                {
                    int testNum = 0 + j;
                    if (playerHealth + testNum > playerHealth && playerHealth + testNum <= 9)
                    {
                        Debug.Log(gameObject.name + ": ||hp: " + playerHealth + " ||testNum: " + testNum + " ||hp + testNum: " + (playerHealth + testNum));
                        hitPointsSprites[-1 + playerHealth + testNum].GetComponent<Image>().sprite = emptyHeartSprite;
                    }
                }
                //hitPointsSprites[playerHealth].GetComponent<Image>().sprite = emptyHeartSprite;
                hitPointsSprites[9].GetComponent<Image>().sprite = emptyHeartSprite;

                if (i != -1)
                {
                hitPointsSprites[i].GetComponent<Image>().sprite = fullHeartSprite;
                }


                //Debug.Log(i + 1);
            }
            /*else if(playerHealth <= 0)
            {
                hitPointsSprites[i + 1].GetComponent<Image>().sprite = emptyHeartSprite;
                //Debug.Log("0 health meer over: " + 0 + playerHealth);
                //Debug.Log(i + 1);
            } else */if (playerHealth == 10)
            {
                //Debug.Log(i + 1);
                //Debug.Log(0 + playerHealth + ": 10 health meer over:");
                if (i < 0)
                {
                    hitPointsSprites[i + 1].GetComponent<Image>().sprite = fullHeartSprite;
                } else
                {
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
        gameObject.GetComponentInParent<playerMovement>().enabled = false;
        gameObject.GetComponentInParent<PlayerActions>().enabled = false;
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
