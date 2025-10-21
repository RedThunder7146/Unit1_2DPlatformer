using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerMoveScript : MonoBehaviour
{
    HelperScript helper;
    public Rigidbody2D myRigidbody;
    public float playerSpeedY;
    public float playerSpeedX;
    public bool onGround = false;
    public Animator playerAnim;
    public LayerMask groundLayer;
    public GameObject weapon;
    public bool SillyMode = false;
    public LogicScript logic;
    float delay = 2;
    public GameObject Reloading;
    public GameObject outOfAmmo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        helper = gameObject.AddComponent<HelperScript>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        logic.bullets = 30;
        logic.magCount = 120;
        delay -= Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(reload());
        }
        Shoot();
        if (Input.GetKey(KeyCode.S))
        {
            SillyMode = true;
            print("SillyMode activated");
        }
        if (Input.GetKey(KeyCode.Minus))
        {
            SillyMode = false;
            print("SillyMode deactivated");
        }
        SillyShoot();
        if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.R))
        {
            helper.ResetGame();
        }
        else if (Input.GetKeyDown(KeyCode.F1))
        {
            helper.ResetScene();
        }

        if (Input.GetKey(KeyCode.Space)&&IsGrounded())
        {


            myRigidbody.linearVelocityY = 1f * playerSpeedY;
        }
        
        if (Input.GetKey(KeyCode.A))
        {

            helper.DoFlipObject(true);
            myRigidbody.linearVelocityX = -1f * playerSpeedX;
            
            if (IsGrounded())
            {
                playerAnim.SetBool("IsRunning", true);
            }
            
        }
        else if (Input.GetKey(KeyCode.D))
        {

            helper.DoFlipObject(false);
            myRigidbody.linearVelocityX = 1f * playerSpeedX;

            if (IsGrounded())
            {
                playerAnim.SetBool("IsRunning", true);
            }


        }
        
        else
        {
            playerAnim.SetBool("IsRunning", false);
            

        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            Sprint();
        }

        else
        {
            playerAnim.SetBool("IsSprinting", false);
        }

        if (IsGrounded())
        {
            playerAnim.SetBool("IsJumping", false);
        }
        else
        {
            playerAnim.SetBool("IsJumping", true);
        }

    }

    bool IsGrounded()
    {

        float distance = 0.1f;
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down * distance;
        
        Debug.DrawRay(position, direction, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
        
    }
    public void Sprint()
    {
        if (Input.GetKey(KeyCode.Space) && IsGrounded())
        {


            myRigidbody.linearVelocityY = 1f * playerSpeedY;
        }
        if (Input.GetKey(KeyCode.A))
        {


            myRigidbody.linearVelocityX = -2f * playerSpeedX;
            if (IsGrounded())
            {
                playerAnim.SetBool("IsSprinting", true);
            }

        }
        else if (Input.GetKey(KeyCode.D))
        {


            myRigidbody.linearVelocityX = 2f * playerSpeedX;

            if (IsGrounded())
            {
                playerAnim.SetBool("IsSprinting", true);
            }
        }
        else
        {
            playerAnim.SetBool("IsSprinting", false);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            playerAnim.SetBool("IsHurt", true);
        }
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        { 
            playerAnim.SetBool("IsHurt", true);
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy") 
        {
            playerAnim.SetBool("IsHurt", false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerAnim.SetBool("IsHurt", false);
    }

    public void Shoot()
    {

        if (logic.bullets > 0)
        {
            int moveDirection = 1;

            if (delay >= 0)
            {
                if (Input.GetMouseButtonDown(0))
                {

                    if (logic.bullets > 0)
                    {
                        GameObject clone;
                        clone = Instantiate(weapon, transform.position, transform.rotation);
                        Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
                        rb.linearVelocity = transform.right * 15;
                        rb.transform.position = new Vector3(transform.position.x, transform.position.y +
                        2, transform.position.z + 1);
                        logic.BulletsFired(1);


                    }

                    else
                    {
                        print("Out of ammo");
                    }
                }
            }
        }
        
        
        

    }

    public void SillyShoot()
    {
        if (SillyMode == true)
        {
            
                if (Input.GetMouseButton(0))
                {

                    if (logic.bullets > 0)
                    {
                        GameObject clone;
                        clone = Instantiate(weapon, transform.position, transform.rotation);
                        Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
                        rb.linearVelocity = transform.right * 15;
                        rb.transform.position = new Vector3(transform.position.x, transform.position.y +
                        2, transform.position.z + 1);
                        logic.BulletsFired(1);

                    }



                    else
                    {
                        print("Out of ammo");
                    }
                }



            

            
            
        }
        
        
    }
    public IEnumerator reload()
    {
        if (logic.magCount > 0)
        {
            if (logic.bullets ==0)
            {
                Reloading.SetActive(true);
                yield return new WaitForSecondsRealtime(2.5f);
                logic.bullets = 30;
                logic.MagazineCount(30);
                Reloading.SetActive(false);
            }
            
        }

        else
        {
            outOfAmmo.SetActive(true);
            yield return new WaitForSecondsRealtime(2.5f);
            outOfAmmo.SetActive(false);
        }

    }

}
