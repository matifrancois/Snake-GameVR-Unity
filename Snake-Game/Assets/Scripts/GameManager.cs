using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject LittleDuck;
    public eat comiendo;
    public SnakeMovement movimientoSnake;
    public food comida;
    public wall pared;

    private int gamePoints = 0;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(comiendo.crashed)
        {
            comiendo.crashed = false;
            createNewFood();
            movimientoSnake.AddBodyPart();
            //Debug.Log(puntos.ToString() + " punto!!!");
        }
        if(pared.gameOver)
        {
            Debug.Log("Game Over");
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

            GameObject food = Instantiate(LittleDuck, new Vector3(posX, 0, posZ), Quaternion.identity) as GameObject;
            food.name = "prueba";
            yield return new WaitForFixedUpdate();
            gamePoints++;
            //Debug.Log("el puntaje del juego es: " + gamePoints.ToString());
            //yield return new WaitForSeconds(3);
            if (comida.isfine)
            {
                food.name = "littleDuck";
                doItAgain = false;
            }
        }
    }

}
