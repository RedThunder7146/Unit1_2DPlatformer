using TMPro;
using UnityEditor;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject Player;
    public float Speed;
    public Animator SlimeAnim;
    public LayerMask groundLayer;
    public Rigidbody2D slimeRigidBody;
    public LogicScript logic;
    float xvel = 1;
    HelperScript helper;
    public TextMeshPro enemyHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        helper = gameObject.AddComponent<HelperScript>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        logic.enHealth = 3;
    }

    // Update is called once per frame
    void Update()
    {
        // print("Enemy says: Player has " + logicScript.health + " health left");
        if (xvel < 0)
        {
            helper.DoFlipObject(true);
            if (ExtendedRayCollisionCheck(-2, 0) == false)
            {
                xvel = -xvel;
                SlimeAnim.SetBool("IsRunning", true);
            }
        }
        if (xvel >0)
        {
            helper.DoFlipObject(false);
            if (ExtendedRayCollisionCheck(2, 0) == false)
            {
                xvel = -xvel;
                SlimeAnim.SetBool("IsRunning", true);
            }
        }
        EnemyDeath();
        
        if (xvel ==0)
        {
            SlimeAnim.SetBool("IsRunning", false);
        }
        if (logic.health == 3)
        {
            slimeRigidBody.linearVelocityX = 1f * xvel * Speed;
        }
        else if (logic.health == 2)
        {
            slimeRigidBody.linearVelocityX = 2f * xvel * Speed;
        }
        else
        {
            slimeRigidBody.linearVelocityX = 3f * xvel * Speed;
        }
    }
       
    


    public bool ExtendedRayCollisionCheck(float xoffs, float yoffs)
    {
        
        float rayLength = 1.0f; // length of raycast
        bool hitSomething = false;

        // convert x and y offset into a Vector3 
        Vector3 offset = new Vector3(xoffs, yoffs, 0);

        //cast a ray downward 
        RaycastHit2D hit;


        hit = Physics2D.Raycast(transform.position + offset, Vector2.down, rayLength, groundLayer);

        Color hitColor = Color.purple;


        if (hit.collider != null)
        {

            hitColor = Color.green;
            hitSomething = true;
        }
        // draw a debug ray to show ray position
        // You need to enable gizmos in the editor to see these
        Debug.DrawRay(transform.position + offset, Vector3.down * rayLength, hitColor);

        return hitSomething;

    }

    public void EnemyDeath()
    {
        if(logic.enHealth == 0)
        {
            Destroy(gameObject);
        }
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            logic.EnemyHealth(1);

        }
    }


}
