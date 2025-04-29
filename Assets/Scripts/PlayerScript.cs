using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Animator playerAnim;
    public bool isFishing;
    public bool poleBack;
    public bool throwBobber;
    public Transform fishingPoint;
    public GameObject bobber;

    public float targetTime = 0.0f;
    public float savedTargetTime;
    public float extraBobberDistance;

    public GameObject fishGame;

    public float timeTillCatch = 0.0f;
    public bool winnerAnim;

    public GameObject water;
    public GameObject gameController;       
    public GameObject fishItemPrefab;
    private InvController invController;   
    public Bobber bobberScript;  

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isFishing = false;
        fishGame.SetActive(false);
        throwBobber = false;
        targetTime = 0.0f;
        savedTargetTime = 0.0f;
        extraBobberDistance = 0.0f;
        invController = gameController.GetComponent<InvController>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isFishing == false && winnerAnim ==  false)
        {
            poleBack = true;
        }
        if(isFishing == true)
        {
            timeTillCatch += Time.deltaTime;
            if(timeTillCatch >= 3)
            {
                Bobber bobberScript = FindObjectOfType<Bobber>();

                if(bobberScript != null && bobberScript.isInWater)
                {
                fishGame.SetActive(true);
                }
                else 
                {
                    Debug.Log("Bobber is not in water");
                    CancelFishing();
                }
            }
        }

        if(Input.GetKeyUp(KeyCode.Space) && isFishing == false && winnerAnim == false)
        {
            poleBack = false;
            isFishing = true;
            throwBobber = true;
            if(targetTime >= 3)
            {
                extraBobberDistance += 3;
            } else
            {
                extraBobberDistance += targetTime;
            }
        }

        Vector3 temp = new Vector3(extraBobberDistance, 0, 0);
        fishingPoint.transform.position += temp;

        if(poleBack == true)
        {
            playerAnim.Play("playerSwingBack");
            savedTargetTime = targetTime;
            targetTime += Time.deltaTime;
        }

        if(isFishing == true)
        {
            if(throwBobber == true)
            {
                Instantiate(bobber, fishingPoint.position, fishingPoint.rotation, transform);
                fishingPoint.transform.position -= temp;
                throwBobber = false;
                targetTime = 0.0f;
                savedTargetTime = 0.0f;
                extraBobberDistance = 0.0f;
                
            }
            playerAnim.Play("playerFishing");
        }

        if(Input.GetKeyDown(KeyCode.P) && timeTillCatch <= 3)
        {
            playerAnim.Play("playerStill");
            poleBack = false;
            throwBobber = false;
            isFishing = false;
            timeTillCatch = 0;
        }
    }

    public void fishGameWon()
    {
        Debug.Log("Win!");
        playerAnim.Play("playerWonFish");
        fishGame.SetActive(false);
        playerAnim.Play("playerStill");
        poleBack = false;
        throwBobber = false;
        isFishing = false;
        timeTillCatch = 0;

        GameObject caughtFish = invController.GetRandomFish();  
        if(caughtFish != null)
        {
            invController.AddItem(caughtFish);
        }
        else
        {
            Debug.LogWarning("No fish found");
        }

    }
    public void fishGameLossed()
    {
        Debug.Log("Loss!");
        playerAnim.Play("playerStill");
        fishGame.SetActive(false);
        playerAnim.Play("playerStill");
        poleBack = false;
        throwBobber = false;
        isFishing = false;
        timeTillCatch = 0;
    }

    private void CancelFishing()
    {
        playerAnim.Play("playerStill");
        poleBack = false;
        throwBobber = false;
        isFishing = false;
        timeTillCatch = 0;

        // Destroy the bobber if it exists
        Bobber bobberScript = FindObjectOfType<Bobber>();
        if (bobberScript != null)
        {
            Destroy(bobberScript.gameObject);
        }
    }


}
