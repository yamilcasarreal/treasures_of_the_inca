using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour
{
    public Transform player;
    public GameObject playerGameObject;

    public int attackDamage;
    public float attackSpeed;
    public float attackDistance;

    public bool waiting = false;

    // Update is called once per frame
    void Update()
    {
        /*Vector3 direction = (player.position - transform.position).normalized;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, attackDistance))
            if (hit.collider.gameObject.tag == "Player" && !waiting && !this.GetComponent<SmallEnemyAI>().isDead)
                StartCoroutine("Timer", attackSpeed);*/
        
    }
    
    void OnCollisionEnter(Collision col)
    {
        if (!waiting)
        {
            if (col.gameObject.tag == "Player")
            {
                Debug.Log("HIthithit");
                StartCoroutine("Timer", attackSpeed);
            }
        }
    }

    // Shows text for a certain number of time
    IEnumerator Timer(float attackSpeed)
    {
        waiting = true;
        yield return new WaitForSeconds(.1f);
        //this.GetComponent<SmallEnemyAI>().playerInRange = true;
        //if (GetComponent<SmallEnemyAI>().isAttacking && GetComponent<SmallEnemyAI>().playerInRange)
        //{
        playerGameObject.GetComponent<PlayerHP>().changePlayerHP(-attackDamage);
        //}
        
        waiting = false;
        //this.GetComponent<SmallEnemyAI>().playerInRange = false;
    }
}
