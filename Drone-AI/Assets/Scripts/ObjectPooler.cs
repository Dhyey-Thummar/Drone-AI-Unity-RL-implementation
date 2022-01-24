using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    private static ObjectPooler instance;
    public static ObjectPooler Instance { get { return instance; } }

    public GameObject bulletPrefab;
    public int bullet_amount = 20;

    private List<GameObject> bullets;
    void Awake ()
    {
        instance = this;

        bullets = new List<GameObject>(bullet_amount); 

        for (int i = 0; i<bullet_amount; i++)
        {
            GameObject prefabInstance = Instantiate (bulletPrefab);
            prefabInstance.transform.SetParent (transform);
            prefabInstance.SetActive (false);

            bullets.Add(prefabInstance);
        }
    }
   public GameObject GetBullet (bool shotByPlayer)
    {
        foreach (GameObject  bullet in bullets)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.SetActive(true);
                bullet.GetComponentInChildren<Bullet>().ShotByPlayer = shotByPlayer;
                return bullet;
            }
        }

        GameObject prefabInstance = Instantiate(bulletPrefab);
        prefabInstance.transform.SetParent(transform);
        prefabInstance.GetComponent<Bullet>().ShotByPlayer = shotByPlayer;
        prefabInstance.SetActive(false);

        bullets.Add(prefabInstance);

        return prefabInstance;
    }
}
