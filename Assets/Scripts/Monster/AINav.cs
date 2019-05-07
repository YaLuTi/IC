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
    }

    void Update()
    {
        // Update the way to the goal every second.
        NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path);
        
        for (int i = 0; i < path.corners.Length - 1; i++)
            Debug.DrawLine(path.corners[i], path.corners[i + 1], colors[i % 2]);
    }

    public Vector3 GetCorners()
    {
        return path.corners[1];
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