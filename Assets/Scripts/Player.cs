using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator anim;
    public bool face;
    public float speed = 2.0f;
    public float force = 11.75f;
    Rigidbody2D body;
    LevelManager manager;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        manager = GameObject.FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //float speedHorizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float speedHorizontal = Input.GetAxis("Horizontal") * speed;

        transform.Translate(speedHorizontal * Time.deltaTime, 0, 0);
        //Vector2 vel = new Vector2(speedHorizontal, 0);
        anim.SetFloat("SpeedX", Mathf.Abs(speedHorizontal));
        if(Input.GetAxis("Horizontal") < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

    }

    private void FixedUpdate()
    {
        /* if (body)
         {
             body.AddForce(new Vector2(Input.GetAxis("Horizontal") * force, 0));
         }*/
    }

    void Jump()
    {
        if (body && Mathf.Abs(body.velocity.y) < 0.05f)
        {
            body.AddForce(new Vector2(0, force / 2), ForceMode2D.Impulse);
            GameObject.FindObjectOfType<AudioManager>().PlayJump();
            //GameObject.FindObjectsOfType<AudioManager>().PlayJump();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && manager.Score < 1)
        {
            FindObjectOfType<AudioManager>().PlayDeath();

            Destroy(gameObject);
        }
        else if(collision.gameObject.CompareTag("Enemy") && manager.Score == 1)
        {
            FindObjectOfType<AudioManager>().PlayKill();
            Destroy(collision.gameObject);

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            GameObject.FindObjectOfType<AudioManager>().PlayCoin();

            Destroy(other.gameObject);
        }

        else if (other.gameObject.CompareTag("PowerUp"))
        {
            GameObject.FindObjectOfType<AudioManager>().PlayPowerUp();
            anim.SetBool("Power", true);
            Destroy(other.gameObject);
            manager.Score++;

        }


    }
}
