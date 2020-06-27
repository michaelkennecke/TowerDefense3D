using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
   public static SceneController Instance;

   private void Awake() {
       if (Instance == null) {
           Instance = this;
           DontDestroyOnLoad(this.gameObject);
       } else {
           Destroy (this.gameObject);
       }
   }

   public void LoadScene(string sceneName) {
       if (sceneName == "Game") {
           Score.Reset();
       }
       SceneManager.LoadScene(sceneName);
   }

   public void StartGame() {
       Score.Reset();
       SceneManager.LoadScene("Game");
   }

   public void ToMainMenu() {
       SceneManager.LoadScene("Menu_Main");
   }

   public void ToEndMenu() {
       GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>().Reset();
       SceneManager.LoadScene("Menu_End");
   }

   public void QuitGame() {
       Application.Quit();
   }


}
