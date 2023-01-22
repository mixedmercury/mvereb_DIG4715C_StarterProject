using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    AudioSource audioSource;
    public GameObject player;
    public AudioClip tireSound;
    private Vector2 direction; 
    private Vector3 playerPosition; 

    public int force;
    public int delay; //time between spawn and launch

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(MoveCar());
    }

    IEnumerator MoveCar()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position; //find direction of player
        Vector2 direction = playerPosition - transform.position;
        direction = direction.normalized;

        var offset = 180f;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //rotate towards player
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));

        yield return new WaitForSeconds (delay); //Wait to move
        audioSource.PlayOneShot(tireSound);
        
        rigidbody2d.AddForce(direction * force); //Launch object
        Object.Destroy(gameObject,2.0f);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<Collider2D>().GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.enemyHit();
        }
    }
}