using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI; //important

public class SupayAI : MonoBehaviour 
{

    public NavMeshAgent agent;
    public float range; //radius of sphere
    public Animator anim;
    public float walkSpeed, chaseSpeed, idleSpeed, idleTime, minIdleTime, maxIdleTime, chaseTime, minChaseTime, maxChaseTime, sightDistance, jumpScareTime;
    public bool shouldMove;
    public bool isStaggered, isChasing, isWalking, isIdle, isAlerted, playerCaptured, playerThrow;
    //public int randIdleTime;

    public Transform centrePoint; //centre of the area the agent wants to move around in
    //instead of centrePoint you can set it as the transform of the agent if you don't care about a specific area

    public Transform player;
    Transform currentDest;
    Vector3 dest;
    public Vector3 rayCastOffSet;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        isWalking = true;
    } 


    void Update()
    {
       
        Vector3 direction = (player.position - transform.position).normalized;
        RaycastHit hit;
        if (Physics.Raycast(transform.position + rayCastOffSet, direction, out hit, sightDistance))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                isChasing = true;
            }
        }
        if (isWalking == true || !isChasing)
        {
            agent.speed = walkSpeed;
            anim.ResetTrigger("sprint");
            anim.ResetTrigger("idle");
            anim.SetTrigger("walk");
            StopCoroutine(stayIdle());
            StopCoroutine(chaseRoutine());
            if (agent.remainingDistance <= agent.stoppingDistance) //done with path
            {
                int randNum = Random.Range(0, 2);
                if (randNum == 0)
                {
                    Vector3 point;
                    if (RandomPoint(centrePoint.position, range, out point)) //pass in our centre point and radius of area
                    {
                        Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                        agent.SetDestination(point);
                    }
                }
                else
                {
                    isIdle = true;
                }
            }
        }

        if (isIdle == true)
        {

            anim.ResetTrigger("walk");
            anim.ResetTrigger("sprint");
            anim.SetTrigger("idle");
            agent.speed = idleSpeed;
            StopCoroutine(stayIdle());
            StartCoroutine(stayIdle());
            isWalking = false;
        }

        if (isChasing == true)
        {
            isIdle = false;
            isWalking = false;
            StopCoroutine(stayIdle());
            StopCoroutine(chaseRoutine());
            StartCoroutine(chaseRoutine());
            dest = player.position;
            agent.destination = dest;
            agent.speed = chaseSpeed;
            anim.ResetTrigger("walk");
            anim.ResetTrigger("idle");
            anim.ResetTrigger("staggered");
            anim.SetTrigger("sprint");
            StartCoroutine(waitAFrame());
            

        }
            /*if (playerThrow == true)
            {
                anim.ResetTrigger("jumpScare");
                anim.SetTrigger("throw");
            }*/
        if (isStaggered == true)
        {
            agent.speed = 0;
            anim.ResetTrigger("walk");
            anim.ResetTrigger("idle");
            anim.SetTrigger("staggered");
            StartCoroutine(staggerDelay());
        }
        if (playerCaptured == true)
        {
                anim.ResetTrigger("walk");
                anim.ResetTrigger("idle");
                anim.ResetTrigger("sprint");
                anim.SetTrigger("jumpScare");
                StartCoroutine(deathRoutine());
                isChasing = false;
                isWalking = false;
                isIdle = false;
                agent.speed = 0;
                //player.gameObject.SetActive(false);

            //
        }

    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
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

    IEnumerator alertDelay()
    {
        yield return new WaitForSeconds(2);
        //shouldMove = true;
    }

    IEnumerator stayIdle()
    {
        idleTime = Random.Range(minIdleTime, maxIdleTime);
        yield return new WaitForSeconds(idleTime);
        isIdle = false;
        isWalking = true;
    }
  
    IEnumerator chaseRoutine()
    {
        chaseTime = 10f;
        isChasing = true;
        //Debug.Log(Time.time);
        yield return new WaitForSeconds(chaseTime);
        //Debug.Log(Time.time);
        StopCoroutine(chaseRoutine());
        isChasing = false;
        //isWalking = true;
    }

    IEnumerator deathRoutine()
    {
        yield return new WaitForSeconds(jumpScareTime);
        //playerThrow = true;
    }
    IEnumerator staggerDelay()
    {
        yield return new WaitForSeconds(0.5f);
        isStaggered = false;
        //shouldMove = true;
        isChasing = true;
    }
    IEnumerator waitAFrame()
    {
        yield return 0;
        if (agent.remainingDistance <= agent.stoppingDistance + 2)
        {
            Debug.Log(agent.remainingDistance);
            playerCaptured = true;
        }
    }


}