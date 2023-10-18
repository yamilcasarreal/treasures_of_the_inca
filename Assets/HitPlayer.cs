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

    private bool waiting = false;

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, attackDistance))
            if (hit.collider.gameObject.tag == "Player" && !waiting && !this.GetComponent<SmallEnemyAI>().isDead)
                StartCoroutine("Timer", attackSpeed);
    }

    // Shows text for a certain number of time
    IEnumerator Timer(float attackSpeed)
    {
        waiting = true;
        //this.GetComponent<SmallEnemyAI>().playerInRange = true;
        playerGameObject.GetComponent<PlayerHP>().changePlayerHP(-attackDamage);
        yield return new WaitForSeconds(attackSpeed);
        waiting = false;
        //this.GetComponent<SmallEnemyAI>().playerInRange = false;
    }
}
