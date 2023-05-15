using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D player;
    private Vector2 movement;
    public float moveSpeed = 5f;
    private float horizontalInput;
    private float verticalInput;

    public float jumpForce = 5f;

    private bool grounded;

    private bool canClimb = false;
    private bool isClimbing = false;

    private Transform ladder;

    public GameObject gameOverText;

    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        //Debug.Log(horizontalInput);

        verticalInput = Input.GetAxisRaw("Vertical");


        movement.x = horizontalInput * moveSpeed;

         if (canClimb){
            if (verticalInput != 0 && grounded && horizontalInput == 0){
                    isClimbing = true;
            }
        }
        else{
                isClimbing = false;
        }

        

        if(isClimbing ){
            float ladderBottom = ladder.transform.GetChild(1).transform.position.y;
            //Debug.Log("kiipeaa1");
            //Debug.Log("pelaajan y " + player.position.y);
            //Debug.Log("ladder bottom " + ladderBottom);
            if(player.position.y >= ladderBottom){
                //Debug.Log("kiipeaa2");
                player.isKinematic = true;
                movement.y = verticalInput * moveSpeed;
            }
        }
        else{
                player.isKinematic = false;
                movement.y = 0;
        }


        if (Input.GetKeyDown("space") && grounded){
                player.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }

        //Animation
        if(horizontalInput != 0){
            animator.SetBool("Walk", true);
        }
        else{
            animator.SetBool("Walk", false);
        }

        //flip
        if (horizontalInput > 0){
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (horizontalInput < 0){
            transform.localScale = new Vector3(-1, 1, 1);
        }


    }

    void FixedUpdate()
    {
        player.position += movement * Time.fixedDeltaTime;

        
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            grounded = true;
        }
        if (collision.gameObject.CompareTag("Ladder"))
        {
            canClimb = true;
            ladder = collision.transform;
        }
        //Debug.Log(canClimb);
        
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            grounded = false;
        }
        if (collision.gameObject.CompareTag("Ladder"))
        {
            canClimb = false;
        }
        //Debug.Log(canClimb);
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Barrel"))
        {
            GameOver();
        }
    }

    void GameOver(){

        gameOverText.SetActive(true);
        StartCoroutine(GameOverCoolDown());
        //Game over / reset game
        
    }

    IEnumerator GameOverCoolDown()
    {
        
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }






}
