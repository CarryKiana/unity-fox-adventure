using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // [SerializeField]
    private Rigidbody2D rb;
    private Animator anim;

    public float speed;
    public float jumpforce;
    public LayerMask ground;
    public Collider2D coll;
    public int Cherry;
    public Text CherryNum;
    private bool isHurt;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isHurt) {
            Movement();
        }
        SwitchAnim();
    }

    void Movement()
    {
        // 取值是-1到1的区间
        float horizontalmove = Input.GetAxis("Horizontal");
        // 取值是-1,0,1
        float facedirection = Input.GetAxisRaw("Horizontal");
        // 角色移动
        if (horizontalmove != 0)
        {
            rb.velocity = new Vector2(horizontalmove * speed * Time.deltaTime, rb.velocity.y);
            anim.SetFloat("running", Mathf.Abs(facedirection));
        }
        if (facedirection != 0) {
            transform.localScale = new Vector3(facedirection, 1, 1);
        }
        // 角色跳跃
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground)) {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);
            anim.SetBool("jumping", true);
            anim.SetBool("falling", false);
        }
    }

    void SwitchAnim () {
        anim.SetBool("idle", false);
        if (rb.velocity.y < 0.1f && !coll.IsTouchingLayers(ground)) {
            anim.SetBool("falling", true);
            print("我在下落啦");
        }
        if (anim.GetBool("jumping")) {
            if (rb.velocity.y < 0) {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        } else if (isHurt) {
            anim.SetBool("hurt", true);
            anim.SetFloat("running", 0);
            if (Mathf.Abs(rb.velocity.x) < 0.1f) {
                anim.SetBool("hurt", false);
                anim.SetBool("idle", true);
                isHurt = false;
            }
        } else if (coll.IsTouchingLayers(ground)) {
            anim.SetBool("falling", false);
            anim.SetBool("idle", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Collection") {
            Destroy(collision.gameObject);
            Cherry += 1;
            CherryNum.text = Cherry.ToString();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            if (anim.GetBool("falling")) {
                Destroy(collision.gameObject);
                rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);
                anim.SetBool("jumping", true);
            } else if (transform.position.x < collision.gameObject.transform.position.x) {
                // 左侧
                isHurt = true;
                rb.velocity = new Vector2(-5, rb.velocity.y);
            } else if (transform.position.x > collision.gameObject.transform.position.x) {
                // 右侧
                isHurt = true;
                rb.velocity = new Vector2(5, rb.velocity.y);
            }
        }
    }
}
