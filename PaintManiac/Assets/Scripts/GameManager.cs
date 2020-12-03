using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float minimumDepthTillDeath = 100f;
    [SerializeField]
    private float timeTillDeath = 3f;
    [SerializeField]
    private List<Image> healthBarImages = new List<Image>();
    [SerializeField]
    private Image pauseMenu;
    [SerializeField]
    private Button resume;
    [SerializeField]
    private Button restartLevel;
    [SerializeField]
    private Button quit;
    [SerializeField]
    private float score;
    [SerializeField]
    private float scoreDecrement = 1f;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI deathText;
    [SerializeField]
    private Button restartGame;
    [SerializeField]
    private Button mainMenu;
    [SerializeField]
    private GameObject death;

    public static bool changeLevel = false;
    public static bool canMove = true;
    public static bool playerHasDied = false;
    public static bool sphereHasBeenDestroyed;

    public static int hitPoints = 3;

    public static Vector3 spawnLocation = Vector3.zero;

    private GameObject instantiatedPlayer;
    private GameObject sphere;
    private string text;
    private float initialScore;
    private int healthBarIndex = 0;

    private void Start()
    {
        // the player will always spawn at the origin of the world
        instantiatedPlayer = Instantiate(player, spawnLocation, Quaternion.identity);
        sphere = GameObject.FindGameObjectWithTag("Sphere");
        resume.onClick.AddListener(Resume);
        restartLevel.onClick.AddListener(Restart);
        quit.onClick.AddListener(Quit);
        pauseMenu.enabled = false;
        pauseMenu.gameObject.SetActive(false);
        initialScore = score;

        mainMenu.onClick.AddListener(GoToMainMenu);
        restartGame.onClick.AddListener(RestartGame);
        death.SetActive(false);

    }
    private void Update()
    {
        PauseMenu();
        PlayerUpdate();
        GameOver();
        if (changeLevel)
        {                                                                                                                   
            ResetVariables();
            ChangeScene();
        }
        if (sphere != null)
        {
            if (sphere.transform.position.y < -200)
            {
                Destroy(sphere);
                sphereHasBeenDestroyed = true;
            }
        }
        ScoreUpdate();
    }

    private void GoToMainMenu()
    {
        Scene mainMenuScene = SceneManager.GetSceneByBuildIndex(0);
        SceneManager.LoadScene(mainMenuScene.buildIndex);
        Debug.Log("Move to main menu");
        hitPoints = 3;
        spawnLocation = Vector3.zero;
        Time.timeScale = 1f;
        death.SetActive(false);
    }
    private void RestartGame()
    {
        Scene level1 = SceneManager.GetSceneByBuildIndex(1);
        SceneManager.LoadScene(level1.buildIndex);
        Debug.Log("Restarting Game");

        hitPoints = 3;
        spawnLocation = Vector3.zero;
        Time.timeScale = 1f;
        death.SetActive(false);
    }

    private void ScoreUpdate()
    {
        // ensures that there isn't negative score
        if (score < 0)
            score = 0;
        // make so that the scores cannot decrease than more than a 10th of the maximum possible score. 
        if (score > initialScore / 10)
            score -= scoreDecrement * Time.deltaTime;
        scoreText.text = Mathf.Round(score).ToString();
    }
    private void PauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenu.enabled)
        {
            pauseMenu.enabled = true;
            pauseMenu.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;

        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.enabled)
        {
            Resume();
        }
    }
    private void PlayerUpdate()
    {
        // if the player has not been destroyed in any way, shape, or form
        if (instantiatedPlayer != null)
        {
            float playerDepth = instantiatedPlayer.transform.position.y;
            // if the player is below the depth limit
            if (playerDepth <= -minimumDepthTillDeath)
            {
                Destroy(instantiatedPlayer, timeTillDeath);
                playerHasDied = true;
            }
        }
        // if it has been destroyed
        else
        {
            hitPoints -= 1;
            healthBarIndex++;
            healthBarImages[healthBarIndex].enabled = true;
            canMove = true;
            instantiatedPlayer = Instantiate(player, spawnLocation, Quaternion.identity);
            playerHasDied = false;
            score -= 200f;
        }
    }

    private void ResetVariables()
    {
        // these are variables that resets when changing levels
        spawnLocation = Vector3.zero;
        hitPoints = 3;
        canMove = true;
        sphereHasBeenDestroyed = false;
    }

    private void GameOver()
    {
        if (hitPoints <= 0)
        {
            // when it is game over:
            // the player cannot move
            canMove = false;
            // the mesh renderer should be disabled
            instantiatedPlayer.GetComponent<MeshRenderer>().enabled = false;
            // there should be an UI that prompts the player to either restart or go to main menu
            // when the player dies, there will be two buttons: restart & Main Menu
            // exit will go back to scene 1.
            deathText.text = "GAME OVER!";
            death.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Debug.Log("Game Over");
        }
    }

    private void ChangeScene()
    {
        if (changeLevel)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            TextWriter scoreWriter = new StreamWriter("CurrentScore.txt", true);
            scoreWriter.WriteLine(score.ToString());
            scoreWriter.Close();
            if (currentScene.buildIndex == 3)
            {
                // we need to put some important information onto the text file
                // name - [Score]
                StreamReader nameFile = new StreamReader("CurrentName.txt");
                text += nameFile.ReadLine();

                TextWriter tsw = new StreamWriter("Leaderboard.txt", true);
                var scoreFile = File.ReadLines("CurrentScore.txt", Encoding.UTF8);

                foreach (var line in scoreFile)
                {
                    Debug.Log(line);
                    score += Mathf.Round(float.Parse(line));
                }

                text += " - " + Mathf.Round(score).ToString();
                tsw.WriteLine(text);
                tsw.Close();
            }
            SceneManager.LoadScene(currentScene.buildIndex + 1);
            // we changed the level
            changeLevel = false;
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenu.enabled = false;
        pauseMenu.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void Restart()
    {
        Destroy(instantiatedPlayer);
        playerHasDied = true;
        Resume();
    }
    public void Quit()
    {
        Application.Quit();
    }
}
