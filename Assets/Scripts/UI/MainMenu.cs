using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioClip mainMenuMusic;
    public AudioSource mainMenuSound;

    void Update()
    {
        mainMenuSound.clip = mainMenuMusic;
        if (!mainMenuSound.isPlaying )
            mainMenuSound.PlayOneShot(mainMenuSound.clip);
    }
    public void PlayButton()
    {
        SceneManager.LoadScene("map-92623");
    }

    public void QuitButton()
    {
        Application.Quit();
        Debug.Log("Quit successfully!");
    }

    public void LoadPrologue()
    {
        SceneManager.LoadScene("PrologueScreen-Roger");
    }
}
