using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponColliderBasic : MonoBehaviour {

    bool Attacking = false;

    public float AttackDamage = 1f;

    public GameObject HitParticle;
    public Transform p;
    LayerMask layerMask;

    [SerializeField]
    List<GameObject> hitObject = new List<GameObject>();

    Collider collider;
	// Use this for initialization
	void Start () {
        layerMask = LayerMask.GetMask("Creature");
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

    private void OnTriggerStay(Collider other)
    {
        if (Attacking && !hitObject.Contains(other.gameObject))
        {
            hitObject.Add(other.gameObject);

            if (((1 << other.gameObject.layer) & layerMask) != 0)
            {
                if (other.GetComponent<MonsterBasic>())
                {
                    MonsterBasic monster = other.gameObject.GetComponent<MonsterBasic>();
                    monster.Damaged(AttackDamage);
                    // Quaternion r = p.rotation;
                    // r.y = -r.y
                    GameObject g = Instantiate(HitParticle, other.ClosestPoint(transform.position), Quaternion.identity);
                    Destroy(g, 3f);
                }
                else if(other.GetComponent<PlayerHP>())
                {
                    PlayerHP playerHP = other.gameObject.GetComponent<PlayerHP>();
                    playerHP.Damaged(AttackDamage);
                    if (playerHP.Invulnerability) return;
                    GameObject g = Instantiate(HitParticle, other.ClosestPoint(transform.position), Quaternion.identity);
                }
            }
        }
    }
}
