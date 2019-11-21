using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponColliderBasic : MonoBehaviour {

    public bool Attacking = false;

    public float AttackDamage = 1f;
    public float DestroyTime = 0.5f;
    public GameObject HitParticle;
    public Transform p;
    LayerMask layerMask;

    [SerializeField]
    List<Transform> hitObject = new List<Transform>();

    Collider collider;
	// Use this for initialization
	void Start () {
        string[] s = { "Player", "Creature" };
        layerMask = LayerMask.GetMask(s);
        Physics.IgnoreCollision(this.GetComponent<Collider>(), this.GetComponentInParent<Collider>(), false);
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void StartAttack()
    {
        Attacking = true;
    }

    public void StopAttack()
    {
        hitObject.Clear();
        Attacking = false;
    }

    // Very 耗能
    private void OnTriggerStay(Collider other)
    {
        if (Attacking && !hitObject.Contains(other.gameObject.transform.root))
        {

            if (((1 << other.gameObject.layer) & layerMask) != 0)
            {
                hitObject.Add(other.gameObject.transform.root);
                if (other.GetComponentInParent<MonsterBasic>())
                {
                    MonsterBasic monster = other.gameObject.GetComponentInParent<MonsterBasic>();
                    monster.Damaged(AttackDamage, other.ClosestPoint(transform.position), transform.position);
                    // Quaternion r = p.rotation;
                    // r.y = -r.y
                    GameObject g = Instantiate(HitParticle, other.ClosestPoint(transform.position), Quaternion.identity);
                    Destroy(g, DestroyTime);
                }
                else if(other.GetComponent<PlayerHP>())
                {
                    PlayerHP playerHP = other.gameObject.GetComponent<PlayerHP>();
                    playerHP.Damaged(AttackDamage);
                    if (playerHP.Invulnerability) return;
                    GameObject g = Instantiate(HitParticle, other.ClosestPoint(transform.position), Quaternion.identity);
                    Destroy(g, DestroyTime);
                }
            }
        }
    }
}
