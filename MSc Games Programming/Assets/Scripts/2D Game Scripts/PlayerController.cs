using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D myRB;
    public float maxSpeed;
    public float acceleration;
    public float currentSpeed;
    public float verticalSpeed;
    public float jumpForce;
    public bool grounded;
    public int health;
    public int damageToTake;
    public Transform animatorGO;
    bool damageCooldown;
    public bool canTakeDamage;
    public float dmgCooldownTimer;
    float animatorGOInitial;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        animatorGOInitial = animatorGO.localScale.x;
        anim = GetComponentInChildren<Animator>();
        canTakeDamage = true;
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed = myRB.velocity.x;
        verticalSpeed = myRB.velocity.y;



        //left and right move code begins

        float move = Input.GetAxis("Horizontal"); //checking for left/right input on either keyboard or controller

        //flip Animator
        if (move > 0)
        {
            animatorGO.localScale = new Vector3 (animatorGOInitial,animatorGO.localScale.y,animatorGO.localScale.z);
        }
        else if (move < 0)
        {
            animatorGO.localScale = new Vector3(-animatorGOInitial, animatorGO.localScale.y, animatorGO.localScale.z);
        }
        //end flip Animator


        if (Mathf.Abs(currentSpeed)<maxSpeed)
        {
            myRB.AddRelativeForce(new Vector2 (move*acceleration,0)); //adding relative force to the rigidbody2D depending on the input vector
        }

        //left and right move code ends


        //animator speed code
        anim.SetFloat("speed", Mathf.Abs(currentSpeed + move));
        anim.SetFloat("vSpeed", verticalSpeed);
        //end animator speed code


        // jump code begins

        if (Input.GetKeyDown("space") && grounded == true)
        {
            myRB.AddForce(new Vector2 (0, jumpForce));
        }

        //jump code ends


    }

    //grounded check code starts

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            grounded = true;
            anim.SetBool("grounded", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            grounded = false;
            anim.SetBool("grounded", false);
        }
    }

    public IEnumerator TakeDamage ()
    {

        if (damageCooldown == false && canTakeDamage == true)
        {
            print("Ow");
            health -= damageToTake;
        }
        damageCooldown = true;
        yield return new WaitForSeconds(dmgCooldownTimer);
        damageCooldown = false;
    }

    //grounded check code ends
}
