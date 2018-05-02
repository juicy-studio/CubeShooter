using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
	public static int score;
	public static int stage;
	private int Cycle=5000;
	public Text HighScore;
	Text text;
	int bossCycle=1;
	int highScore = 0;
	AudioManager audiomanager;

	void Awake()
	{
		if (PlayerPrefs.HasKey ("highscore")) {
			highScore = PlayerPrefs.GetInt ("highscore");
			HighScore.text = "BEST : " + highScore;
		} else {
			PlayerPrefs.SetInt ("highscore", highScore);
		}

		audiomanager = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<AudioManager> ();
		text = GetComponent<Text>();
		score = 0;
		stage = 0;
	}

	void Update()
	{
		text.text = "Score : " + score;
		if (highScore <= score) {
			PlayerPrefs.SetInt ("highscore", score);
			HighScore.text = "BEST : " + score;
		}

		if (stage < 7) {
			if (score >= 750 && stage == 0) {
				stage++;
				audiomanager.nextMusic ();
			}

			if (score >= 1500 && stage == 1) {
				stage++;
				audiomanager.nextMusic ();
			}

			if (score >= 2500 && stage == 2) {
				stage++;
				audiomanager.nextMusic ();
			}

			if (score >= 3000 && stage == 3) {
				stage++;
				audiomanager.nextMusic ();
			}
			if (score >= 3500 && stage == 4) {
				stage++;
				audiomanager.nextMusic ();
			}
			if (score >= 4250 && stage == 5) {
				stage++;
				audiomanager.nextMusic ();
			}
			if (score >= 5000 && stage == 6) {
				stage++;
				audiomanager.nextMusic ();
			}
		} else {
			if (1000<=score - Cycle) {
			/*	if (stage == 2 + 7 * bossCycle || stage == 5 + 7 * bossCycle) {
					stage = stage + 2;
				} else {
					stage++;
				}*/
				stage++;
				Cycle = Cycle + 1000;
			}
		}

	}

}
