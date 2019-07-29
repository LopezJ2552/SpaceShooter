using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectOnContact : MonoBehaviour
{
    public GameObject explosion;
    private GameController gameController;
    void Start ()
    {
        GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent <GameController>();
        }
        if (gameController == null)
        {
            //Debug.log ("Cannot find 'GameController' script");
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag ("Boundary") || other.CompareTag ("Enemy") || other.CompareTag ("Bolt"))
        {
            return;
        }
        if (other.CompareTag ("Player"))
        {
            Instantiate(explosion, other.transform.position, other.transform.rotation);
        }
        Destroy(gameObject);
    }
}