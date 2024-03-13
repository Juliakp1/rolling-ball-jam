using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class player : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    public float speed;
    public GameObject level1Cube;
    public GameObject level2Cube;
    public GameObject level3Cube;
    public GameObject level4Cube;
    public GameObject level5Cube;
    public GameObject level6Cube;
    public GameObject exitCube;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnTriggerEnter(Collider other)
    {
        if (GameObject.ReferenceEquals(other.gameObject, level1Cube)){
            SceneManager.LoadScene("level1", LoadSceneMode.Single);
        }
        if (GameObject.ReferenceEquals(other.gameObject, level2Cube)) {
            SceneManager.LoadScene("level2", LoadSceneMode.Single);
        }
        if (GameObject.ReferenceEquals(other.gameObject, level3Cube)) {
            SceneManager.LoadScene("level3", LoadSceneMode.Single);
        }
        if (GameObject.ReferenceEquals(other.gameObject, level4Cube)) {
            SceneManager.LoadScene("level4", LoadSceneMode.Single);
        }
        if (GameObject.ReferenceEquals(other.gameObject, level5Cube)) {
            SceneManager.LoadScene("level5", LoadSceneMode.Single);
        }
        if (GameObject.ReferenceEquals(other.gameObject, level6Cube)) {
            SceneManager.LoadScene("level6", LoadSceneMode.Single);
        }
        if (GameObject.ReferenceEquals(other.gameObject, exitCube))
        {
            Debug.Log("Quitting..!");
            Application.Quit();
        }


    }
}
