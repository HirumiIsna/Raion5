using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 direction;
    [SerializeField] private float speed = 5f; 
    private float lifetime = 5f;

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
        
        // --- KOREKSI ROTASI ---
        
        // 1. Hitung Sudut Berdasarkan Arah Vektor (X dan Y)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        // 2. Terapkan Offset
        
        // Jika sprite Anda defaultnya TEGAK (menghadap ke atas, Z=90 untuk ke kanan):
        // Kita butuh offset +90 derajat agar sprite yang tegak lurus (defaultnya) menjadi horizontal.
        float rotationOffset = 90f; 
        
        // Peluru harus menghadap ke arah yang dihitung (angle)
        // Jika peluru tetap menghadap ke kanan saat angle sudah untuk KIRI (-180 sampai -90),
        // coba tambahkan 180 derajat untuk membaliknya ke kiri.
        
        // Karena angle KIRI akan menghasilkan 180 derajat, dan peluru defaultnya miring 90 derajat,
        // kita menggunakan formula ini:
        transform.rotation = Quaternion.AngleAxis(angle + rotationOffset, Vector3.forward); 
        
        // 🚨 Pilihan Alternatif (Jika yang di atas salah):
        // Jika sprite Anda sudah menghadap KANAN saat Z=0 (Bukan 90):
        // transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Update()
    {
        // Pergerakan:
        transform.position += direction * speed * Time.deltaTime;

        lifetime -= Time.deltaTime;
        if (lifetime <= 0f)
        {
            Destroy(gameObject);
        }
    }
}