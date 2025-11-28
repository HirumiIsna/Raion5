using UnityEngine;
using System.Collections;

public class UltimateHitbox : MonoBehaviour
{
    public float lifeTime = 0.3f;

    void Start()
    {
        StartCoroutine(ActivateHitbox());
        Destroy(gameObject, lifeTime);
    }

    IEnumerator ActivateHitbox()
    {
        Collider c = GetComponent<Collider>();
        c.enabled = false;

        yield return new WaitForSeconds(0.5f);

        c.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy") || other.CompareTag("EnemyBullet"))
        {
            Destroy(other.gameObject);
        }
    }

}
