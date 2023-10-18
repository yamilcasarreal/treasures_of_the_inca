using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHP : MonoBehaviour
{
    public GameObject text;
    public int currentHP = 100;

    public void changePlayerHP(int changeInHP)
    {
        currentHP += changeInHP;
        text.GetComponent<TMPro.TextMeshProUGUI>().text = currentHP + "";

        if (currentHP <= 0)
        {
            SceneManager.LoadScene("Roger-UI-Changes");
        }
    }
}
