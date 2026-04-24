using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public float speed = 5f;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;

    public int maxAmmo = 5;
    private int ammo;

    public int maxHealth = 1;
    private int currentHealth;

    private Vector2 lastMoveDir = Vector2.up; // muistaa suunnan
    private bool isDead = false; 

    void Start()
    {
        ammo = maxAmmo;
        currentHealth = maxHealth; 
    }

    void Update()
    {
        Move();
        Shoot();
    }

    void Move()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        if (Input.GetKey(KeyCode.D)) moveX = 1f;
        if (Input.GetKey(KeyCode.W)) moveY = 1f;
        if (Input.GetKey(KeyCode.S)) moveY = -1f;

        Vector2 dir = new Vector2(moveX, moveY).normalized;

        if (dir != Vector2.zero)
        {
            lastMoveDir = dir; 
        }

        Vector3 movement = new Vector3(moveX, moveY, 0f);
        transform.Translate(movement * speed * Time.deltaTime);
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

    public bool IsDead()
    {
        return isDead;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true; 
        Debug.Log("Player died!");

        // yksinkertainen reset
        transform.position = Vector3.zero;
        currentHealth = maxHealth;
        ammo = maxAmmo;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ammo"))
        {
            TakeDamage(10);
            Destroy(collision.gameObject);
        }
    }
}