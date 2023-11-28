using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using Random = UnityEngine.Random;
using UnityEngine;
using UnityEngine.AI; //important

public class SmallEnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public float range; //radius of sphere
    public Animator anim;
    public float walkSpeed, chaseSpeed, idleSpeed, idleTime, minIdleTime, maxIdleTime, chaseTime, minChaseTime, maxChaseTime, sightDistance;
    public bool playerInSight, playerInRange, testCo, gotShot, reset, wait, isAttacking;
    public bool isStaggered, isWalking, isIdle, isAlerted, isDead;

    public Transform centrePoint; //centre of the area the agent wants to move around in
    //instead of centrePoint you can set it as the transform of the agent if you don't care about a specific area
    GameObject playerGameObject;
    public Transform player;
    Transform currentDest;
    Vector3 dest;
    public Vector3 rayCastOffSet;

    void Start()
    {
        isAttacking = false;
        //gameObject.SetActive(false);
        chaseTime = 5f;
        agent = GetComponent<NavMeshAgent>();
        playerInSight = false;
        isWalking = true;
        testCo = true;
        //isWalking = true;
    }

    void Update()
    {

        Vector3 direction = (player.position - transform.position).normalized;
        RaycastHit hit;
        if (Physics.Raycast(transform.position + rayCastOffSet, direction, out hit, sightDistance))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                playerInSight = true;
            }
        }

        if (!playerInSight && !isDead)
        {
            Patrol();
        }
        else if (playerInSight && !isDead && !playerInRange)
            Chase();
        else if (playerInSight && !isDead && playerInRange)
        {
            Attack();
        }

    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    } 

    void Patrol()
    {
        StopCoroutine(idleRoutine());
        if (isWalking)
        {
            //StopCoroutine(idleRoutine());
            anim.ResetTrigger("idle");
            anim.ResetTrigger("attack");
            anim.SetTrigger("walk");
            agent.speed = walkSpeed;
            if (agent.remainingDistance <= agent.stoppingDistance) //done with path
            {
                int randNum = Random.Range(0, 2);
                if (randNum == 0)
                {
                    isWalking = true;

                    Vector3 point;
                    if (RandomPoint(centrePoint.position, range, out point)) //pass in our centre point and radius of area
                    {
                        Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                        agent.SetDestination(point);
                    }
                }
                else
                {
                    isWalking = false;
                    //Idle();
                }
            }
        }
        else
        {
            //Stop
            Idle();
        }
    }

    void Chase()
    {
        if (isStaggered)
        {
            agent.speed = 0;
            StartCoroutine("staggerDelay");
            // anim.ResetTrigger("walk");
            anim.ResetTrigger("idle");
            anim.ResetTrigger("attack");
            anim.ResetTrigger("walk");
            anim.SetTrigger("staggered");

        }
        else
        {

            agent.speed = chaseSpeed;
            // anim.ResetTrigger("walk");
            anim.ResetTrigger("idle");
            anim.ResetTrigger("attack");
            anim.ResetTrigger("staggered");
            anim.SetTrigger("walk");
        }

        dest = player.position;
        agent.destination = dest;
        if (testCo)
        {
            testCo = false;
            StartCoroutine("chaseRoutine");
        }

        StartCoroutine(waitAFrame());
    }

    void Idle()
    {
        agent.speed = 0;
        anim.ResetTrigger("walk");
        anim.ResetTrigger("attack");
        anim.SetTrigger("idle");
        StartCoroutine(idleRoutine());

    }

    public void Attack()
    {
        agent.speed = 0;
        anim.ResetTrigger("walk");
        anim.ResetTrigger("idle");
        anim.SetTrigger("attack");
        isAttacking = true;
        

        StartCoroutine(attackDelay());
    }

    IEnumerator idleRoutine()
    {
        yield return new WaitForSeconds(4f);
        isWalking = true;
    }

    IEnumerator chaseRoutine()
    {
        //isChasing = true;
        yield return new WaitForSeconds(chaseTime);
        chaseTime = 5;
        testCo = true;
        playerInSight = false;
    }

    IEnumerator deathRoutine()
    {
        yield return 0;
    }

    IEnumerator staggerDelay()
    {
        yield return new WaitForSeconds(0.5f);
        isStaggered = false;
    }

    IEnumerator waitAFrame()
    {
        yield return 0;
        if (agent.remainingDistance <= agent.stoppingDistance + 4)
        {
            Debug.Log(agent.remainingDistance);
            playerInRange = true;
        }
        
    }

    IEnumerator attackDelay()
    {
        yield return new WaitForSeconds(2f);
        playerInRange = false;
        isAttacking = false;
    }

}
