using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTransform : MonoBehaviour
{
    public List<Transform> ProjectileSpawnPosition;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform GetTransform(int i)
    {
        return ProjectileSpawnPosition[i];
    }
}
