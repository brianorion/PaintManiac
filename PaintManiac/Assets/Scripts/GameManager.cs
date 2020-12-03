using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float minimumDepthTillDeath = 100f;
    [SerializeField]
    private float timeTillDeath = 3f;

    public static bool changeLevel = false;
    public static bool canMove = true;
    public static bool playerHasDied = false;
    public static bool sphereHasBeenDestroyed;

    public static int hitPoints = 3;

    public static Vector3 spawnLocation = Vector3.zero;

    private GameObject instantiatedPlayer;
    private GameObject sphere;
    private float score;
    private string text;

    private void Start()
    {
        // the player will always spawn at the origin of the world
        instantiatedPlayer = Instantiate(player, spawnLocation, Quaternion.identity);
        sphere = GameObject.FindGameObjectWithTag("Sphere");
    }
    private void Update()
    {
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
            canMove = true;
            instantiatedPlayer = Instantiate(player, spawnLocation, Quaternion.identity);
            playerHasDied = false;
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

            Debug.Log("Game Over");
        }
    }

    private void ChangeScene()
    {
        if (changeLevel)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            if (currentScene.buildIndex == 2)
            {
                // we need to put some important information onto the text file
                // name - [Score]
                StreamReader nameFile = new StreamReader("CurrentName.txt");
                text += nameFile.ReadLine();

                TextWriter tsw = new StreamWriter("Leaderboard.txt", true);
                var scoreFile = File.ReadLines("CurrentScore.txt", Encoding.UTF8);

                foreach (var line in scoreFile)
                {
                    score += Int32.Parse(line);
                }

                text += "-" + score.ToString();
                tsw.WriteLine(text);
                tsw.Close();
            }
            SceneManager.LoadScene(currentScene.buildIndex + 1);
            // we changed the level
            changeLevel = false;
        }
    }
}
