using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class HomeScreenButton : MonoBehaviour
{
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private Button quitButton;
    [SerializeField]
    private TMP_InputField userName;

    private void Start()
    {
        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void StartGame()
    {
        if (userName.text != "")
        {
            TextWriter tsw = new StreamWriter("CurrentName.txt");
            tsw.WriteLine(userName.text);
            Scene currentScene = SceneManager.GetActiveScene();
            TextWriter scoreWriter = new StreamWriter("CurrentScore.txt");
            scoreWriter.WriteLine(0);
            scoreWriter.Close();
            tsw.Close();
            SceneManager.LoadScene(currentScene.buildIndex + 1);
        }
        
    }
    private void QuitGame()
    {
        Application.Quit();
    }
}

