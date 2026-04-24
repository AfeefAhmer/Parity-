using UnityEngine;

public class AmmoController : MonoBehaviour
{
    //public float speed = 10f;
    public float lifetime = 5f;
    //Rigidbody2D rb;
    void Start()
    {
        //rb= GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime); // varmuuden vuoksi
    }

    //void Update()
    //{
    //    rb.linearVelocity=(transform.right * speed);
    //}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}