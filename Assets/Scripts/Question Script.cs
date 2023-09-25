using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestionScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D questionBlockBody;
    public GameObject coinObject;
    private Rigidbody2D coinBody;
    public Vector2 coinImpulse;
    private AudioSource questionAudio;
    private bool coinHit = false;
    private bool resetBlock;
    private SpriteRenderer questionSprite;
    public Sprite usedBlock;
    private GameObject blockObject;
    private Animator questionAnimator;
    void Start()
    {
        resetBlock = false;
        blockObject = GetComponent<Transform>().Find("Power Block").gameObject;
        questionBlockBody = blockObject.GetComponent<Rigidbody2D>();
        coinBody = coinObject.GetComponent<Rigidbody2D>();
        questionAudio = GetComponent<AudioSource>();
        questionSprite = blockObject.GetComponent<SpriteRenderer>();
        questionAnimator = blockObject.GetComponent<Animator>();
      
    }

    public void Reset()
    {
 
        coinHit = false;
        resetBlock = false;
        questionBlockBody.bodyType = RigidbodyType2D.Dynamic;
        
        questionAnimator.enabled = true;
        questionAnimator.SetTrigger("reset");
        coinObject.SetActive(true);
        blockObject.transform.localPosition = Vector3.zero;
        coinObject.transform.localPosition = Vector3.zero;
    }



    // Update is called once per frame
    void Update()
    {
        
        if (questionBlockBody.velocity.magnitude > 0.2 && !coinHit)
        {
            blockHit();
        }
        
    }

    void FixedUpdate()
    {
        //remove items once finished moving
        if (coinHit && (Vector3.Distance(blockObject.transform.localPosition, Vector3.zero) < 0.001))
        {
            questionBlockBody.bodyType = RigidbodyType2D.Static;
            resetBlock = true;
        }
        if (resetBlock && (Vector3.Distance(coinObject.transform.localPosition, Vector3.zero) < 0.001))
        {
            coinObject.SetActive(false);
        }
    }
    void blockHit()
    {
        coinBody.AddForce(coinImpulse);
        questionAudio.PlayOneShot(questionAudio.clip);
        questionAnimator.enabled = false;
        questionSprite.sprite = usedBlock;
        coinHit = true;
        
    }
}
