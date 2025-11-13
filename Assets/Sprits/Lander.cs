using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class Lander : MonoBehaviour
{
    private Rigidbody2D lander_rb;
    private float force = 700f;
    private float turnspeed = 100f;
    
    private void Awake()
    {
        lander_rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (Keyboard.current.upArrowKey.isPressed)
        {
            lander_rb.AddForce(force * transform.up * Time.deltaTime);
        }
        if (Keyboard.current.rightArrowKey.isPressed)
        {
             lander_rb.AddTorque(-turnspeed * Time.deltaTime);
        }
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            lander_rb.AddTorque(+turnspeed * Time.deltaTime);
        }
    }
    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out LandingPad landingPad))
        {
            float soft_landing_speed = 4f;
            float relative_velocity = collision.relativeVelocity.magnitude;

            if (relative_velocity > soft_landing_speed)
            {
                Debug.Log("You Crashed!");
                return;
            }
            //vector dot product to get landing angle

            float dot_vector = Vector2.Dot(Vector2.up, transform.up);
            float min_dot_vector = 0.9f;
            if (dot_vector < min_dot_vector)
            {
                Debug.Log("Crashed!!");
                return;
            }
            Debug.Log("Landed Safely");

            float maxscore_landing_angle = 100f;
            float score_normalize_dot_vector = 10f;

            /*score using angle checking
                - using dot_vector - 1f and normalize to get the percentage of angle its off by
                - multiplying that percentage to find score after deduction
            */
            float landing_angle_score = maxscore_landing_angle - Mathf.Abs(dot_vector - 1f) * score_normalize_dot_vector * maxscore_landing_angle;

            /*
              Score using impact checking
                - max score you can get here is 40
            */
            float maxscore_landing_speed = 100f;
            float landing_speed_score = (soft_landing_speed - relative_velocity) * maxscore_landing_speed;

            Debug.Log("Score by angel: " + landing_angle_score);
            Debug.Log("Score by speed: " + landing_speed_score);
            return;
        }
        Debug.Log("Crashed outside landing pad");
    }
}
