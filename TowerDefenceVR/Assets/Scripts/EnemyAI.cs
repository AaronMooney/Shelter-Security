using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    Vector3 target;
    private float defaultMoveSpeed = 6;
    public float moveSpeed = 6;
    public bool stunned = false;
    public float turnDist = 5;
    public float turnSpeed = 3;
    Vector3[] path;
    int targetIndex;
    Animator anim;
    AnimatorStateInfo info;
    public bool isImmune = false;
    public bool isSlowed = false;
    public bool attacking = false;
    GameObject[] targetObjects;
    public GameObject targetObject;
    public int damage = 10;
    public bool isRanged;
    private float attackDistance;


    void Start()
    {
        targetObjects = GameObject.FindGameObjectsWithTag("Base");
        targetObject = targetObjects[Random.Range(0, targetObjects.Length)];
        target = RandomPointInBounds(targetObject.GetComponent<BoxCollider>().bounds);
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

    public void OnPathFound(Vector3[] waypoints, bool pathSuccess)
    {
        if (this != null)
        {
            if (!GetComponent<EnemyHealth>().isDead)
            {
                if (pathSuccess)
                {
                    path = waypoints;

                    for (int i = 0; i < path.Length; i++)
                    {
                        path[i] = new Vector3(path[i].x, -1, path[i].z);
                    }

                    StopCoroutine("FollowPath");
                    StartCoroutine("FollowPath");
                } else
                {
                    Destroy(gameObject);
                    GameObject.Find("Spawner").GetComponent<SpawnWave>().SpawnEnemy();
                }
            }
        }
    }

    private void GetPath()
    {
        PathRequestController.RequestPath(transform.position, target, OnPathFound);
    }

    IEnumerator FollowPath()
    {
        anim.SetBool("walking", true);
        Vector3 currentWaypoint = path[0];
        while (true)
        {
            transform.LookAt(currentWaypoint);
            while (stunned) yield return null;
            while (attacking) yield return null;
            if (GetComponent<EnemyHealth>().isDead) break;
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }
            transform.LookAt(currentWaypoint);
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    void Update()
    {
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


        if (!GetComponent<EnemyHealth>().isDead)
        {
            if (Vector3.Distance(transform.position, target) <= attackDistance)
            {
                attacking = true;
                Debug.Log("hit base");
                anim.SetBool("walking", false);
                anim.SetBool("attacking", true);
            }
        }
        else
        {
            anim.SetBool("walking", false);
            anim.SetBool("attacking", false);
            attacking = false;
        }
    }

    private void Attack()
    {
        Debug.Log("Attack");
        if (targetObject != null)
        {
            targetObject.GetComponent<Health>().TakeDamage(damage);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!GetComponent<EnemyHealth>().isDead && collision.collider.tag == "Base")
        {
            attacking = true;
            Debug.Log("hit base");
            anim.SetBool("walking", false);
            anim.SetBool("attacking", true);
        }
    }

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

    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            bounds.min.x - 12,
            0f,
            Random.Range(bounds.min.z + 3, bounds.max.z - 3)
        );
    }

}
