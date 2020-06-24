using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public void Quit() {
        SceneController.Instance.QuitGame();
    }
    
    public void LoadScene(string sceneName) {
        SceneController.Instance.LoadScene(sceneName);
    }
}
