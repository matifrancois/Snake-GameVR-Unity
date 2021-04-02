using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject LittleDuck;
    public GameObject chicken;
    public eat comiendo;
    public SnakeMovement movimientoSnake;
    public food comida;
    public CameraFollow snakeCamera;
    public wall_script pared_derecha;
    public wall_script pared_izquierda;
    public wall_script pared_inferior;
    public wall_script pared_superior;
    public TextMesh points;
    public GameObject menu;
    public TextMesh Level;
    public GameObject gameOverBoard;
    public TextMesh scoreGameOver;
    public TextMesh PressXkey;
    public GameObject keyMapping;
    public TextMesh AxisInvert;

    private int gamePoints;
    private bool gameOver;
    private bool isInPause;
    private string[] levels = { "Easy", "Medium", "Hard" };
    private bool isMapping;
    private string[] keys = { "", "", "", "", "" };
    private int indexKey;
    private string[] textMap = { "A", "B", "C", "D", "R1", "is Over" };
    private enum snaKeys : int { A, B, C, D, R1 }
    private bool changeAxisSnake;
    void Start()
    {
        changeAxisSnake = false;
        menu.SetActive(false);
        isInPause = false;
        isMapping = true;
        gameOver = false;
        gamePoints = 0;
        gameOverBoard.SetActive(false);
        mapBoard();
        indexKey = 0;
        PressXkey.text = "Press " + textMap[indexKey];
        if (PlayerPrefs.HasKey("mapped"))
        {
            isMapping = false;
            movimientoSnake.isInMapping = false;
            Time.timeScale = 1;
            PressXkey.gameObject.SetActive(false);
            keyMapping.SetActive(false);
            for (int i = 0; i < 5; i++)
            {
                keys[i] = PlayerPrefs.GetString(textMap[i]);
            }
            movimientoSnake.R1 = keys[(int)snaKeys.R1];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isMapping)
        {
            for (int i = 0; i < 20; i++)
            {
                if (Input.GetKeyDown("joystick button " + i.ToString()))
                {
                    keys[indexKey] = "joystick button " + i.ToString();
                    indexKey += 1;

                    PressXkey.text = "Press " + textMap[indexKey];
                }
            }
            if(indexKey == 5)
            {
                // store the information in the database
                for(int i = 0; i< 5; i++)
                {
                    PlayerPrefs.SetString(textMap[i], keys[i]);
                }
                PlayerPrefs.SetString("mapped", "true");
                movimientoSnake.R1 = keys[(int)snaKeys.R1];
                isMapping = false;
                movimientoSnake.isInMapping = false;
                Time.timeScale = 1;
                PressXkey.gameObject.SetActive(false);
                keyMapping.SetActive(false);
            }
        }
        else
        {
            gameOver = pared_derecha.gameOver || pared_izquierda.gameOver || pared_inferior.gameOver || pared_superior.gameOver;
            if (comiendo.crashed)
            {
                comiendo.crashed = false;
                createNewFood();
                movimientoSnake.AddBodyPart();
            }

            points.text = "Score: " + gamePoints.ToString();

            if (Input.GetKeyDown(keys[(int)snaKeys.D]) && !gameOver)
            {
                if (movimientoSnake.isInPause == false)
                {
                    isInPause = true;
                    movimientoSnake.isInPause = true;
                    menu.SetActive(true);
                    PauseGame();
                }
                else
                {
                    isInPause = false;
                    movimientoSnake.isInPause = false;
                    menu.SetActive(false);
                    ResumeGame();
                }
            }
            //**************************************************
            //                 Menu
            //**************************************************
            if (isInPause)
            {
                if (Input.GetKeyDown(keys[(int)snaKeys.C]))
                {
                    Application.Quit();
                }
                if (Input.GetKeyDown(keys[(int)snaKeys.B]))
                {
                    changeCamara();
                }
                if (Input.GetKeyDown(keys[(int)snaKeys.A]))
                {
                    changeLevel();
                }
                if (Input.GetKeyDown(keys[(int)snaKeys.R1]))
                {
                    changeAxis();
                }
            }

            if (gameOver || comiendo.gameOver)
            {
                gameOverBoard.SetActive(true);
                Time.timeScale = 0;
                scoreGameOver.text = "Your score: " + gamePoints.ToString();
                if (Input.GetKeyDown(keys[(int)snaKeys.A]) || Input.GetKeyDown(keys[(int)snaKeys.B]) || Input.GetKeyDown(keys[(int)snaKeys.C]) || Input.GetKeyDown(keys[(int)snaKeys.D]) || Input.GetKeyDown(keys[(int)snaKeys.R1]))
                {
                    Time.timeScale = 1;
                    Application.LoadLevel(Application.loadedLevel);
                }
            }
        }
        
    }

    void createNewFood()
    {       
         //espera un frame
         StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        bool doItAgain = true;
        while (doItAgain)
        {
            float posX = Random.Range(-8, 8);
            float posZ = Random.Range(-8, 8);
            int selector = Random.Range(0, 10);
            GameObject food;
            if (selector>5)
                food = Instantiate(LittleDuck, new Vector3(posX, 0, posZ), Quaternion.identity) as GameObject;
            else
                food = Instantiate(chicken, new Vector3(posX, 0, posZ), Quaternion.identity) as GameObject;

            food.name = "prueba";
            yield return new WaitForFixedUpdate();
            gamePoints++;
            if (comida.isfine)
            {
                food.name = "littleDuck";
                doItAgain = false;
            }
            else
            {
                Destroy(food);
            }
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
    }

    void changeCamara()
    {
        snakeCamera.newEvento = true;
    }

    void changeLevel()
    {
        movimientoSnake.index_menu += 1;
        Level.text = levels[movimientoSnake.index_menu % 3];
    }

    void mapBoard()
    {
        Time.timeScale = 0;
        isMapping = true;
    }
    void changeAxis()
    {
        changeAxisSnake = !changeAxisSnake;
        movimientoSnake.changeSnakeAxis = changeAxisSnake;
        if (changeAxisSnake)
            AxisInvert.text = "True";
        else
            AxisInvert.text = "False";
    }
}
