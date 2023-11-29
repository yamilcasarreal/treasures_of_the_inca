using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmallEnemyHP : MonoBehaviour
{
    public int currentHP = 100;
    public float deathTimer;
    public Animator anim;
    public NavMeshAgent agent;
    public GameObject player;

    public void changeSmallEnemyHP(int changeInHP)
    {
        currentHP += changeInHP;

        if (currentHP <= 0)
        {
            this.GetComponent<SmallEnemyAI>().isDead = true;
            agent.speed = 0;
            anim.ResetTrigger("idle");
            anim.ResetTrigger("walk");
            anim.ResetTrigger("staggered");
            anim.ResetTrigger("attack");
            anim.SetTrigger("dead");

            player.GetComponent<Consumables>().SpawnAmmo(this.gameObject.transform.position);
            StartCoroutine("Timer", deathTimer);
        }
    }

    IEnumerator Timer(float deathTimer)
    {
        yield return new WaitForSeconds(deathTimer);
        Destroy(this.gameObject);
    }
}
