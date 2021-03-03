using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemy : MonoBehaviour
{
    public float moveSpeed;
    public int FSM;
    public float detectionRadius;
    public float alertTime;
    public float cooldownTime;
    public float attackSpeedMultiplier;
    public bool canTakeDamage;
    public bool attacking;
    public float health;
    public int damageToTake;
    public float dmgCooldownTimer;
    public float chargingTime;
    float playerDetectedTime;
    bool charging;
    bool damageCooldown;
    bool waitingCoroutine;
    public Transform RaycastStartPos;
    public LayerMask maskToHit;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        FSM = 0;
    }

    // Update is called once per frame
    void Update()
    {

        //detecting the player
        RaycastHit2D rayHit = Physics2D.CircleCast(transform.position, 1f, new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y), detectionRadius, maskToHit);

        if (rayHit.collider != null && rayHit.collider.tag == "Player" && attacking == false)
        {
            Debug.DrawLine(RaycastStartPos.position, player.transform.position, Color.red);
            print(rayHit.collider.name);
            print("I SEE YOU");
            FSM = 1;
        }
        else if (rayHit.collider == null || rayHit.collider.tag != "Player" && attacking == false)
        {
            FSM = 0;
        }

        //destroy self if damaged to 0
        if (health <=0)
        {
            FSM = 4;
        }


        //switch statement controls the finite state machine (what each state does)
        switch (FSM)
        {
            case 4://destroy self when dead
                attacking = false;
                print("ded");
                GameObject.Destroy(gameObject);
                break;
            case 3: //cools down after charging
                attacking = true;
                canTakeDamage = true;
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0 * moveSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
                if (waitingCoroutine == false)
                {
                    StartCoroutine(ChargeCooldown());
                }
                ;
                break;
            case 2: //charge at high speed
                attacking = true;
                canTakeDamage = true;
                Vector3 playerRelativePos = transform.InverseTransformPoint(player.transform.position);
                if (playerRelativePos.x > 0)
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(attackSpeedMultiplier * Mathf.Abs(moveSpeed), gameObject.GetComponent<Rigidbody2D>().velocity.y);
                }
                else if (playerRelativePos.x <= 0)
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-attackSpeedMultiplier * Mathf.Abs(moveSpeed), gameObject.GetComponent<Rigidbody2D>().velocity.y);
                }
                
                if (waitingCoroutine == false)
                {
                    StartCoroutine(Charging());
                }
                break;
            case 1: //pauses the enemy, waits until the alertTime is finished, then transitions to charging state
                print ("I AM IN ALERT STATE");
                attacking = true;
                canTakeDamage = true;
                if (waitingCoroutine == false)
                {
                    StartCoroutine(Alerted());
                }
                break;
            default: //default Patrolling state
                attacking = false;
                canTakeDamage = false;
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
                break;
        }

        //turn back if hit wall
        if (gameObject.GetComponent<Rigidbody2D>().velocity.x <= 0.1 && gameObject.GetComponent<Rigidbody2D>().velocity.x >= -0.1)
        {
            moveSpeed = moveSpeed * -1;
        }
    }




    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "ground")
        {
            moveSpeed = moveSpeed * -1;
        }
        
    }


    public IEnumerator Alerted()
    {
        print("I am alerted");
        waitingCoroutine = true;
        yield return new WaitForSeconds(alertTime);
        FSM = 2;
        waitingCoroutine = false;

    }

    public IEnumerator Charging()
    {
        print("Counting down charge time");
        waitingCoroutine = true;
        yield return new WaitForSeconds(chargingTime);
        FSM = 3;
        waitingCoroutine = false;
        
    }

    public IEnumerator ChargeCooldown()
    {
        print("cooldown");
        waitingCoroutine = true;
        yield return new WaitForSeconds(cooldownTime);
        FSM = 0;
        waitingCoroutine = false;
    }
    public IEnumerator TakeDamage()
    {
        if (damageCooldown == false && canTakeDamage == true)
        {
            print("enemysaysOw");
            health -= damageToTake;
        }
        damageCooldown = true;
        yield return new WaitForSeconds(dmgCooldownTimer);
        damageCooldown = false;
    }
}
