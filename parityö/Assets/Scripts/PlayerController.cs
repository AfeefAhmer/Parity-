using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    // Ampuminen
    public GameObject AmmoPrefab;
    public float AmmoSpeed = 10f;
    public Transform Amppuminen;

    // AMMO LIMIT
    public int maxAmmo = 5;
    private int currentAmmo;

    // Health
    public int maxHealth = 1;
    private int currentHealth;

    private bool isDead = false;

    void Start()
    {
        currentAmmo = maxAmmo;
        currentHealth = maxHealth;
    }

    public int GetAmmo()
    {
        return currentAmmo;
    }

    public bool IsDead()
    {
        return isDead;
    }

    void Update()
    {
        if (isDead) return;

        Move();
        Shoot();
    }

    void Move()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.LeftArrow)) moveX = -1f;
        if (Input.GetKey(KeyCode.RightArrow)) moveX = 1f;
        if (Input.GetKey(KeyCode.UpArrow)) moveY = 1f;
        if (Input.GetKey(KeyCode.DownArrow)) moveY = -1f;

        Vector3 movement = new Vector3(moveX, moveY, 0f);
        transform.Translate(movement * speed * Time.deltaTime);
    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.C) && currentAmmo > 0)
        {
            GameObject bullet = Instantiate(AmmoPrefab, Amppuminen.position, Quaternion.identity);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 dir = new Vector2(
                    Input.GetAxisRaw("Horizontal"),
                    Input.GetAxisRaw("Vertical")
                ).normalized;

                if (dir == Vector2.zero)
                    dir = Amppuminen.up;

                rb.linearVelocity = dir * AmmoSpeed;
                bullet.transform.up = dir;
            }

            currentAmmo--;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

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

        // Piilotetaan pelaaja (ei tuhota heti)
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ammo"))
        {
            TakeDamage(1); // 1 damage riittää kun maxHealth = 1
            Destroy(collision.gameObject);
        }
    }
}