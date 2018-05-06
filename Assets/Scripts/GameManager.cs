using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : PlayerController {

	public static GameManager instance = null;
	[SerializeField] private int tankerAttack = 20;
	[SerializeField] private int soldierAttack = 10;
	[SerializeField] private int rangerAttack = 7;
	[SerializeField] private GameObject Hero;
	[SerializeField] private Transform[] SpawnGates;
	[SerializeField] private GameObject[] EnemiesObjects;
	[SerializeField] private Text levelText;
	[SerializeField] private Text KilledText;
	[SerializeField] private GameObject healthPoint;
	[SerializeField] private float timeForHealth = 40;
	[SerializeField] private Transform[] powerUpSpwan;
	[SerializeField] private GameObject speedPowerUp;
	[SerializeField] private int maxPowerUps = 10;
	[SerializeField] private Text endGameText;
	[SerializeField] private int numOfLevels = 10;
	[SerializeField] private GameObject explosion;
	[SerializeField] private float distanceExplosion = 2;

	//[SerializeField] private Transform arrowPosition;




	private int Level = 1;
	private float timeBetweenSpawn = 2;
	private float currentSpawnTime =0;
	private List<EnemyHealth> enemies = new List<EnemyHealth> ();
	private List<EnemyHealth> killedEnemies = new List<EnemyHealth>();
	private List<GameObject> PowerUpsSpawns = new List<GameObject> ();
	private GameObject newEnemy;
	private int numOfEnemiesInLevel;
	private int theSpawnNum;
	private int rndEnemy;
	private int killEnemies = 0;
	private float timertogethealth = 0;
	private float powerUppSpawnTime = 30;
	private float currentPowerUpSpawnTime = 0;
	private int powerUps = 0;
	private PlayerHealth hero;
	private GameObject Smoke;
	private GameObject FireWorks;


	public float Timertogethealth
	{
		set{ Timertogethealth = value; }

	}
	public GameObject HealthPoint{
		get{ return healthPoint; }
	}


	//public Transform ArrowPosition{
	//	get{
	//		return arrowPosition;
	//	}

	//}

	public void registerEnemy(EnemyHealth enemy){
		enemies.Add (enemy);
	}

	public void KilledEnemy(EnemyHealth enemy){
		killedEnemies.Add (enemy);
		killEnemies++;
		changeKilledNum ();

	}

	public void registerPowerUps(GameObject spawn){
		PowerUpsSpawns.Add (spawn);
		powerUps++;
	}


	void Start () {
		Smoke = GameObject.FindGameObjectWithTag ("DustStorm");
		Smoke.SetActive (false);
		FireWorks =GameObject.FindGameObjectWithTag ("FireWorks");
		FireWorks.SetActive (false);
		explosion.SetActive (false);
		hero = Hero.GetComponent<PlayerHealth> ();
		healthPoint.GetComponent<SpriteRenderer> ().enabled = false;
		healthPoint.GetComponent<SphereCollider> ().enabled = false;
		numOfEnemiesInLevel = 1;
		endGameText.GetComponent<Text>().enabled = false;
		StartCoroutine (healthPointIncrease ());
		StartCoroutine (Spawn ());
		StartCoroutine (powerUpsco ());

	}

	// Update is called once per frame
	void Update () {
		currentSpawnTime += Time.deltaTime;
		timertogethealth += Time.deltaTime;
		currentPowerUpSpawnTime += Time.deltaTime;
		if (Level == 5)
			Smoke.SetActive (true);
		if (Level == (numOfLevels + 1)) {
			GameManager.instance.endGame (1);
			gameOver = true;
		}

	}


	IEnumerator Spawn(){
		if (!GameOver && currentSpawnTime >= timeBetweenSpawn ) {
			currentSpawnTime = 0;
			if (enemies.Count < numOfEnemiesInLevel) {
				rndEnemy = Random.Range (0, 2);
				print ("randomnumber = " + rndEnemy.ToString());
				theSpawnNum = Random.Range (0, 5);
				newEnemy = Instantiate (EnemiesObjects[rndEnemy].gameObject);
				newEnemy.transform.position = SpawnGates [theSpawnNum].position;
				yield return new WaitForSeconds ((float)(Level * 0.5));
			} else if (killedEnemies.Count >= enemies.Count) {
				enemies.Clear ();
				killedEnemies.Clear ();
				Level++;
				changeLevelNum ();
				NumOfEnemiesInLevel ();	
				yield return new WaitForSeconds (5f);

			}
			//yield return new WaitForSeconds (timeBetweenSpawn);
		}

		yield return null;
			StartCoroutine (Spawn ());


		}	
	IEnumerator healthPointIncrease ()
	{
		if (timertogethealth >= timeForHealth ) {
			if (!hero.TakeHealth) {
				yield return null;
			}			
			healthPoint.GetComponent<SpriteRenderer> ().enabled = true;
			healthPoint.GetComponent<SphereCollider> ().enabled = true;
			hero.TakeHealth = false;
			yield return new WaitForSeconds (timeForHealth);
		}

		yield return null;
		StartCoroutine (healthPointIncrease ());


	}

	IEnumerator powerUpsco (){
		if (currentPowerUpSpawnTime >= powerUppSpawnTime ) {
			currentPowerUpSpawnTime = 0;
			int randomnum = Random.Range (0, powerUpSpwan.Length - 1);
			GameObject newpowerup = Instantiate (speedPowerUp.gameObject);
			newpowerup.transform.position = powerUpSpwan [randomnum].position;
			newpowerup.transform.rotation = powerUpSpwan [randomnum].rotation;
			registerPowerUps (newpowerup);
		}
		yield return null;
	    StartCoroutine (powerUpsco ());

		



	}

	private void NumOfEnemiesInLevel(){
		numOfEnemiesInLevel = (int)(1.25 * Level);
	}
	private void changeLevelNum(){
		if(Level != (numOfLevels +1))
		levelText.text = "Level " + Level.ToString ();
	}
	private void changeKilledNum(){
		KilledText.text =  killEnemies.ToString ();

	}








	private int heroAttack = 10;
	private bool gameOver = false;

	public int HeroAttack{
		get{return heroAttack;}
	}

	public bool GameOver
	{
		get{ return gameOver; }
		set{ gameOver = value; }
	}

	public GameObject Player
	{
		get{ return Hero; }
	}


	public int TankerAttack
	{
		get{ return tankerAttack; }
	}

	public int SoldierAttack
	{
		get{ return soldierAttack; }
	}

	public int RangerAttack
	{
		get{ return rangerAttack; }
	}


	void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
//		DontDestroyOnLoad (gameObject);
	}





	public void IsHeroDied(int currentHealth)
	{
		if (currentHealth > 0)
			gameOver = false;
		else
			gameOver = true;

	}

	public void HeroAttackValue(int value){
		heroAttack = value;
	}

	public void resettimertogethealth(){
		timertogethealth = 0;
	}


	public void endGame(int num){
		if (num == 0) {
			endGameText.text = "Defeated";
			endGameText.enabled = true;
		} else if (num == 1) {
			endGameText.text = "Victory";
			FireWorks.SetActive (true);
			endGameText.enabled = true;
		}
		StartCoroutine (changeScene ());
	}


	IEnumerator changeScene(){
		yield return new WaitForSeconds (5);
		endGameText.enabled = false;
		SceneManager.LoadScene ("GameMenu");
	yield return  null;


	}

	public void EasyMode(){
		Player.GetComponent<PlayerHealth> ().HealthPower = 1000;
	}

	public void MediumMode(){
		Player.GetComponent<PlayerHealth> ().HealthPower = 500;
	}

	public void HardMode(){
		Player.GetComponent<PlayerHealth> ().HealthPower = 100;
	}


	public void MakeExplosion(Transform enemyPos)
	{
		explosion.SetActive (true);
		GameObject newExplosion = Instantiate (explosion)as GameObject;
		newExplosion.transform.position = enemyPos.position;
		if(Vector3.Distance(enemyPos.position,Player.transform.position) <= distanceExplosion)
			Player.GetComponent<PlayerHealth>().Hurt(30f);
			
		StartCoroutine (resetExplosion (newExplosion));
	}

	IEnumerator resetExplosion(GameObject exp){
		yield return new WaitForSeconds (3f);
		Destroy (exp.gameObject);
		yield return null;
	}
}
