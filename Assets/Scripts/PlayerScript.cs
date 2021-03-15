using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    Animator anim; 
    
    public AudioSource musicSource;

    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    private Rigidbody2D rd2d;

    public float speed;

    public Text score;

    public Text livesText;

    public Text winText;

    private int scoreValue = 0;

    private int life = 3; 

    public float jumpForce;

    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();  
        musicSource.clip = musicClipOne;
        musicSource.Play();
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        winText.text = "Use WASD";
        life = 3;
        livesText.text = "Lives: " + life.ToString(); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        
        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }

        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }

          if (Input.GetKeyUp(KeyCode.W))
        {

          anim.SetInteger("State", 0);

         }
        if (Input.GetKeyDown(KeyCode.D))
        {

          anim.SetInteger("State", 1);

         }
         if(Input.GetKeyUp(KeyCode.D))
         {
             anim.SetInteger("State", 0);
         }
        if (Input.GetKeyDown(KeyCode.A))
        {
         anim.SetInteger("State", 1);
        }
         if (Input.GetKeyUp(KeyCode.A))
        {
         anim.SetInteger("State", 0);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            if (scoreValue == 4)
            {
               transform.position = new Vector2(20,15);
               life = 3;
               livesText.text = "Lives: " + life.ToString();
            }
            else if (scoreValue >= 8)
            {
                musicSource.Stop();
                musicSource.clip = musicClipTwo;
                musicSource.loop = false;
                musicSource.Play();
                winText.text = "You win! Game by Emma Murphy!";
            }
        }

        else if (collision.gameObject.CompareTag("Enemy"))
        {
             Destroy(collision.collider.gameObject);
             life = life - 1; 
              livesText.text = "Lives: " + life.ToString();
              if(life <= 0)
                {
                winText.text = "You lost.";
                Destroy(this);
                }

        }
    }
    
    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   } 

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }
    }
}