using TMPro;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public TextMeshPro enemyHealth;
    public LogicScript logic;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.gameObject.tag != "Player") || (collision.gameObject.tag != "Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
