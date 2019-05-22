using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class AINav : MonoBehaviour
{
    public Transform target;
    private NavMeshPath path;

    public float PlayerMomentum;
    float lastDistance;
    private float elapsed = 0.0f;
    Color[] colors = { Color.yellow, Color.red };
    void Start()
    {
        path = new NavMeshPath();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        elapsed = 0.0f;
        lastDistance = 0;
        PlayerMomentum = 0;
        NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path);
    }

    void Update()
    {
        // Update the way to the goal every second.
        if (target != null)
        {
            NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path);
        }
        
        for (int i = 0; i < path.corners.Length - 1; i++)
            Debug.DrawLine(path.corners[i], path.corners[i + 1], colors[i % 2]);
    }

    public Vector3 GetCorners()
    {
        if (target == null) return transform.position;
        if(path.corners.Length >= 1)
        return path.corners[1];
        else if(path.corners.Length == 0)
        {
            return path.corners[0];
        }
        else
        {
            return this.transform.position;
        }
    }

    public void CaculatePlayerMomentum()
    {
        float d = Vector3.Distance(transform.position, target.transform.position);
        if (lastDistance == 0)
        {
            lastDistance = d;
        }
        else
        {
            StartCoroutine(AddMomentum(lastDistance - d));
            lastDistance = d;
        }
    }

    IEnumerator AddMomentum(float d)
    {
        PlayerMomentum += d;
        yield return new WaitForSecondsRealtime(0.2f);
        PlayerMomentum -= d;
        yield return 0;
    }
}