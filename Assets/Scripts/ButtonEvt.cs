using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonEvt : MonoBehaviour
{
    // Start is called before the first frame update
    public void Credit(){
        SceneManager.LoadScene("Credit");
    }
    public void Game(){
        SceneManager.LoadScene("Game");
    }
    public void MainMenu(){
        SceneManager.LoadScene("MainMenu");
    }
    public void Quit(){
        Application.Quit();
    }
    public void Start(){
        Time.timeScale = 1f;
    }
    public void HideObject(){
        gameObject.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
    }
}
