using UnityEngine;

public class EnemyShooterTriple : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float fireRate = 1.5f;
    // public float bulletSpeed = 5f; // Dihapus karena speed sudah ada di Bullet.cs

    private float fireTimer = 0f;
    public float speed = 3f;

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f)
        {
            FireTripleShot();
            fireTimer = fireRate;
        }
    }

    void FireTripleShot()
    {
        // --- MODIFIKASI 2: Normalisasi Vektor Arah ---
        // Vektor Y yang lebih besar (misalnya 1) akan membuat tembakan lebih miring ke atas/bawah.
        Vector2[] dirs = new Vector2[]
        {
            new Vector2(-1,  0.2f).normalized,    // Kiri Atas (100% miring ke atas)
            new Vector2(-1,  0).normalized,    // Kiri (Horizontal)
            new Vector2(-1, -0.2f).normalized,    // Kiri Bawah (100% miring ke bawah)
        };
        // Anda juga bisa mencoba:
        // new Vector2(-1, 0.5f).normalized  // Jika ingin kemiringan yang lebih landai

        foreach (Vector2 d in dirs)
        {
            // Rotasi sekarang ditangani di script Bullet.cs, 
            // jadi kita bisa membiarkan Quaternion.identity di sini.
            GameObject b = Instantiate(bulletPrefab, transform.position, Quaternion.identity); 

            Bullet bullet = b.GetComponent<Bullet>();
            
            // Menggunakan Vector3 casting agar sesuai dengan parameter SetDirection
            bullet.SetDirection(d); 
        }
    }
}