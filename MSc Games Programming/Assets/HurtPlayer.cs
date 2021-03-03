using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    public float pushbackForce;
    public int damageToDeal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.GetComponent<PlayerController>().canTakeDamage == true)
        {
            print("player says ow");
            collision.GetComponent<PlayerController>().damageToTake = damageToDeal;
            StartCoroutine(collision.GetComponent<PlayerController>().TakeDamage());
            collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(collision.transform.position.x-gameObject.transform.position.x , collision.transform.position.y - gameObject.transform.position.y) * pushbackForce, ForceMode2D.Impulse);
        }
    }
}
