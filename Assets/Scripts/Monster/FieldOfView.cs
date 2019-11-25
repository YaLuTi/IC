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
        GetTarget();
    }

    public void GetTarget()
    {
        ViewTargets.Clear();
        Collider[] targets = Physics.OverlapSphere(transform.position, Radius, TargetMask);
        for(int i = 0; i < targets.Length; i++)
        {
            Transform target = targets[i].transform;
            if (!target.CompareTag("Player")) continue;
            Vector3 dir = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dir) < Angle / 2)
            {
                float Distance = Vector3.Distance(transform.position, target.transform.position);

                RaycastHit raycastHitl;
                if (!Physics.Raycast(transform.position, dir, out raycastHitl, Distance, ObjMask))
                {
                    ViewTargets.Add(targets[i].gameObject);
                    monster.e_FoundPlayer();
                }
                else
                {
                    Debug.Log(raycastHitl.collider.name);
                }
            }
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
