using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI lastestScoreText;
    void Start()
    {
        lastestScoreText.text = string.Format("YOU WERE IN CHURCH FOR:\n{0:F2}",PlayerPrefs.GetFloat("lastScore",0));
        highScoreText.text = string.Format("LONGEST TIME:\n{0:F2}",PlayerPrefs.GetFloat("highScore",0));
    }

    public void Restart(){
        SceneManager.LoadScene("MainMenu");
    }
}
