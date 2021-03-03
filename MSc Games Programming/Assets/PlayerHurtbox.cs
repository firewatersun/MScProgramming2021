using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtbox : MonoBehaviour
{
    public int damageToDeal;
    public float pushbackForce;
    PlayerController pc;
    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && pc.grounded == false)
        {
            pc.canTakeDamage = false;
            print("Have hurt the enemy");
            collision.GetComponent<PatrollingEnemy>().damageToTake = damageToDeal;
            StartCoroutine(collision.GetComponent<PatrollingEnemy>().TakeDamage());
            collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(collision.transform.position.x - gameObject.transform.position.x, collision.transform.position.y - gameObject.transform.position.y) * pushbackForce, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && pc.canTakeDamage == false)
        {
            pc.canTakeDamage = true;
        }
    }
}
