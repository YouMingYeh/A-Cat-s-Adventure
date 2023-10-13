using Platformer.Core;
using Platformer.Model;
using UnityEngine;

public class LaserEmitter : MonoBehaviour
{
    public Transform laserPrefab; // 雷射的Prefab
    public float laserSpeed = 0.1f; // 雷射移动速度
    public float secondsPerFire = 1f; // 控制发射频率
    private float lastFireTime; // 上次发射的时间

    PlatformerModel model;

    private void Start()
    {
        model = Simulation.GetModel<PlatformerModel>();
    }
    private void Update()
    {
        // Check if enough time has passed to fire again
        if (Time.time - lastFireTime >= secondsPerFire)
        {
            FireLaser();
            lastFireTime = Time.time;
        }
    }

    void FireLaser()
    {
        if (!model.player.controlEnabled) return;
        Transform laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);

        // Set the velocity of the laser to make it continuous
        Rigidbody2D rb = laser.GetComponent<Rigidbody2D>();
        rb.isKinematic = true; // Set to Kinematic for continuous movement
        rb.velocity = new Vector2(-1.0f, 0.0f) * laserSpeed;

        // Automatically destroy the laser after 5 seconds
        Destroy(laser.gameObject, 3f);
    }


}
