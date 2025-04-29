using UnityEngine;

public class fishingBarScript : MonoBehaviour
{

    public Rigidbody rb;
    public bool atTop;
    public float targetTime = 4.0f;
    public float savedTargetTime;

    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public GameObject p4;
    public GameObject p5;
    public GameObject p6;
    public GameObject p7;
    public GameObject p8;

    public bool onFish;
    public PlayerScript playerS;
    public GameObject bobber;

    public float minY = -1.73f;
    public float maxY = 0.61f;

    public GameObject fishIcon; 
    public float fishSpeed = 1.5f;

    private float fishTargetY;
    private float fishWaitTime = 0f;
    private float fishMaxWaitTime = 1f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fishTargetY = Random.Range(minY, maxY);
    }

    // Update is called once per frame
    void Update()
    {
        if(onFish == true)
        {
            targetTime += Time.deltaTime;
        }
        if(onFish == false)
        {
            targetTime -= Time.deltaTime;
        }
        if(targetTime <= 0.0f)
        {
            transform.localPosition = new Vector3(-0.111f, 0.3991613f, 0);
            onFish = false;
            playerS.fishGameLossed();
            Destroy(GameObject.Find("Bobber(Clone)"));
            targetTime = 1.0f;
        }
        if(targetTime >= 8.0f)
        {
            transform.localPosition = new Vector3(-0.111f, 0.3991613f, 0);
            onFish = false;
            playerS.fishGameWon();
            Destroy(GameObject.Find("Bobber(Clone)"));
            targetTime = 1.0f;
        }
        if (targetTime <= 0f)
        {
            p1.SetActive(false);
            p2.SetActive(false);
            p3.SetActive(false);
            p4.SetActive(false);
            p5.SetActive(false);
            p6.SetActive(false);
            p7.SetActive(false);
            p8.SetActive(false);
        }
        else if (targetTime <= 1.0f)
        {
            p1.SetActive(true);
            p2.SetActive(false);
            p3.SetActive(false);
            p4.SetActive(false);
            p5.SetActive(false);
            p6.SetActive(false);
            p7.SetActive(false);
            p8.SetActive(false);
        }
        else if (targetTime <= 2.0f)
        {
            p1.SetActive(true);
            p2.SetActive(true);
            p3.SetActive(false);
            p4.SetActive(false);
            p5.SetActive(false);
            p6.SetActive(false);
            p7.SetActive(false);
            p8.SetActive(false);
        }
        else if (targetTime <= 3.0f)
        {
            p1.SetActive(true);
            p2.SetActive(true);
            p3.SetActive(true);
            p4.SetActive(false);
            p5.SetActive(false);
            p6.SetActive(false);
            p7.SetActive(false);
            p8.SetActive(false);
        }
        else if (targetTime <= 4.0f)
        {
            p1.SetActive(true);
            p2.SetActive(true);
            p3.SetActive(true);
            p4.SetActive(true);
            p5.SetActive(false);
            p6.SetActive(false);
            p7.SetActive(false);
            p8.SetActive(false);
        }
        else if (targetTime <= 5.0f)
        {
            p1.SetActive(true);
            p2.SetActive(true);
            p3.SetActive(true);
            p4.SetActive(true);
            p5.SetActive(true);
            p6.SetActive(false);
            p7.SetActive(false);
            p8.SetActive(false);
        }
        else if (targetTime <= 6.0f)
        {
            p1.SetActive(true);
            p2.SetActive(true);
            p3.SetActive(true);
            p4.SetActive(true);
            p5.SetActive(true);
            p6.SetActive(true);
            p7.SetActive(false);
            p8.SetActive(false);
        }
        else if (targetTime <= 7.0f)
        {
            p1.SetActive(true);
            p2.SetActive(true);
            p3.SetActive(true);
            p4.SetActive(true);
            p5.SetActive(true);
            p6.SetActive(true);
            p7.SetActive(true);
            p8.SetActive(false);
        }
        else
        {
            p1.SetActive(true);
            p2.SetActive(true);
            p3.SetActive(true);
            p4.SetActive(true);
            p5.SetActive(true);
            p6.SetActive(true);
            p7.SetActive(true);
            p8.SetActive(true);
        }

        if(Input.GetKey(KeyCode.Space))
        {
            rb.linearVelocity = Vector3.up * 4f; 
        }
        // Clamp the bar's vertical position so it doesn't go past the top/bottom
        Vector3 pos = transform.localPosition;
        pos.y = Mathf.Clamp(pos.y, minY + 0.025f, maxY - 0.025f);
        transform.localPosition = pos;

                // --- FISH ICON MOVEMENT ---
        if (fishIcon != null)
        {
            Vector3 fishPos = fishIcon.transform.localPosition;

            // Move fish toward its target
            fishPos.y = Mathf.MoveTowards(fishPos.y, fishTargetY, fishSpeed * Time.deltaTime);
            fishIcon.transform.localPosition = fishPos;

            // If fish is close to its target, wait a bit then pick new target
            if (Mathf.Abs(fishPos.y - fishTargetY) < 0.01f)
            {
                fishWaitTime += Time.deltaTime;

                if (fishWaitTime >= fishMaxWaitTime)
                {
                    fishTargetY = Random.Range(minY, maxY);
                    fishWaitTime = 0f;
                    fishMaxWaitTime = Random.Range(0.5f, 1.5f); // varies how long the fish waits
                }
            }
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("fish"))
        {
            onFish = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("fish"))
        {
            onFish = false;
        }
    }
}
