using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRestart : MonoBehaviour
{
    public GameObject mario;
    private Rigidbody2D marioBody;
    public Camera gameCamera;
    private SpriteRenderer marioSprite;
    public Animator marioAnimator;

    void Start()
    {
        marioBody = mario.GetComponent<Rigidbody2D>();
        marioSprite = mario.GetComponent<SpriteRenderer>();
    }

    public void Restart()
    {
        marioAnimator.SetTrigger("gameRestart");
        gameCamera.transform.position = new Vector3(0, 0, -10);
        marioBody.velocity = Vector3.zero;
        marioBody.transform.position = new Vector3(0f, -2.7f, 0.0f);
        // reset sprite direction
        marioSprite.flipX = false;
        Variables.alive = true;
        Time.timeScale = 1.0f;
      
    }
}
