using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class character : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private float delayRestart = 3f;
    private bool dead = false;
    private bool won = false;
    private int count = 0;
    private float timeLimit = 5.2f;
    private float totalPickups;
    private string curScene;

    public TextMeshProUGUI countText;
    public TextMeshProUGUI timerText;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    public GameObject particles;
    public GameObject pickupsParent;
    public AudioSource coinNoise;
    public AudioSource winNoise;
    public AudioSource loseNoise;
    public float speed;

    // Start is called before the first frame update
    void Start(){
        rb = GetComponent<Rigidbody>();
        totalPickups = pickupsParent.transform.childCount;
        SetCountText();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        curScene = SceneManager.GetActiveScene().name;
    }

    private void Update() {

        // extra key mapping
        if (Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(curScene);
        }
        if (Input.GetKeyDown(KeyCode.M)) {
            SceneManager.LoadScene("mainMenu");
        }

        if (dead == true){ // reloads the scene after a few seconds if the player died
            delayRestart -= Time.deltaTime;
            if (delayRestart < 0) {
                SceneManager.LoadScene(curScene);
            }
        }
        if (won == true){
            delayRestart -= Time.deltaTime;
            if (delayRestart < 0){
                SceneManager.LoadScene("mainMenu");
            }
        }

        //time limit counter here
        if (dead == false) {
            timeLimit -= Time.deltaTime;
            if (timeLimit <= 0) {
                loseTextObject.SetActive(true);
                dead = true;
                timeLimit = 0f;
                loseNoise.Play();
            } else if (timeLimit <= 1) {
                timerText.color = Color.red;
            } else if (timeLimit <= 2.5) {
                timerText.color = Color.magenta;
            }
            timerText.text = "Timer: " + timeLimit.ToString("F2");
        }
    }

    private void FixedUpdate(){
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    void OnMove(InputValue movementValue){
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("pickUp")){
            other.gameObject.SetActive(false);
            coinNoise.Play();
            Instantiate(particles, transform.position, Quaternion.Euler(-90,0,0));
            count++;
            SetCountText();
            timeLimit = 5.1f;
            timerText.color = Color.black;
        }
        if (other.gameObject.CompareTag("deadly") && won == false){
            loseTextObject.SetActive(true);
            dead = true;
            loseNoise.Play();
        }
    }
    void SetCountText(){
        countText.text = "Points: " + count.ToString();
        if (count >= totalPickups && dead == false){
            winTextObject.SetActive(true);
            won = true;
            winNoise.Play();
        }
    }

}
