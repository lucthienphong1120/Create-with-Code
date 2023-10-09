using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public List<GameObject> targets;
	public TextMeshProUGUI scoreText;
	public TextMeshProUGUI livesText;
	public TextMeshProUGUI gameOverText;
	public GameObject titleScreen;
	public GameObject pauseScreen;
	public Button restartButton;
	private float spawnRate = 1.2f;
	private int score = 0;
	private int lives = 3;
	public bool isGameActive = true;
	public bool paused = false;
	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.P))
		{
			ChangePaused();
		}
	}

	void ChangePaused()
	{
		paused = !paused;
		pauseScreen.SetActive(paused);
		Time.timeScale = paused ? 0 : 1;
	}

	IEnumerator SpawnTarget()
	{
		while (isGameActive)
		{
			yield return new WaitForSeconds(spawnRate);
			int index = Random.Range(0, targets.Count);
			Instantiate(targets[index]);
		}
	}

	public void UpdateScore(int scoreToAdd)
	{
		score += scoreToAdd;
		scoreText.text = "Score: " + score;
	}

	public void UpdateLives(int livesToChange)
	{
		if (lives > 0)
		{
			lives += livesToChange;
			livesText.text = "Lives: " + lives;
		}
		else
		{
			GameOver();
		}
	}

	public void GameOver()
	{
		gameOverText.gameObject.SetActive(true);
		restartButton.gameObject.SetActive(true);
		isGameActive = false;
	}

	public void RestartGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void StartGame(int difficulty)
	{
		isGameActive = true;
		paused = false;
		score = 0;
		lives = 3;
		spawnRate /= difficulty;
		gameOverText.gameObject.SetActive(false);
		restartButton.gameObject.SetActive(false);
		titleScreen.gameObject.SetActive(false);
		UpdateScore(0);
		UpdateLives(0);
		StartCoroutine(SpawnTarget());
	}
}
