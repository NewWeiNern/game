using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SpriteEvent : MonoBehaviour
{
    public enum SceneObject{
        None,
        Cart
    }
    public SceneObject select = new SceneObject(); 
    private void OnMouseDown() {
        switch(select){
            case SceneObject.Cart : 
            SceneManager.LoadScene("MainMenu");
            break;
            case SceneObject.None : 
            default :
            break;
        }
    }
}
