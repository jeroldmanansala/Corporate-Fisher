using UnityEngine;

public class Bobber : MonoBehaviour
{

    public bool gameIsOver;
    public Animator bobberAnim;
    public float bobberTime;
    public bool isInWater = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bobberTime += Time.deltaTime;
        if(bobberTime >= 3)
        {
            bobberAnim.Play("bobberFish");
        }
        if(Input.GetKeyDown(KeyCode.P) && bobberTime <= 3)
        {
            Destroy(gameObject);
        }
        if(gameIsOver == true)
        {
            Destroy(gameObject);
        }
    }

    public void gameOver()
    {
        gameIsOver = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            isInWater = true;
            Debug.Log("Bobber hit water!");
        }
    }

    
}
