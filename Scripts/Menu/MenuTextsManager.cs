using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuTextsManager : MonoBehaviour
{

    void Awake()
    {
        // High scores texts
        if (this.name.Equals("Easy high score"))
            this.GetComponent<Text>().text = PlayerPrefs.GetInt("playerHighScoreEasy").ToString();
        if (this.name.Equals("Medium high score"))
            this.GetComponent<Text>().text = PlayerPrefs.GetInt("playerHighScoreMedium").ToString();
        if (this.name.Equals("Hard high score"))
            this.GetComponent<Text>().text = PlayerPrefs.GetInt("playerHighScoreHard").ToString();
    }

    void Update()
    {
        // Stars text
        if (this.name.Equals("Stars count (1)"))
            if (PlayerPrefs.GetInt("playerStars") > 99999999)
            {
                this.GetComponent<Text>().text = "99999999";
            }
            else
            {
                this.GetComponent<Text>().text = PlayerPrefs.GetInt("playerStars").ToString();
            }

        // Hearts text
        if (this.name.Equals("Your Hearts count"))
            if (PlayerPrefs.GetInt("playerLife") > 99999999)
            {
                this.GetComponent<Text>().text = "99999999";
            }
            else
            {
                this.GetComponent<Text>().text = PlayerPrefs.GetInt("playerLife").ToString();
            }
    }
}
