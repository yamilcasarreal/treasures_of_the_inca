using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class AI : MonoBehaviour
{
    public NavMeshAgent ai;
    public List<Transform> destinations;
    public Animation aiAnim;

    public float walkSpeed, chaseSpeed, idleTime, mindIdleTime, maxIdleTime, sightDistance, catchDistance, chaseTime, minChaseTime, maxChaseTime, jumpScareTime;
    public bool isWalking, isChasing;

    public int destinationAmount;
    public Transform player;
    Transform currentDest;
    Vector3 dest;
    public Vector3 rayCastOffSet;

    int randNum, randNum2;

    public string deathScene;
 
    // Start is called before the first frame update
    void Start()
    {   
        aiAnim = GetComponent<Animation>();
        isWalking = true;
        randNum = Random.Range(0, destinationAmount);
        currentDest = destinations[randNum];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        RaycastHit hit;
        if (Physics.Raycast(transform.position + rayCastOffSet, direction, out hit, sightDistance))
        {
            if(hit.collider.gameObject.tag == "Player")
            {
                isWalking = false;
                StopCoroutine("stayIdle");
                StopCoroutine("chaseRoutine");
                StartCoroutine("chaseRoutine");
                aiAnim.Stop("Walk");
                aiAnim.Stop("Idle");
                aiAnim.Play("Run");
                isChasing = true;

            }
        }

        if (isChasing == true)
        {
            dest = player.position;
            ai.destination = dest;
            ai.speed = chaseSpeed;
            if (ai.remainingDistance <= ai.stoppingDistance)
            {
                player.gameObject.SetActive(false);
                aiAnim.Stop("Run");
                aiAnim.Play("Attack1");
                StartCoroutine(deathRoutine());
                isChasing = false;

            }

        }

        if (isWalking == true)
        {
            dest = currentDest.position;
            ai.destination = dest;
            ai.speed = walkSpeed;
            if (ai.remainingDistance <= ai.stoppingDistance)
            {
                randNum2 = Random.Range(0, 2);
                if (randNum2 == 0)
                {
                    randNum = Random.Range(0, destinationAmount);
                    currentDest = destinations[randNum];
                }
                if (randNum2 == 1)
                {
                    aiAnim.Stop("Walk");
                    aiAnim.Play("Idle");
                    StopCoroutine("stayIdle");
                    StartCoroutine("stayIdle");
                    isWalking = false;
                }
            }
        }
    }
    IEnumerator stayIdle()
    {
        idleTime = Random.Range(mindIdleTime, maxIdleTime);
        yield return new WaitForSeconds(idleTime);
        isWalking = true;
        randNum = Random.Range(0, destinationAmount);
        currentDest = destinations[randNum];
        aiAnim.Stop("Idle");
        aiAnim.Play("Walk");
    }

    IEnumerator chaseRoutine()
    {
        chaseTime = Random.Range(minChaseTime, maxChaseTime);
        yield return new WaitForSeconds(chaseTime);
        isWalking = true;
        isChasing = false;
        randNum = Random.Range(0, destinationAmount);
        currentDest = destinations[randNum];
        aiAnim.Stop("Idle");
        aiAnim.Play("Walk");

    }

    IEnumerator deathRoutine()
    {
        yield return new WaitForSeconds(jumpScareTime);
        SceneManager.LoadScene(deathScene);
    }
}
