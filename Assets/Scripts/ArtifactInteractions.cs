using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ArtifactInteractions : MonoBehaviour
{
    public int numberOfArtifactsFound;
    public int numberOfArtifacts;

    public float displayTextTime;
    public GameObject text;

    // Updates # of artifacts found, changes text accordingly, and starts timer
    public void ArtifactGrabbed()
    {
        numberOfArtifactsFound++;

        if (numberOfArtifactsFound >= numberOfArtifacts)
            WinGame();

        text.GetComponent<TMPro.TextMeshProUGUI>().text = numberOfArtifactsFound + "/" + numberOfArtifacts + " Artifacts Found";
        StartCoroutine("Timer", displayTextTime);
    }

    // Shows text for a certain number of time
    IEnumerator Timer(float displayTimer)
    {
        text.SetActive(true);
        yield return new WaitForSeconds(displayTimer);
        text.SetActive(false);
    }

    private void WinGame()
    {
        SceneManager.LoadScene("WinScreen-Roger");
    }
}
