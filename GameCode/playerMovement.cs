using UnityEngine;

public class playerMovement : MonoBehaviour
{   
    //global variables
    [SerializeField] private float speed;
    [SerializeField] private float jumppower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    //private bool jump = true;  --Opted for ray cast instead so can add wall jumps
    private Rigidbody2D body;
    private Animator animate;
    private BoxCollider2D boxCollider;
    private float wallCooldown;
    private bool fastfall = false;
    private float horizontalInput;

    private void Awake() {
        //grabs references for components
        body = GetComponent<Rigidbody2D>();
        animate = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update() {
        horizontalInput = Input.GetAxis("Horizontal");

        //movement

        if (canjump() == true)
        {
            body.gravityScale = 3;
            fastfall = false;
        }

        //activates fast fall
        if ((Input.GetKeyDown(KeyCode.DownArrow) | Input.GetKeyDown(KeyCode.S))){
            //body.velocity = new Vector2(body.velocity.x, -speed*2/3); -- opted for gravity increase instead
            body.gravityScale = 10;
            fastfall = true;
        }
       //checks to see if chara is on ground to reset jump
       //update: better method found, see OnCollisionEnter2D method instead
       // if (body.velocity.y == 0)
       //jump = true;

            //flips character
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(2, 2, 1);

        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-2, 2, 1);

        //set animtor parameters
        animate.SetBool("run", horizontalInput != 0);
        animate.SetBool("grounded",canjump());

        if (wallCooldown > 0.4f)
        {
            
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (canWallJump() && !canjump())
            {
                body.gravityScale = 3;
                body.velocity = new Vector2(0,0);
                animate.SetTrigger("jump");
            }
            else if (!fastfall){
                body.gravityScale = 3;
            }
            
            if (Input.GetKeyDown(KeyCode.UpArrow) | Input.GetKeyDown(KeyCode.W))
            {
                Jump();
                
            }

        }
        else
            wallCooldown += Time.deltaTime;
        
        
    }
 
    private void Jump()
    {
        if (canjump())
        {
            body.velocity = new Vector2(body.velocity.x, jumppower);
            animate.SetTrigger("jump");
            //jump = false; -- using new canjump() method instead
        }
        else if (canWallJump() && !canjump()) {

            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 15, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x) * 2, transform.localScale.y, transform.localScale.z);
            }
            else{
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 6, 15);
            }
            
            wallCooldown = 0;
            body.gravityScale = 3;

        }
    }

    //private void OnCollisionEnter2D(Collision2D collision) {
       // if (collision.gameObject.tag == "ground") -- using new canjump() method instead
       //     jump = true;
    //}

    private bool canjump()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0,Vector2.down,0.1f,groundLayer);
       
        return raycastHit.collider != null;
    }

    private bool canWallJump()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
    public bool canAttack() {
        return !canWallJump();    
    }
    public void die(bool isDead) {
        if (isDead) {
            body.velocity = Vector2.zero;
        }
    }
}
