using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{

    private float projectile_velocity;
    [SerializeField] private Rigidbody2D rb2d;
    private GameObject myTank;
    private TankManager myTankManager;

    void Start()
    {
        rb2d.velocity = transform.up * projectile_velocity;
        // Destroy shot if it exists for longer than 5 seconds, could represent a range mechanic
        // although the levels aren't really big enough for this to factor into gameplay
        Destroy(gameObject, 5f);
    }

    // Retrieve message regarding shot origin
    public void Retriever(GameObject tankInput)
    {
        myTank = tankInput;
        myTankManager = myTank.GetComponent<TankManager>();
        projectile_velocity = myTankManager.myShotSpeed;
    }

    // Keeping the shot speed value updated since it is altered by powerups
    private void Update()
    {
        projectile_velocity = myTankManager.myShotSpeed;
    }

    // Destroy shot if it hits object
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // If the shot hits another tank and it isn't the tank the shot was fired from, score is risen and tank hit trigger occurs
        if (hitInfo.gameObject != myTank && hitInfo.CompareTag("Tank") && hitInfo.gameObject.GetComponent<TankManager>().invun == false)
        {
            myTankManager.myScore += 15;
            GameEvents.current.TankHitTrigger();
            if (!(myTank.name == "TankEnemy"))
            {
                AchievementManager.achievement01Count += 1;
            }
        }
        // The shot passes through powerups
        if (!hitInfo.CompareTag("PowerUp"))
        {
            Destroy(gameObject);
        }
    }

}
