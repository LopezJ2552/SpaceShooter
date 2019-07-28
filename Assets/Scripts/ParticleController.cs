using UnityEngine;
using System.Collections;

public class ParticleController : MonoBehaviour
{
    private ParticleSystem ps;
    void Start()
    {
        ps = GetComponent<ParticleSystem>();

    }

    void Update()
    {
        var main = ps.main;
        GameObject gameController = GameObject.Find("Game Controller");
        GameController controllerScript = gameController.GetComponent<GameController>(); 

        if (controllerScript.score >= controllerScript.Goal)
        {
            main.simulationSpeed = 15.0f;
            //Debug.Log(main.simulationSpeed);
        }
        else 
        {
             main.simulationSpeed = 1.0f;
        }
    }
}