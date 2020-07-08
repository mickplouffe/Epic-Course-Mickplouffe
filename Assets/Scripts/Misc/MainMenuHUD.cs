using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuHUD : MonoBehaviour
{

    public void PlayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
