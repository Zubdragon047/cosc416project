using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private const string Level1Key = "Level1Scores";
    private const string Level2Key = "Level2Scores";

    public void SaveScore(int level, float newTime) {
        // check what level this is
        string key = (level == 1) ? Level1Key : Level2Key;
        // grab the saved scores
        List<float> scores = LoadScores(level);
        // add this time to the list
        scores.Add(newTime);
        // grab top 3 fastest times
        scores = scores.OrderBy(s => s).Take(3).ToList();

        for (int i = 0; i < scores.Count; i++)
        {
            PlayerPrefs.SetFloat($"{key}_{i}", scores[i]);
        }
        PlayerPrefs.Save();

    }

    public List<float> LoadScores(int level) {
        // check what level this is
        string key = (level == 1) ? Level1Key : Level2Key;
        // create a new scores list to put the saved values into
        List<float> scores = new List<float>();

        // grab the top 3 scores saved
        for (int i = 0; i < 3; i++)
        {
            if (PlayerPrefs.HasKey($"{key}_{i}"))
                scores.Add(PlayerPrefs.GetFloat($"{key}_{i}"));
        }
        return scores;
    }
    
}
