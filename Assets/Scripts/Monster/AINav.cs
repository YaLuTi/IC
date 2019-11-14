using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class AINav : MonoBehaviour
{
    public Vector3 target = Vector3.zero;
    public bool HighRate = false;
    Vector3 LastTarget = Vector3.zero;
    bool y = false;

    private NavMeshPath path;

    public float PlayerMomentum;
    float lastDistance;
    private float elapsed = 0.0f;
    Color[] colors = { Color.yellow, Color.red };
    void Start()
    {
        path = new NavMeshPath();
        // target = GameObject.FindGameObjectWithTag("Player").transform;
        elapsed = 0.0f;
        lastDistance = 0;
        PlayerMomentum = 0;
    }

    void Update()
    {
        // Update the way to the goal every second.
        if (target == Vector3.zero) return;
        if (target != null && LastTarget != target && !HighRate)
        {
            NavMesh.CalculatePath(transform.position, target, NavMesh.AllAreas, path);
            NavMeshHit hit;
            for (int i = 1; i < path.corners.Length - 2; i++)
            {
                bool result = NavMesh.FindClosestEdge(path.corners[i], out hit, NavMesh.AllAreas);
                if (result && hit.distance < 1.5f)
                    path.corners[i] = hit.position + hit.normal * 1.5f;
            }
            LastTarget = target;
            Debug.Log(path.corners.Length);
        }
        
        for (int i = 0; i < path.corners.Length - 1; i++)
            Debug.DrawLine(path.corners[i], path.corners[i + 1], colors[i % 2]);
    }

    public Vector3 GetCorners()
    {
        if (target == null || target == Vector3.zero) return transform.position;
        if (path.corners.Length >= 1)
        {
            return path.corners[1];
        }
        else if (path.corners.Length == 0)
        {
            return target;
        }
        else
        {
            return this.transform.position;
        }
    }

    public void CaculatePlayerMomentum()
    {
        float d = Vector3.Distance(transform.position, target);
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