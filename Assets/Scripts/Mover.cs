using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
     public float speed;

     private Rigidbody rb;

     void Awake()
     {
          GameObject gameController = GameObject.Find("Game Controller");
        GameController controllerScript = gameController.GetComponent<GameController>(); 

        if (controllerScript.hardMode == true)
        {
             speed = speed * 2;
        }
     }
     void Start()
     {
          rb = GetComponent<Rigidbody>();
          rb.velocity = transform.forward * speed;
     }
}