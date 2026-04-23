using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public float speed = 5f;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;

    public int maxAmmo = 5;
    private int ammo;

    public int health = 1;

    private Vector2 lastMoveDir = Vector2.up; // muistaa suunnan

    void Start()
    {
        ammo = maxAmmo;
    }

    void Update()
    {
        Move();
        Shoot();
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 move = new Vector2(x, y).normalized;

        if (move != Vector2.zero)
        {
            lastMoveDir = move; // tallennetaan suunta
        }

        transform.position += (Vector3)move * speed * Time.deltaTime;
    }

    void Shoot()
    {
        if (!Input.GetKeyDown(KeyCode.E)) return;
        if (ammo <= 0) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = lastMoveDir * bulletSpeed;
            bullet.transform.up = lastMoveDir;
        }

        ammo--;
    }

   
    public int GetAmmo()
    {
        return ammo;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}