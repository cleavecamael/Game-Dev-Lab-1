using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBlockScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D coinBlockBody;
    public GameObject coinObject;
    private Rigidbody2D coinBody;
    public Vector2 coinImpulse;
    private AudioSource coinAudio;
    private bool coinHit = false;
    private bool resetBlock;
    private GameObject blockObject;

    void Start()
    {
        resetBlock = false;
        blockObject = GetComponent<Transform>().Find("Wood Block").gameObject;
        coinBlockBody = blockObject.GetComponent<Rigidbody2D>();
        coinBody = coinObject.GetComponent<Rigidbody2D>();
        coinAudio = GetComponent<AudioSource>();

    }


    // Update is called once per frame
    void Update()
    {

        if (coinBlockBody.velocity.magnitude > 0.2 && !coinHit)
        {
            blockHit();
        }

    }

    void FixedUpdate()
    {
        //remove items once finished moving

        if (resetBlock && (Vector3.Distance(coinObject.transform.localPosition, Vector3.zero) < 0.001))
        {
            coinObject.SetActive(false);
        }
    }
    void blockHit()
    {
        coinBody.AddForce(coinImpulse);
        coinAudio.PlayOneShot(coinAudio.clip);
        coinHit = true;

    }

    public void Reset()
    {
        coinHit = false;
        resetBlock = false;
        coinBlockBody.bodyType = RigidbodyType2D.Dynamic;
        coinObject.SetActive(true);
        blockObject.transform.localPosition = Vector3.zero;
        coinObject.transform.localPosition = Vector3.zero;

    }
}