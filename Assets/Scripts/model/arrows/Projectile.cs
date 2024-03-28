using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

public class Projectile : MonoBehaviour
{

    public float speed = 5f;

    private int[] damageValues = new int[2] { 40, 20 };
    public int Damage { get { return damage; } }
    private int damage;

    public int projectile_type;

    public Transform target;
    private bool hasHitAnEnemy;

    // Start is called before the first frame update
    void Start()
    {
        this.damage = damageValues[projectile_type];
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            // Calcula la dirección hacia el objetivo
            Vector2 direccion = (target.position - transform.position).normalized;

            // Mueve el proyectil en la dirección calculada
            transform.Translate(direccion * speed * Time.deltaTime);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    /*
    When the projectile collides with an enemy(rest of collisions are disabled), we check if its gameObject and its script
    are not null, in case they've just been destroyed. If they're not, we get their script component to apply damage later.
    Before that, we use -hasHitAnEnemy- class variable for projectiles from type==0 do damage only to the first collider they hit,
    if that projectile has already done damage to any other gameObject, it destroys itself.

    In case the projectile is type==1, they do AOE damage(can collide multiple times), and we also check if the target is not
    slowed, this slows their speed down. Also triggers their hurt animation. After collision, the projectiles self-destruct
     
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject != null && collision.gameObject.GetComponent<Ork>() != null)
        {
            Ork ork = collision.gameObject.GetComponent<Ork>();
            if (hasHitAnEnemy && projectile_type == 0)
            {
                return;
            }
            else if (projectile_type == 1 && !ork.isSlowed)
            {
                ork.SlowDown();
            }
            collision.gameObject.GetComponent<Animator>().SetTrigger("Hurt");
            dealDamage(damage, ork.transform);
        }
        hasHitAnEnemy = true;
        Destroy(this.gameObject);
    }

    /*
     Instantiated projectile does its damage to their target. 
     We use parameters for damage and the transform from the collided unit, then get their script and apply armor logic.
     After that, we substract the amount from the instances health and in case its health gets below 0, destroy their gameObject
     */
    public void dealDamage(int amount, Transform target)
    {
        if (target != null)
        {
            Ork ork = target.gameObject.GetComponent<Ork>();
            ork.Health -= (int)(amount * (1 - (ork.getArmorReduction() / 100)));
            if (ork.Health <= 0)
            {
                Destroy(ork.gameObject);
            }
        }
    }
}
