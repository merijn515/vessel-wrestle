using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGameButton()
    {
        SceneManager.LoadScene(1);
    }
    public void StartMatch()
    {
        SceneManager.LoadScene(2);
        
    }
    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitGameButton()
    {
        Debug.Log("quit game");
        Debug.Log("ceil " + Mathf.Ceil(Mathf.Infinity));
        Debug.Log("floor " + Mathf.Floor(Mathf.Infinity));
        Debug.Log("ceilToInt " + Mathf.CeilToInt(Mathf.Infinity));
        Debug.Log("floorToInt " + Mathf.FloorToInt(Mathf.Infinity));
        Debug.Log("ceil " + Mathf.Ceil(Mathf.NegativeInfinity));
        Debug.Log("floor " + Mathf.Floor(Mathf.NegativeInfinity));
        Debug.Log("ceilToInt " + Mathf.CeilToInt(Mathf.NegativeInfinity));
        Debug.Log("floorToInt " + Mathf.FloorToInt(Mathf.NegativeInfinity));
        Application.Quit();
    }
}
