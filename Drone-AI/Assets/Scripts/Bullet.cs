using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 12f;
    public float life_bullet = 2f;
    private float bullet_timer;
    public int damage = 10;
    private bool shotByPlayer;
    public bool ShotByPlayer { get { return shotByPlayer; } set { shotByPlayer = value; } }

    void OnEnable()
    {
        bullet_timer = life_bullet;
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        bullet_timer -= Time.deltaTime;
        if (bullet_timer <= 0)
        {
            gameObject.SetActive(false);
            /*Destroy(gameObject);*/
        }
    }
}
