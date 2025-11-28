using UnityEngine;

public class Movements : MonoBehaviour
{
    [Header("Movement")]
    public GameObject gameOverCanvas;
    public float speed = 5f;

    [Header("Dash Settings")]
    public float dashSpeed = 20f;
    public float dashDuration = 0.25f;
    public float dashCooldown = 5f;
    public float postDashInvincibleTime = 0.5f;

    [Header("Charges")]
    public int chargeCount = 2;
    private Charge[] charges;

    [Header("Animation")]
    [SerializeField] private Animator animator;
    public GameObject explosionPrefab;

    private bool isDashing = false;
    private float dashTimeLeft = 0f;
    private Vector2 dashDirection;

    private float invincibleTimer = 0f;
    private Rigidbody2D rb;
    private Vector2 movement;

    public float padding = 0.5f;
    private Camera cam;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        charges = new Charge[chargeCount];
        for (int i = 0; i < chargeCount; i++)
        {
            charges[i] = new Charge(dashCooldown);
        }
        cam = Camera.main;
    }

    void Update()
    {
        if (!isDashing)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            if (movement.y > 0){
                animator.SetBool("isUp", true);
            }else{
                animator.SetBool("isUp", false);
            }

            if (movement.y < 0){
                animator.SetBool("isDown", true);
            }else{
                animator.SetBool("isDown", false);
            }
            
            if (movement.sqrMagnitude > 1f)
                movement = movement.normalized;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
            TryDash();

        // update cooldown tiap charge
        foreach (var c in charges)
            c.Update(Time.deltaTime);

        // dash timer
        if (isDashing)
        {
            dashTimeLeft -= Time.deltaTime;
            if (dashTimeLeft <= 0f)
            {
                isDashing = false;
                invincibleTimer = postDashInvincibleTime;
            }
        }

        if (invincibleTimer > 0f)
            invincibleTimer -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        if (isDashing)
            rb.MovePosition(rb.position + dashDirection * dashSpeed * Time.fixedDeltaTime);
        else
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        ClampToScreen();
    }

    void ClampToScreen()
    {
        if (cam == null) return;

        Vector3 pos = transform.position;

        Vector3 min = cam.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 max = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        pos.x = Mathf.Clamp(pos.x, min.x + padding, max.x - padding);
        pos.y = Mathf.Clamp(pos.y, min.y + padding, max.y - padding);

        transform.position = pos;
    }

    void TryDash()
    {
        int slot = GetReadyChargeIndex();
        if (slot == -1 || isDashing)
            return;

        // tentuin arah dash
        if (movement != Vector2.zero)
            dashDirection = movement.normalized;
        else
            dashDirection = -transform.right;

        // aktifin dash
        isDashing = true;
        dashTimeLeft = dashDuration;

        // consume slot ini
        charges[slot].Consume();
    }

    int GetReadyChargeIndex()
    {
        for (int i = 0; i < charges.Length; i++)
        {
            if (charges[i].IsReady)
                return i;
        }
        return -1;
    }

    public Charge GetCharge(int index)
    {
        return charges[index];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // kalau masih invincible (lagi dash / post dash), ga kena hit
        if (isDashing || invincibleTimer > 0f)
            return;

        if (other.CompareTag("Enemy") || other.CompareTag("EnemyBullet"))
        {
            // Debug test
            Debug.Log("Player Hit!");
            
            Destroy(gameObject, 0.2f);
            //ledakan klo mati
            GameObject explosion = Instantiate(explosionPrefab, other.transform.position, Quaternion.identity);
            Destroy(explosion, 1f);

            Time.timeScale = 1f;
            if (gameOverCanvas != null)
                gameOverCanvas.SetActive(true);
        }
    }

}

[System.Serializable]


public class Charge
{
    private float cooldown;
    private float timer = 0f;

    public bool IsReady => timer <= 0f;
    public float CooldownPercent => Mathf.Clamp01(1 - (timer / cooldown));

    public Charge(float cooldown)
    {
        this.cooldown = cooldown;
        timer = 0f;
    }

    public void Update(float dt)
    {
        if (timer > 0f)
        {
            timer -= dt;
        }
    }

    public void Consume()
    {
        timer = cooldown;
    }
}
