using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour {

    MonsterBasic monster;
    public float Radius;

    [Range(0, 360)]
    public float Angle;

    public LayerMask TargetMask;
    public LayerMask ObjMask;

    public List<GameObject> ViewTargets = new List<GameObject>();

    int TestCount = 5;
    /* private void Start()
     {
         StartCoroutine(View(.2f));
     }

     IEnumerator View(float delay)
     {
         while (true)
         {
             yield return new WaitForSeconds(delay);
             GetTarget();
         }
     }*/
     

    private void Start()
    {
        monster = GetComponent<MonsterBasic>();
    }

    private void Update()
    {
        if (monster.PlayerDistance < Radius)
        {
            GetTarget();
        }
    }

    public void GetTarget()
    {
        if (TestCount >= 5)
        {
            ViewTargets.Clear();
            if (monster.PlayerDistance < Radius)
            {
                Transform target = monster.player.transform;
                Vector3 dir = (target.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, dir) < Angle / 2)
                {
                    float Distance = Vector3.Distance(transform.position, target.transform.position);

                    RaycastHit raycastHitl;
                    if (!Physics.Raycast(transform.position, dir, out raycastHitl, Distance, ObjMask))
                    {
                        monster.e_FoundPlayer();
                    }
                    else
                    {
                    }
                }
            }
            
        }
        else if(TestCount < 5)
        {
            TestCount++;
        }
    }

    public bool GetIsViewTarget()
    {

        return true;
    }
    public Vector3 DirFromAngle(float angleDegress, bool GlobalAngle)
    {
        if (!GlobalAngle)
        {
            angleDegress += transform.eulerAngles.y;
        }
        // I dont really understand
        return new Vector3(Mathf.Sin(angleDegress * Mathf.Deg2Rad), 0, Mathf.Cos(angleDegress * Mathf.Deg2Rad));
    }
}
