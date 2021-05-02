using UnityEngine;

public class Owl : Bird
{
    [SerializeField]
    private float _explosionRadius;
    [SerializeField]
    private float _explosionPower;

    // Burung akan meledak ketika bersentuhan dengan objek lain
    public void Explode()
    {
        Vector2 explosionPos = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, _explosionRadius);

        foreach (Collider2D hit in colliders)
        {
            Rigidbody2D rigidbody2D = hit.GetComponent<Rigidbody2D>();
            Vector2 dir = hit.gameObject.transform.position - transform.position;

            if (rigidbody2D != null)
            {
                rigidbody2D.AddForce(dir * _explosionPower, ForceMode2D.Impulse);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Explode();
    }
}
