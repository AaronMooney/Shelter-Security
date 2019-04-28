using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Aaron Mooney
 * 
 * EnemyAI script that handles path following, status effects and enemy state
 * 
 * This script is adapted from the tutorial "A* Pathfinding Tutorial (Unity)" by Sebastian Lague
 * link: https://www.youtube.com/playlist?list=PLFt_AvWsXl0cq5Umv3pMC9SPnKjfp9eGW
 * and modified by myself, parts taken from the tutorial are marked with 
 * // ** SEBASTIAN LAGUE ** //
 *       his code here...
 *       any modifications within his code are marked with
 *       // ** AARON MOONEY ** //
 *       my code here
 *       // ** AARON MOONEY END ** //
 * // ** SEBASTIAN LAGUE END ** //
 * 
 * */
public class EnemyAI : MonoBehaviour
{
    [Header("Pathfinding")]
    public Vector3[] path;
    public GameObject targetObject;

    [Header("Enemy Stats")]
    public float moveSpeed = 6;
    public int damage = 10;
    public bool isRanged;

    [Header("Enemy Status")]
    public bool stunned = false;
    public bool isImmune = false;
    public bool isSlowed = false;
    public bool attacking = false;

    private GameObject[] targetObjects;
    private Animator anim;
    private AnimatorStateInfo info;
    private float attackDistance;
    private Vector3 target;
    private float defaultMoveSpeed = 6;
    private int targetIndex;


    void Start()
    {
        // Set target as random entrance at a random point on the door
        targetObjects = GameObject.FindGameObjectsWithTag("Base");
        targetObject = targetObjects[Random.Range(0, targetObjects.Length)];
        target = RandomPointInBounds(targetObject.GetComponent<BoxCollider>().bounds);

        if (gameObject.tag == "Aerial") target = new Vector3(target.x, 10f, target.z);
        GetPath();
        anim = GetComponent<Animator>();
        info = anim.GetCurrentAnimatorStateInfo(0);
        defaultMoveSpeed = moveSpeed;
        if (isRanged)
        {
            attackDistance = 10f;
            damage = 5;
        } else
        {
            attackDistance = 3f;
        }
    }

    // ** SEBASTIAN LAGUE ** //
    // If a path is successful then set path to waypoints
    public void OnPathFound(Vector3[] waypoints, bool pathSuccess)
    {
        // ** AARON MOONEY ** //
        if (this != null)
        {
            if (!GetComponent<EnemyHealth>().isDead)
            {
        // ** AARON MOONEY END ** //
                if (pathSuccess)
                {
                    path = waypoints;

                    // ** AARON MOONEY ** //
                    // My modification changes the y value of the path depending on whether it is a ground or aerial enemy
                    for (int i = 0; i < path.Length; i++)
                    {
                        if (gameObject.tag == "Aerial") path[i] = new Vector3(path[i].x, 10f, path[i].z);
                        else path[i] = new Vector3(path[i].x, -1, path[i].z);
                    }
                    // ** AARON MOONEY END ** //

                    StopCoroutine("FollowPath");
                    StartCoroutine("FollowPath");

                    // ** AARON MOONEY ** //
                } else
                {
                    Destroy(gameObject);
                    GameObject.Find("Spawner").GetComponent<SpawnWave>().SpawnEnemy();
                }
                // ** AARON MOONEY END ** //
            }
        }
    }

    private void GetPath()
    {
        PathRequestController.RequestPath(transform.position, target, OnPathFound);
    }

    IEnumerator FollowPath()
    {
        // ** AARON MOONEY ** //
        anim.SetBool("walking", true);
        // ** AARON MOONEY END ** //
        Vector3 currentWaypoint = path[0];
        while (true)
        {
            // ** AARON MOONEY ** //
            // look in direction and pause path following while stunned or attacking and break when dead
            transform.LookAt(currentWaypoint);
            while (stunned) yield return null;
            while (attacking) yield return null;
            if (GetComponent<EnemyHealth>().isDead) break;
            // ** AARON MOONEY END ** //
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }
            // ** AARON MOONEY ** //
            transform.LookAt(currentWaypoint);
            // ** AARON MOONEY END ** //
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    // Helper method that draws enemy paths in the scene
    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);
                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
    // ** SEBASTIAN LAGUE END ** //

    // Update enemy path - unused due to bug where some enemies freeze
    public void UpdatePath()
    {
        GetPath();
    }

    private void Update()
    {
        // check for immunity and negative status effects and remove them
        if (isImmune)
        {
            Invoke("RemoveImmunity", 10f);
        }
        if (stunned)
        {
            Invoke("RemoveStun", 3f);
        }
        if (isSlowed)
        {
            Invoke("RemoveSlow", 5f);
        }

        // Attack when enemy is in range of target
        if (!GetComponent<EnemyHealth>().isDead)
        {
            if (Vector3.Distance(transform.position, target) <= attackDistance)
            {
                attacking = true;
                anim.SetBool("walking", false);
                anim.SetBool("attacking", true);

                if (gameObject.tag == "Aerial") GetComponent<DroneAttack>().Laser();
            }
        }
        else
        {
            anim.SetBool("walking", false);
            anim.SetBool("attacking", false);
            attacking = false;
        }
    }

    // Attack method called from enemy animation
    private void Attack()
    {
        Debug.Log("Attack");
        if (targetObject != null)
        {
            targetObject.GetComponent<Health>().TakeDamage(damage);
        }
    }

    // Check for collisions and on collision with base start attacking
    private void OnCollisionEnter(Collision collision)
    {
        if (!GetComponent<EnemyHealth>().isDead && collision.collider.tag == "Base")
        {
            attacking = true;
            anim.SetBool("walking", false);
            anim.SetBool("attacking", true);
        }
    }

    // remove status effects
    void RemoveImmunity()
    {
        isImmune = false;
    }

    void RemoveStun()
    {
        stunned = false;
    }

    void RemoveSlow()
    {
        moveSpeed = defaultMoveSpeed;
        isSlowed = false;
    }

    // Choose a random point in collider at y = 0
    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            bounds.min.x - 12,
            0f,
            Random.Range(bounds.min.z + 3, bounds.max.z - 3)
        );
    }

}
