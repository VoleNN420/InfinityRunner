using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D player;

    private bool jumped;
    private bool doubleJumped;

    private float timeStamp;

    public float secondJumpMultiplayer;

    public LayerMask whatIsGround;
    public float jumpForce;
    public float liftingForce;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (IsGrounded() && Time.time >= timeStamp)
        {

            if (jumped || doubleJumped)

            {
                jumped = false;
                doubleJumped = false;
            }
            timeStamp = Time.time + 1f;
        }



        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("MousePressed");
            if (!jumped)
            {
                rb.velocity = new Vector2(0f, jumpForce);
                jumped = true;
                SoundManager.instance.PlayJumpSound();
            }
            else if (!doubleJumped)
            {

                rb.velocity = new Vector2(0f, jumpForce * secondJumpMultiplayer);
                doubleJumped = true;
                SoundManager.instance.PlayJumpSound();
            }
        }
        if (Input.GetMouseButton(0) && rb.velocity.y <= 0)
        {
            rb.AddForce(new Vector2(0f, liftingForce * Time.deltaTime));
        }

        


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Obstacle" && !GameManager.instance.immortality.isActive)
        {
            PlayerDeath();
        }

        else if (collision.CompareTag("Coin"))
        {
            //zniszczyæ coin
            Destroy(collision.gameObject);
            //dodac punkt
            GameManager.instance.CoinCollected();
            SoundManager.instance.PlayCoinGrabSound();
        }
        else if(collision.CompareTag("Immortality"))
        {
            GameManager.instance.ImmortalityCollected();
            Destroy(collision.gameObject);
        }
        else if(collision.CompareTag("Magnet"))
        {
            GameManager.instance.MagnetCollected();
            Destroy(collision.gameObject);
        }
    }



    private void PlayerDeath()
    {
        GameManager.instance.GameOver();
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(player.bounds.center, player.bounds.size, 0f, Vector2.down, 0.1f, whatIsGround);
        return hit.collider != null;
    }


}


