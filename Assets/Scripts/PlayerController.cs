using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject powerupIndicator;
    public Rigidbody playerRb;
    public GameObject focalPoint;
    private float speedPl = 5.0f;
    public bool hasPowerup = false;
    private float powerupStrength = 15.0f;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveVertical = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward *  moveVertical * speedPl);
        powerupIndicator.transform.position = transform.position + new Vector3( 0, - 0.5f, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            powerupIndicator.SetActive(true);
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountDownRoutine());
        }
    }
    IEnumerator PowerupCountDownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFormPlayer = (collision.gameObject.transform.position - transform.position);
            Debug.Log("Collided with:" + collision.gameObject.name + "with powerup set to " + hasPowerup );
            enemyRigidbody.AddForce(awayFormPlayer*powerupStrength,ForceMode.Impulse);
        }
    }
}
