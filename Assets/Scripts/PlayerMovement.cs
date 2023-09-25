using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{   
    public float speed = 10;
    private Rigidbody2D marioBody;
    public float upSpeed = 10;
    private bool onGroundState = true;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;
    public TextMeshProUGUI scoreText;
    public GameObject enemies;
    public JumpOverGoomba jumpOverGoomba;
   
    public Animator marioAnimator;
    public AudioSource marioAudio;
    public AudioClip marioDeath;
    public GameManager gameManager;
    public float deathImpulse;

    [System.NonSerialized]
    public bool alive = true;
    int collisionLayerMask = (1 << 3) | (1 << 6) | (1 << 7);

    // Start is called before the first frame update
    void Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a") && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;
            if (marioBody.velocity.x > 0.1f)
                marioAnimator.SetTrigger("onSkid");
        }

        if (Input.GetKeyDown("d") && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;
            marioSprite.flipX = false;
            if (marioBody.velocity.x < -0.1f)
                marioAnimator.SetTrigger("onSkid");
        }


        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
    }
    public float maxSpeed = 20;
    void OnCollisionEnter2D(Collision2D col)
    {
        if (((collisionLayerMask & (1 << col.transform.gameObject.layer)) > 0) & !onGroundState)
        {

            onGroundState = true;
            // update animator state
            marioAnimator.SetBool("onGround", onGroundState);
        }

    }
    // FixedUpdate may be called once per frame. See documentation for details.
    void FixedUpdate()
    {
        float moveHorizontal = 0;
        if (alive)
        {
             moveHorizontal = Input.GetAxisRaw("Horizontal");
        }
        

        if (Mathf.Abs(moveHorizontal) > 0)
        {
            Vector2 movement = new Vector2(moveHorizontal, 0);
            // check if it doesn't go beyond maxSpeed
            if (marioBody.velocity.magnitude < maxSpeed)
                marioBody.AddForce(movement * speed);
        }

        // stop
        if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
        {
            // stop
            marioBody.velocity = Vector2.zero;
        }
        if (Input.GetKeyDown("space") && onGroundState)
        {
            onGroundState = false;
            marioAnimator.SetBool("onGround", onGroundState);
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
        }

        

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            marioAnimator.SetBool("onGround", onGroundState);
            alive = false;
            PlayDeathImpulse();
            marioAnimator.SetTrigger("died");
            
 
        }

    }
    public void RestartButtonCallback(int input)
    {
        
        // reset everything
        gameManager.ResetGame();
        marioAnimator.SetTrigger("gameRestart");
        marioAudio.Stop();
        alive = true;
        // resume time
        Time.timeScale = 1.0f;
    }
    //reset character
    public void Reset()
    {
        marioBody.velocity = Vector3.zero;
        marioBody.transform.position = new Vector3(0f, -2.7f, 0.0f);
        // reset sprite direction
        faceRightState = true;
        marioSprite.flipX = false;
    }

    void PlayDeathImpulse()
    {
        marioBody.AddForce(Vector2.up * deathImpulse, ForceMode2D.Impulse);
    }
    void PlayJumpSound()
    {
        // play jump sound
        marioAudio.PlayOneShot(marioAudio.clip);
    }

    void PlayDeathSound()
    {
        marioAudio.PlayOneShot(marioDeath);
    }

    void GameOverScene()
    {
        // stop time
        Time.timeScale = 0.0f;
        // set gameover scene
        gameManager.GameOver(); 
    }




}
