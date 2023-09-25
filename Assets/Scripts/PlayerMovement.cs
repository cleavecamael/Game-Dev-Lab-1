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
   
   
    public Animator marioAnimator;
    public AudioSource marioAudio;
    public AudioClip marioDeath;
    public GameManager gameManager;
    public float deathImpulse;
    private bool moving = false;
    private bool jumpedState = false;
    public GameObject GameOverPanel;
    public TextMeshProUGUI gameOverText;

    [System.NonSerialized]
    public bool alive = Variables.alive;
    int collisionLayerMask = (1 << 3) | (1 << 6) | (1 << 7);

    // Start is called before the first frame update
    void Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        Time.timeScale = 1.0f;
   

    }

    // Update is called once per frame
    void Update()
    { 
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
    }
    public float maxSpeed = 20;
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground")) onGroundState = true;
    }
    // FixedUpdate may be called once per frame. See documentation for details.
    void FixedUpdate()
    {
        float moveHorizontal = 0;

        if (Mathf.Abs(moveHorizontal) > 0)
        {
            Vector2 movement = new Vector2(moveHorizontal, 0);
            // check if it doesn't go beyond maxSpeed
            if (marioBody.velocity.magnitude < maxSpeed)
                marioBody.AddForce(movement * speed);
        }

        
        if (Variables.alive && moving)
        {
            Move(faceRightState == true ? 1 : -1);
        }



    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            
 
        }

    }



    void Move(int value)
    {
        Debug.Log("moving" + value.ToString());
        print("scale" + Time.timeScale.ToString());
        Vector2 movement = new Vector2(value, 0);
        // check if it doesn't go beyond maxSpeed
        if (marioBody.velocity.magnitude < maxSpeed)
            marioBody.AddForce(movement * speed);
    }
    public void MoveCheck(int value)
    {
        if (value == 0)
        {
            moving = false;
        }
        else
        {
            FlipMarioSprite(value);
            moving = true;
            Move(value);
        }
    }
    void FlipMarioSprite(int value)
    {
        if (value == -1 && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;
            if (marioBody.velocity.x > 0.05f)
                marioAnimator.SetTrigger("onSkid");

        }

        else if (value == 1 && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;
            if (marioBody.velocity.x < -0.05f)
                marioAnimator.SetTrigger("onSkid");
        }
    }

    public void Jump()
    {
        Debug.Log("Hello");
        if (Variables.alive && onGroundState)
        {
            // jump
            Debug.Log("yes");
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            jumpedState = true;
            // update animator state
            marioAnimator.SetBool("onGround", onGroundState);

        }
    }


    public void JumpHold()
    {
        if (Variables.alive && jumpedState)
        {
            // jump higher
            marioBody.AddForce(Vector2.up * upSpeed * 30, ForceMode2D.Force);
            jumpedState = false;

        }
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
    private void gameOver()
    {
        Time.timeScale = 0.0f;
        gameOverText.text = scoreText.text;
        GameOverPanel.SetActive(true);
    }
}
   