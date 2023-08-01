using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemBehaviour : MonoBehaviour
{
    public GameEvent itemTaken;
    private float distance;
    public Rigidbody2D rb;
    public Collider2D thisCollider2D;

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Player")
        {
            // check if the item is already in the inventory and if not add it to the inventory
            if (Player.Instance.CheckItemInInventory(this.gameObject))
            {
                Debug.Log("O item já está no inventário!");
            } else
            {
                Player.Instance.AddToInventory(this.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // remove item from inventory
            Player.Instance.RemoveFromInventory(this.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //this.transform.DOMove(collision.transform.position, 0.1f);
            distance = Vector2.Distance(transform.position, collision.transform.position);
            //Vector2 direction = collision.transform.position - transform.position;

            rb.MovePosition(collision.transform.position + new Vector3(1f, 1f, 0f) * 3f * Time.fixedDeltaTime);

            //transform.position = Vector2.MoveTowards(this.transform.position, collision.transform.position, 5f * Time.deltaTime);
        } else
        {
            // to make the player can get item stuck in a random object
            if (Input.GetKeyDown("e"))
            {
                thisCollider2D.isTrigger = true;
                StartCoroutine(DisableTrigger());
            }
        }
    }

    // Support method
    IEnumerator DisableTrigger()
    {
        yield return new WaitForSeconds(1f);
        thisCollider2D.isTrigger = false;
    }

}
