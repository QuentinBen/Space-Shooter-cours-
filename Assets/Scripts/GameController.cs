using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{
	//Asteroids Waves infos
	public GameObject hazard;
	public Vector3 spawnValues;		//Spawn coordonnates  limits
	public int hazardCount;			//Nb of spawns
	public float spawnWait;			//Time between 2 spawns
	public float startWait;			//Time until 1st wave starts
	public float waveWait;			//Time between 2 waves

	public Text scoreText;
	private int score;

	void Start()
	{
		score = 0;
		UpdateScore ();

		StartCoroutine (SpawnWaves());
	}

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds (startWait);

		while (true) 
		{
			for (int i = 0; i < hazardCount; i++) 
			{
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;

				Instantiate (hazard, spawnPosition, spawnRotation);

				yield return new WaitForSeconds (spawnWait);
			}

			yield return new WaitForSeconds (waveWait);
		}
	}

	public void AddScore(int scoreValue)
	{
		score += scoreValue;
		UpdateScore ();
	}

	void UpdateScore()
	{
		scoreText.text = "Score : " + score.ToString ();
	}
}