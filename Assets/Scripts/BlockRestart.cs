using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRestart : MonoBehaviour
{
    public GameObject powerBlocks;
    public GameObject coinBlocks;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Restart()
    {
        foreach (Transform powerBlock in powerBlocks.transform)
        {

            powerBlock.gameObject.GetComponent<QuestionScript>().Reset();
        }
        foreach (Transform coinBlock in coinBlocks.transform)
        {

            coinBlock.gameObject.GetComponent<CoinBlockScript>().Reset();
        }
    }
}
