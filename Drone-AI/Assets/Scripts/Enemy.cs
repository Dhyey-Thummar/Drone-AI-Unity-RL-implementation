using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemy_health = 5;
    public int damage = 5;
    private bool killed = false;
    public bool Killed { get { return killed; } }
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bullet>() != null)
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet.ShotByPlayer == true)
            {
                enemy_health -= bullet.damage;
                bullet.gameObject.SetActive(false);
            }
            if (enemy_health <= 0)
            {
                if(killed == false)
                {
                    killed = true;
                    OnKill();
                }
                
            }
        }   
    }
    protected virtual void OnKill()
    {

    }
}   

  
