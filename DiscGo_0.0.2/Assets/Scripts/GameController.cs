using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameController : MonoBehaviour {

    public static GameController controller;

	public GameObject disc;
	public GameObject bg;
	public GameObject ball;

	private Renderer discRenderer;
	private Renderer bgRenderer;
	private Renderer ballRenderer;

	private int discColor;
	private int bgColor;
	private int ballColor;

    public GameObject disc2;
    public GameObject disc3;
    public GameObject disc4;
    public GameObject disc5;
    public GameObject disc6;
    public GameObject disc7;
    public GameObject disc8;
    public GameObject disc9;
    public GameObject disc10;
    public GameObject disc11;
    public GameObject disc12;
    public GameObject disc13;
    public GameObject disc14;

	public GameObject heart;
	public GameObject points;
	public GameObject player;
	public GameObject bg1;
	public GameObject menuDisc;
	public GameObject menuBG;

    public GameObject htpItems;
	public GameObject music;
	public GameObject introMusic;

    public GameObject startMenu;
    public GameObject pauseMenu;
    public GameObject scoreMenu;
    public GameObject loseMenu;
    public GameObject htpMenu;
    public GameObject leaderboardMenu;
    public GameObject highscoreMenu;

    public Text scoreText;
    public Text livesText;

    public Camera gameCamera;
	public Camera menuCamera;
	public Camera gameOverCamera;

    public Vector3 spawnValues;

    public int score;
    public int lives;
    public int safeguard;
    public int gameActive;


    //Leaderboard Scores and Names
    public InputField inputField;
    private int tempName;
    public Text[] lbn1;
    public Text[] lbs1;
    public string[] lbNames;
    public int[] lbScores;


    private int rand;
	private int randPU;
    private int pauseActive;
	private float rotation;
	private float border;
	private float spacing;
    private bool firstTimeStart;

    void Start ()
    {
        lbNames = new string[10];
        lbScores = new int[10];
        //lbn1 = new Text[10];
        //lbs1 = new Text[10];
        Load();

        startMenu.SetActive(true);
        pauseMenu.SetActive(false);
        scoreMenu.SetActive(false);
        loseMenu.SetActive(false);
        leaderboardMenu.SetActive(false);
        htpMenu.SetActive(false);
        highscoreMenu.SetActive(false);

        score = 0;
        lives = 1;
        gameActive = 0;
        safeguard = 0;
        pauseActive = 0;

        firstTimeStart = true;

        SetScoreText();
        SetLivesText();
        CreateGameController();

        ballRenderer = ball.GetComponent<Renderer>();
		bgRenderer = bg.GetComponent<Renderer>();
		discRenderer = disc.GetComponent<Renderer>();

		gameCamera.enabled = false;
		menuCamera.enabled = true;
		gameOverCamera.enabled = false;
		music.gameObject.SetActive (false);
		border = 0.75f;
		player.gameObject.SetActive (false);
		bg1.gameObject.SetActive (false);
		htpItems.gameObject.SetActive (false);
    }

    public void LoseGame()
    {
        gameActive = 0;
        gameCamera.enabled = false;
        gameOverCamera.enabled = true;
        player.SetActive(false);


        for (tempName = 0; tempName < 10; tempName++)
        {
            if (score > int.Parse(lbs1[tempName].text))
            {
                for (int i = 9; i > tempName; i--)
                {
                    Debug.Log("hi");
                    lbs1[i].text = lbs1[i-1].text;
                }
                    lbs1[tempName].text = score.ToString();
                    highscoreMenu.SetActive(true);
                    break;
            }

            if (tempName == 9)
            {
                loseMenu.SetActive(true);
            }
        }
    }   

    public void OnSubmit()
    {
        for (int i = 9; i > tempName; i--)
        {
            Debug.Log(i);
            lbn1[i].text = lbn1[i-1].text;
        }
        lbn1[tempName].text = inputField.text;
        highscoreMenu.SetActive(false);
        loseMenu.SetActive(true);
        Save();
    }

    void Update ()
    {
        if (gameActive == 1)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                PauseGame();
            }
        }

        if (pauseActive == 1)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                StartGame();
            }
        }
    }

    public void StartGame()
    {
        gameActive = 1;
        pauseActive = 0;
        Time.timeScale = 1;

        startMenu.SetActive(false);
        pauseMenu.SetActive(false);
        leaderboardMenu.SetActive(false);
        htpMenu.SetActive(false);
        scoreMenu.SetActive(true);

        menuCamera.enabled = false;
        gameCamera.enabled = true;

        htpItems.gameObject.SetActive(false);
        menuDisc.gameObject.SetActive(false);
        menuBG.gameObject.SetActive(false);
        introMusic.gameObject.SetActive(false);

        music.gameObject.SetActive(true);
        player.gameObject.SetActive(true);
        bg1.gameObject.SetActive(true);

        if (firstTimeStart == true)
        {
            firstTimeStart = false;
            Enumerators();
        }

    }

    public void EndGame()
    {
        Debug.Log("Game Over");
        Application.Quit();
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        gameActive = 0;
        pauseActive = 1;
        Time.timeScale = 0;
    }

    public void Leaderboard()
    {
        leaderboardMenu.SetActive(true);
        startMenu.SetActive(false);
        loseMenu.SetActive(false);
    }

    public void HTP()
    {
        htpMenu.SetActive(true);
        startMenu.SetActive(false);
        htpItems.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
        if (lives < 0)
        {
            LoseGame();
        }
    }

	void Enumerators ()
	{
		StartCoroutine(SpawnWaves());
		StartCoroutine(SpawnPU ());
		StartCoroutine(SpawnPickups ());
		StartCoroutine(IntroColorSwitcher());
	}

    //Spawn Disks
    IEnumerator SpawnWaves ()
    {
        while (true)
        {
			yield return new WaitForSeconds(2);
            Vector3 spawnPosition = new Vector3(spawnValues.x, spawnValues.y, spawnValues.z);
            Quaternion spawnRotation = Quaternion.identity;
            int rand = UnityEngine.Random.Range(2, 14);

            //Determine disks
            switch (rand)
            {
                case 2:
                    Instantiate(disc2, spawnPosition, spawnRotation);
                    break;
                case 3:
                    Instantiate(disc3, spawnPosition, spawnRotation);
                    break;
                case 4:
                    Instantiate(disc4, spawnPosition, spawnRotation);
                    break;
                case 5:
                    Instantiate(disc5, spawnPosition, spawnRotation);
                    break;
                case 6:
                    Instantiate(disc6, spawnPosition, spawnRotation);
                    break;
                case 7:
                    Instantiate(disc7, spawnPosition, spawnRotation);
                    break;
                case 8:
                    Instantiate(disc8, spawnPosition, spawnRotation);
                    break;
                case 9:
                    Instantiate(disc9, spawnPosition, spawnRotation);
                    break;
                case 10:
                    Instantiate(disc10, spawnPosition, spawnRotation);
                    break;
                case 11:
                    Instantiate(disc11, spawnPosition, spawnRotation);
                    break;
                case 12:
                    Instantiate(disc12, spawnPosition, spawnRotation);
                    break;
                case 13:
                    Instantiate(disc13, spawnPosition, spawnRotation);
                    break;
                case 14:
                    Instantiate(disc, spawnPosition, spawnRotation);
                    break;
                default:
                    break;
            }
        }
    }

    //Spawn Pickups
	IEnumerator SpawnPickups ()
	{
		while (true) 
		{
			spacing = UnityEngine.Random.Range (0.4f, 0.6f);
			yield return new WaitForSeconds (4.0f + spacing);
			Vector3 spawnPointPosition = new Vector3(UnityEngine.Random.Range(-border,border), UnityEngine.Random.Range(-border,border), 0.0f);
			Quaternion spawnPointRotation = Quaternion.identity;
			Instantiate (points, spawnPointPosition, spawnPointRotation);
		}
	}

	IEnumerator SpawnPU ()
	{
		while (true)
        {
			yield return new WaitForSeconds (14.0f + spacing);
			Vector3 spawnPUPosition = new Vector3 (UnityEngine.Random.Range (-border, border), UnityEngine.Random.Range (-border, border), 0.0f);
			Quaternion spawnPURotation = Quaternion.identity;
			int randPU = UnityEngine.Random.Range (1, 3);

			if (randPU == 1) {
				Instantiate (heart, spawnPUPosition, spawnPURotation);
			}
		}
	}


	IEnumerator ColorSwitcher()
    {
            yield return new WaitForSeconds(1);

            discColor = UnityEngine.Random.Range(1, 21);
            bgColor = UnityEngine.Random.Range(1, 21);
            ballColor = UnityEngine.Random.Range(1, 21);

            //Change Disc Color
            switch (discColor)
            {
                case 1:
                    discRenderer.sharedMaterial.color = Color.red;
                    break;
                case 2:
                    discRenderer.sharedMaterial.color = Color.yellow;
                    break;
                case 3:
                    discRenderer.sharedMaterial.color = Color.blue;
                    break;
                case 4:
                    discRenderer.sharedMaterial.color = Color.green;
                    break;
                case 5:
                    discRenderer.sharedMaterial.color = Color.magenta;
                    break;
                case 6:
                    discRenderer.sharedMaterial.color = Color.cyan;
                    break;
                case 7:
                    discRenderer.sharedMaterial.color = Color.white;
                    break;
                case 8:
                    discRenderer.sharedMaterial.color = Color.black;
                    break;
                case 9:
                    discRenderer.sharedMaterial.color = Color.yellow + Color.red;
                    break;
                case 10:
                    discRenderer.sharedMaterial.color = Color.green + Color.yellow;
                    break;
                case 11:
                    discRenderer.sharedMaterial.color = Color.green + Color.blue;
                    break;
                case 12:
                    discRenderer.sharedMaterial.color = Color.blue + Color.red;
                    break;
                case 13:
                    discRenderer.sharedMaterial.color = Color.blue + Color.magenta;
                    break;
                case 14:
                    discRenderer.sharedMaterial.color = Color.red + Color.magenta;
                    break;
                case 15:
                    discRenderer.sharedMaterial.color = Color.white + Color.blue;
                    break;
                case 16:
                    discRenderer.sharedMaterial.color = Color.white + Color.red;
                    break;
                case 17:
                    discRenderer.sharedMaterial.color = Color.white + Color.green;
                    break;
                case 18:
                    discRenderer.sharedMaterial.color = Color.white + Color.yellow;
                    break;
                case 19:
                    discRenderer.sharedMaterial.color = Color.white + Color.cyan;
                    break;
                case 20:
                    discRenderer.sharedMaterial.color = Color.white + Color.magenta;
                    break;
                default:
                    break;
            }

            //Change BG Color
            switch (bgColor)
            {
                case 1:
                    bgRenderer.material.color = Color.red;
                    break;
                case 2:
                    bgRenderer.material.color = Color.yellow;
                    break;
                case 3:
                    bgRenderer.material.color = Color.blue;
                    break;
                case 4:
                    bgRenderer.material.color = Color.green;
                    break;
                case 5:
                    bgRenderer.material.color = Color.magenta;
                    break;
                case 6:
                    bgRenderer.material.color = Color.cyan;
                    break;
                case 7:
                    bgRenderer.material.color = Color.white;
                    break;
                case 8:
                    bgRenderer.material.color = Color.black;
                    break;
                case 9:
                    bgRenderer.material.color = Color.yellow + Color.red;
                    break;
                case 10:
                    bgRenderer.material.color = Color.green + Color.yellow;
                    break;
                case 11:
                    bgRenderer.material.color = Color.green + Color.blue;
                    break;
                case 12:
                    bgRenderer.material.color = Color.blue + Color.red;
                    break;
                case 13:
                    bgRenderer.material.color = Color.blue + Color.magenta;
                    break;
                case 14:
                    bgRenderer.material.color = Color.red + Color.magenta;
                    break;
                case 15:
                    bgRenderer.material.color = Color.white + Color.blue;
                    break;
                case 16:
                    bgRenderer.material.color = Color.white + Color.red;
                    break;
                case 17:
                    bgRenderer.material.color = Color.white + Color.green;
                    break;
                case 18:
                    bgRenderer.material.color = Color.white + Color.yellow;
                    break;
                case 19:
                    bgRenderer.material.color = Color.white + Color.cyan;
                    break;
                case 20:
                    bgRenderer.material.color = Color.white + Color.magenta;
                    break;
                default:
                    break;
            }

            //Change Ball Color
            switch (ballColor)
            {
                case 1:
                    ballRenderer.material.color = Color.red;
                    break;
                case 2:
                    ballRenderer.material.color = Color.yellow;
                    break;
                case 3:
                    ballRenderer.material.color = Color.blue;
                    break;
                case 4:
                    ballRenderer.material.color = Color.green;
                    break;
                case 5:
                    ballRenderer.material.color = Color.magenta;
                    break;
                case 6:
                    ballRenderer.material.color = Color.cyan;
                    break;
                case 7:
                    ballRenderer.material.color = Color.white;
                    break;
                case 8:
                    ballRenderer.material.color = Color.black;
                    break;
                case 9:
                    ballRenderer.material.color = Color.yellow + Color.red;
                    break;
                case 10:
                    ballRenderer.material.color = Color.green + Color.yellow;
                    break;
                case 11:
                    ballRenderer.material.color = Color.green + Color.blue;
                    break;
                case 12:
                    ballRenderer.material.color = Color.blue + Color.red;
                    break;
                case 13:
                    ballRenderer.material.color = Color.blue + Color.magenta;
                    break;
                case 14:
                    ballRenderer.material.color = Color.red + Color.magenta;
                    break;
                case 15:
                    ballRenderer.material.color = Color.white + Color.blue;
                    break;
                case 16:
                    ballRenderer.material.color = Color.white + Color.red;
                    break;
                case 17:
                    ballRenderer.material.color = Color.white + Color.green;
                    break;
                case 18:
                    ballRenderer.material.color = Color.white + Color.yellow;
                    break;
                case 19:
                    ballRenderer.material.color = Color.white + Color.cyan;
                    break;
                case 20:
                    ballRenderer.material.color = Color.white + Color.magenta;
                    break;
                default:
                    break;
            }

            StartCoroutine(ColorSwitcher());
	}

	IEnumerator IntroColorSwitcher()
    {
            yield return new WaitForSeconds(2);

            for (int i = 0; i < 20; i++)
            {
                discColor = UnityEngine.Random.Range(1, 21);
                bgColor = UnityEngine.Random.Range(1, 21);
                ballColor = UnityEngine.Random.Range(1, 21);


                //Change Disc Color
                switch (discColor)
                {
                    case 1:
                        discRenderer.sharedMaterial.color = Color.red;
                        break;
                    case 2:
                        discRenderer.sharedMaterial.color = Color.yellow;
                        break;
                    case 3:
                        discRenderer.sharedMaterial.color = Color.blue;
                        break;
                    case 4:
                        discRenderer.sharedMaterial.color = Color.green;
                        break;
                    case 5:
                        discRenderer.sharedMaterial.color = Color.magenta;
                        break;
                    case 6:
                        discRenderer.sharedMaterial.color = Color.cyan;
                        break;
                    case 7:
                        discRenderer.sharedMaterial.color = Color.white;
                        break;
                    case 8:
                        discRenderer.sharedMaterial.color = Color.black;
                        break;
                    case 9:
                        discRenderer.sharedMaterial.color = Color.yellow + Color.red;
                        break;
                    case 10:
                        discRenderer.sharedMaterial.color = Color.green + Color.yellow;
                        break;
                    case 11:
                        discRenderer.sharedMaterial.color = Color.green + Color.blue;
                        break;
                    case 12:
                        discRenderer.sharedMaterial.color = Color.blue + Color.red;
                        break;
                    case 13:
                        discRenderer.sharedMaterial.color = Color.blue + Color.magenta;
                        break;
                    case 14:
                        discRenderer.sharedMaterial.color = Color.red + Color.magenta;
                        break;
                    case 15:
                        discRenderer.sharedMaterial.color = Color.white + Color.blue;
                        break;
                    case 16:
                        discRenderer.sharedMaterial.color = Color.white + Color.red;
                        break;
                    case 17:
                        discRenderer.sharedMaterial.color = Color.white + Color.green;
                        break;
                    case 18:
                        discRenderer.sharedMaterial.color = Color.white + Color.yellow;
                        break;
                    case 19:
                        discRenderer.sharedMaterial.color = Color.white + Color.cyan;
                        break;
                    case 20:
                        discRenderer.sharedMaterial.color = Color.white + Color.magenta;
                        break;
                    default:
                        break;
                }

                //Change BG Color
                switch (bgColor)
                {
                    case 1:
                        bgRenderer.material.color = Color.red;
                        break;
                    case 2:
                        bgRenderer.material.color = Color.yellow;
                        break;
                    case 3:
                        bgRenderer.material.color = Color.blue;
                        break;
                    case 4:
                        bgRenderer.material.color = Color.green;
                        break;
                    case 5:
                        bgRenderer.material.color = Color.magenta;
                        break;
                    case 6:
                        bgRenderer.material.color = Color.cyan;
                        break;
                    case 7:
                        bgRenderer.material.color = Color.white;
                        break;
                    case 8:
                        bgRenderer.material.color = Color.black;
                        break;
                    case 9:
                        bgRenderer.material.color = Color.yellow + Color.red;
                        break;
                    case 10:
                        bgRenderer.material.color = Color.green + Color.yellow;
                        break;
                    case 11:
                        bgRenderer.material.color = Color.green + Color.blue;
                        break;
                    case 12:
                        bgRenderer.material.color = Color.blue + Color.red;
                        break;
                    case 13:
                        bgRenderer.material.color = Color.blue + Color.magenta;
                        break;
                    case 14:
                        bgRenderer.material.color = Color.red + Color.magenta;
                        break;
                    case 15:
                        bgRenderer.material.color = Color.white + Color.blue;
                        break;
                    case 16:
                        bgRenderer.material.color = Color.white + Color.red;
                        break;
                    case 17:
                        bgRenderer.material.color = Color.white + Color.green;
                        break;
                    case 18:
                        bgRenderer.material.color = Color.white + Color.yellow;
                        break;
                    case 19:
                        bgRenderer.material.color = Color.white + Color.cyan;
                        break;
                    case 20:
                        bgRenderer.material.color = Color.white + Color.magenta;
                        break;
                    default:
                        break;
                }

                //Change Ball Color
                switch (ballColor)
                {
                    case 1:
                        ballRenderer.material.color = Color.red;
                        break;
                    case 2:
                        ballRenderer.material.color = Color.yellow;
                        break;
                    case 3:
                        ballRenderer.material.color = Color.blue;
                        break;
                    case 4:
                        ballRenderer.material.color = Color.green;
                        break;
                    case 5:
                        ballRenderer.material.color = Color.magenta;
                        break;
                    case 6:
                        ballRenderer.material.color = Color.cyan;
                        break;
                    case 7:
                        ballRenderer.material.color = Color.white;
                        break;
                    case 8:
                        ballRenderer.material.color = Color.black;
                        break;
                    case 9:
                        ballRenderer.material.color = Color.yellow + Color.red;
                        break;
                    case 10:
                        ballRenderer.material.color = Color.green + Color.yellow;
                        break;
                    case 11:
                        ballRenderer.material.color = Color.green + Color.blue;
                        break;
                    case 12:
                        ballRenderer.material.color = Color.blue + Color.red;
                        break;
                    case 13:
                        ballRenderer.material.color = Color.blue + Color.magenta;
                        break;
                    case 14:
                        ballRenderer.material.color = Color.red + Color.magenta;
                        break;
                    case 15:
                        ballRenderer.material.color = Color.white + Color.blue;
                        break;
                    case 16:
                        ballRenderer.material.color = Color.white + Color.red;
                        break;
                    case 17:
                        ballRenderer.material.color = Color.white + Color.green;
                        break;
                    case 18:
                        ballRenderer.material.color = Color.white + Color.yellow;
                        break;
                    case 19:
                        ballRenderer.material.color = Color.white + Color.cyan;
                        break;
                    case 20:
                        ballRenderer.material.color = Color.white + Color.magenta;
                        break;
                    default:
                        break;
                }

                yield return new WaitForSeconds(0.25f);

            }

            yield return new WaitForSeconds(0.3f);
            StartCoroutine(ColorSwitcher());
	}

    private void CreateGameController()
    {
        //if (controller == null)
        {
            //DontDestroyOnLoad(gameObject);      //Makes this object THE game controller and doesn't allow it to be destroyed
            controller = this;
        }
        //else if (controller != this)
        {
            //Destroy(gameObject);                //Prevents new game controllers from being created
        }
    }

    // Saves the game
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/saveInfo.dat");

        SaveData data = new SaveData();

        for (int tempName = 0; tempName < 10; tempName++)
        {
            Debug.Log("BOI");
            lbScores[tempName] = int.Parse(lbs1[tempName].text);
            lbNames[tempName] = lbn1[tempName].text;
        }


        data.lbScores = lbScores;
        data.lbNames = lbNames;

        bf.Serialize(file, data);
        file.Close();
    }

    // Loads the game
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/saveInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/saveInfo.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            lbScores = data.lbScores;
            lbNames = data.lbNames;

            for (int i = 0; i < 10; i++)
            {
                lbs1[i].text = lbScores[i].ToString();
                lbn1[i].text = lbNames[i];
            }
        }
    }
}

[Serializable]
class SaveData
{
    //Leaderboard Scores and Names
    public Text[] lbn1;
    public Text[] lbs1;
    public int[] lbScores;
    public string[] lbNames;
}