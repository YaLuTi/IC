using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTransform : MonoBehaviour
{
    public List<GameObject> Projectiles;
    public List<Transform> ProjectileSpawnPosition;
    MonsterBasic monster;
    // Start is called before the first frame update
    void Start()
    {
        monster = GetComponent<MonsterBasic>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FireProjectile(int i)
    {
        GameObject g = Instantiate(Projectiles[i], ProjectileSpawnPosition[i].position, Quaternion.identity);
        g.transform.LookAt(monster.player.transform.position);
        Destroy(g, 4f);
    }

    public Transform GetTransform(int i)
    {
        return ProjectileSpawnPosition[i];
    }
}
