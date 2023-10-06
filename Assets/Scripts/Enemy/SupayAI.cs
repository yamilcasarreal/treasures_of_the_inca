using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI; //important

//if you use this code you are contractually obligated to like the YT video
public class SupayAI : MonoBehaviour //don't forget to change the script name if you haven't
{

    public NavMeshAgent agent;
    public float range; //radius of sphere
    public Animator anim;
    public float walkSpeed, chaseSpeed, idleSpeed, idleTime, minIdleTime, maxIdleTime, chaseTime, minChaseTime, maxChaseTime, sightDistance, jumpScareTime;
    public bool isChasing, isWalking, isIdle, isAlerted, playerCaptured, playerThrow;
    //public int randIdleTime;

    public Transform centrePoint; //centre of the area the agent wants to move around in
    //instead of centrePoint you can set it as the transform of the agent if you don't care about a specific area

    public Transform player;
    Transform currenDest;
    Vector3 dest;
    public Vector3 rayCastOffSet;

    void Start()
    {
        playerCaptured = false;
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
                //isWalking = false;
                StopCoroutine(stayIdle());
                StopCoroutine(chaseRoutine());
                StartCoroutine(chaseRoutine());
                //anim.ResetTrigger("walk");
                //anim.ResetTrigger("idle");
                //anim.SetTrigger("sprint");

                isChasing = true;
            }
        }
        if (isWalking == true)
        {
            agent.speed = walkSpeed;
            anim.ResetTrigger("sprint");
            anim.ResetTrigger("idle");
            anim.SetTrigger("walk");
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
            isWalking = false;
            dest = player.position;
            agent.destination = dest;
            agent.speed = chaseSpeed;
            anim.ResetTrigger("walk");
            anim.ResetTrigger("idle");
            anim.SetTrigger("sprint");
            if (agent.remainingDistance <= agent.stoppingDistance +2)
            {
       
                playerCaptured = true;

            }

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
            //
        }
        if (playerThrow == true)
        {
            anim.ResetTrigger("jumpScare");
            anim.SetTrigger("throw");
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

    IEnumerator stayIdle()
    {
        idleTime = Random.Range(minIdleTime, maxIdleTime);
        yield return new WaitForSeconds(idleTime);
        isIdle = false;
        isWalking = true;
    }
  
    IEnumerator chaseRoutine()
    {
        chaseTime = Random.Range(minChaseTime, maxChaseTime);
        yield return new WaitForSeconds(chaseTime);
        isChasing = false;
        isWalking = true;
    }

    IEnumerator deathRoutine()
    {
        //player.gameObject.SetActive(false);
        yield return new WaitForSeconds(jumpScareTime);
        playerThrow = true;
        //playerCaptured = false;
        //anim.ResetTrigger("jumpScare");
        //anim.SetTrigger("throw");


    }


}