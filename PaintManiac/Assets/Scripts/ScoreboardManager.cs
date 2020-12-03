using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Text;
public class ScoreboardManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI input;

    private int index; 
    private void Start()
    {
        input.text = "";
        // check any txt files that contains scoreboard related data
        // the scoreboard will only output the top 10 tries
        var enumLines = File.ReadLines("Leaderboard.txt", Encoding.UTF8);

        // the leaderboard must be sorted first

        foreach (var line in enumLines)
        {
            // this prints out the top 10
            if (index < 10)
            {
                input.text += line + "\n";
            }
        }
    }
}
