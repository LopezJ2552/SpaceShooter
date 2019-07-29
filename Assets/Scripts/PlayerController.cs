using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Boundary
{
     public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
     public float speed;
     public float tilt;
     public Boundary boundary;
     public Text powerUpText;
     private Rigidbody rb;
     private bool pickedUp;

     public GameObject shot;
     public Transform shotSpawn;
     public float fireRate;
     private float nextFire;
     public AudioClip shotSound;
     public AudioSource shotSource;
     private void Start()
     {
          pickedUp = false;
          powerUpText.text = "";
          rb = GetComponent<Rigidbody>();
     }

     void Update ()
     {
          if (Input.GetButton("Fire1") && Time.time > nextFire)
          {
               nextFire = Time.time + fireRate;
               shotSource.clip = shotSound;
               shotSource.Play();
              Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
          }
     }

     void FixedUpdate()
     {
          float moveHorizontal = Input.GetAxis("Horizontal");
          float moveVertical = Input.GetAxis("Vertical");

          Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
          rb.velocity = movement * speed;

          rb.position = new Vector3
          (
               Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
               0.0f,
               Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
          );

          rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
     }

     void OnTriggerEnter(Collider other)
     {
        if ((other.CompareTag ("PickUp")) && pickedUp == false)
        {
          pickedUp = true;  
          powerUpText.text = "Power Up!";
          StartCoroutine (PowerUpTime());    
        }
    }

     IEnumerator PowerUpTime ()
     {
          fireRate = fireRate / 2;
          Debug.Log("Picked Up");
          yield return new WaitForSeconds(5);
          Debug.Log("5 Seconds Later");
          fireRate = fireRate * 2;
          pickedUp = false;
          powerUpText.text = "";
          StopCoroutine (PowerUpTime());
     }
}