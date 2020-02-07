using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speedEnemy;
    private Rigidbody enemyRb;
    private GameObject playerRb;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        playerRb = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (playerRb.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection* speedEnemy);
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
