using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
   public void OnStartClick()
   {
       SceneManager.LoadScene("Day 2");
   }

    public void SelectRed()
    {
        PlayerPrefs.SetString("SelectedLibrary", "Red");
        SceneManager.LoadScene("Day 2");
    }

    public void SelectBlue()
    {
        PlayerPrefs.SetString("SelectedLibrary", "Blue");
        SceneManager.LoadScene("Day 2");
    }

   public void OnExitClick()
   {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
       Application.Quit();
   }
}
