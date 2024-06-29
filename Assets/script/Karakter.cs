using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;         // Kecepatan gerak horizontal
    public float jumpForce = 10f;    // Kekuatan lompatan
    private bool isGrounded = true;  // Apakah karakter sedang berada di tanah?

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Mendapatkan komponen Rigidbody2D dan SpriteRenderer dari GameObject ini
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Pergerakan horizontal
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal * speed, rb.velocity.y);
        rb.velocity = movement;

        // Melompat jika berada di tanah dan tombol Space pertama kali ditekan
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump(); // Memanggil fungsi untuk melompat
        }

        // Mengubah orientasi sprite berdasarkan arah gerakan
        if (moveHorizontal < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveHorizontal > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    void Jump()
    {
        // Menambahkan kekuatan lompatan ke Rigidbody2D
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        isGrounded = false; // Setelah melompat, karakter tidak lagi di tanah
        Debug.Log("Karakter melompat"); // Log untuk lompatan
    }

    // Mengecek apakah karakter menyentuh tanah
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Karakter kembali berada di tanah
            Debug.Log("Karakter menyentuh tanah"); // Log untuk menyentuh tanah
        }
    }

    // Menangani kasus di mana karakter terus berada di tanah
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Memastikan karakter tetap dianggap di tanah
            Debug.Log("Karakter tetap di tanah"); // Log untuk tetap di tanah
        }
    }

    // Menangani kasus di mana karakter meninggalkan tanah
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false; // Karakter meninggalkan tanah
            Debug.Log("Karakter meninggalkan tanah"); // Log untuk meninggalkan tanah
        }
    }
}
