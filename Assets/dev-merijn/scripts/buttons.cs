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
    public void Rematch()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitGameButton()
    {
        Application.Quit();
    }
}
