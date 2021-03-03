using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{
    public bool pickedUp;

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
        if (collision.tag == "Player" && pickedUp == false)
        {
            pickedUp = true;
            GameManager.currentPickupCount = GameManager.currentPickupCount - 1 ;
            GameManager.score = GameManager.score + GameManager.pickupValue;
            Destroy(gameObject);
        }

    }
}
