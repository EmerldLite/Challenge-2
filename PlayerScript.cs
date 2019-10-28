using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public Text score;

    public Text winText;

    private int scoreValue = 0;

    public Text lives;

    private int livesValue;

    Animator anim;

    public AudioSource musicSource;

    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;


    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        winText.text = "";
        livesValue = 3;
        anim = GetComponent<Animator>();
        SetLivesText();
        SetScore();


    }

    // Update is called once per frame
    void Update()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }


        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    void SetLivesText()
    {
        lives.text = "Lives: " + livesValue.ToString();
        if (livesValue <= 0)
        {
            Destroy(this);
            winText.text = "You Lose! Try Again!";
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            SetScore();
        }

        if (collision.collider.tag == "Enemy")
        {
            Destroy(collision.collider.gameObject);
            livesValue = livesValue - 1;
            lives.text = livesValue.ToString();
            SetLivesText();
        }
    }

    void SetScore()
    {
        if (scoreValue >= 8)
        {
            winText.text = "You win! Game created by Gavin McAllister!";
            musicSource.Stop();
            musicSource.clip = musicClipTwo;
            musicSource.Play();
        }

        if (scoreValue == 4)
        {
            transform.position = new Vector2(60f, 0.3f);
            livesValue = 3;
            lives.text = livesValue.ToString();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }
}
