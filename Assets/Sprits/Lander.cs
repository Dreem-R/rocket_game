using UnityEngine;
using UnityEngine.InputSystem;

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
}
