using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    Vector3 target;
    public float moveSpeed = 6;
    public bool stunned = false;
    Vector3[] path;
    int targetIndex;
    Animator anim;
    AnimatorStateInfo info;
    public bool isImmune = false;
    GameObject targetObject;

    public float StunTime { get; set; }

    void Start()
    {
        targetObject = GameObject.Find("Target");
        target = RandomPointInBounds(GameObject.Find("Target").gameObject.GetComponent<BoxCollider>().bounds);
        PathRequestController.RequestPath(transform.position, target, OnPathFound);
        anim = GetComponent<Animator>();
        info = anim.GetCurrentAnimatorStateInfo(0);
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccess)
    {
        if (!GetComponent<EnemyHealth>().isDead)
        {
            if (pathSuccess)
            {
                path = newPath;
                StopCoroutine("FollowPath");
                StartCoroutine("FollowPath");
            }
        }
    }

    IEnumerator FollowPath()
    {
        anim.SetBool("walking", true);
        Vector3 currentWaypoint = path[0];
        while (true)
        {
            transform.LookAt(currentWaypoint);
            while (stunned) yield return null;
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
            StartCoroutine("ImmuneTimer");
        }
        if (stunned)
        {
            StartCoroutine("RemoveStun");
        }

        if (!GetComponent<EnemyHealth>().isDead)
        {
            if (Vector3.Distance(transform.position, target) <= 2)
            {
                anim.SetBool("walking", false);
                anim.SetBool("attacking", true);
            }
        }
        else
        {
            anim.SetBool("walking", false);
            anim.SetBool("attacking", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!GetComponent<EnemyHealth>().isDead && collision.collider.tag.Contains("Base"))
        {
            anim.SetBool("walking", false);
            anim.SetBool("attacking", true);
        }
    }

    IEnumerator ImmuneTimer()
    {
        yield return new WaitForSeconds(10f);
        isImmune = false;
        yield return null;
    }

    IEnumerator RemoveStun()
    {
        yield return new WaitForSeconds(3f);
        stunned = false;
        yield return null;
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
            Random.Range(bounds.min.x - 12, bounds.max.x -12),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

}
