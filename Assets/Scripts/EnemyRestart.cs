using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRestart : MonoBehaviour
{
    public GameObject enemies;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Restart()
    {
        foreach (Transform enemy in enemies.transform)
        {

            enemy.gameObject.GetComponent<EnemyMovement>().Reset();
        }
    }
}
