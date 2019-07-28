using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    public float scrollSpeed;
    public float tileSizeZ;
    // Start is called before the first frame update
    private Vector3 startPosition;
    private float count;
    private float newPosition;
    void Start()
    {
        count = 0;
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {       
        GameObject gameController = GameObject.Find("Game Controller");
        GameController controllerScript = gameController.GetComponent<GameController>(); 

        if ((controllerScript.score >= controllerScript.Goal) && (count != 200))
        {
            StartCoroutine (Accelerate ());
        }

        else
        {
            newPosition = Mathf.Repeat (Time.time * scrollSpeed, tileSizeZ);
            transform.position = startPosition + Vector3.forward * newPosition;
        }

        if (count == 200)
        {
            StopCoroutine (Accelerate());
            newPosition = Mathf.Repeat (Time.time * scrollSpeed * count, tileSizeZ);                
            transform.position = startPosition + Vector3.forward * newPosition;
        }
    }

    IEnumerator Accelerate ()
    {
        count = count + 0.5f;

        newPosition = Mathf.Repeat (Time.time * scrollSpeed * count, tileSizeZ);                
        transform.position = startPosition + Vector3.forward * newPosition;
        
        //Debug.Log(count);
        yield return null;
    }
}
