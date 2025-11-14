using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class Lander : MonoBehaviour
{
    private const float GRAVITY_NORMAL = 0.7f;

    public static Lander Instance { get; private set; }

    public event EventHandler OnUpForce;
    public event EventHandler OnLeftForce;
    public event EventHandler OnRightForce;
    public event EventHandler OnBeforeForce;
    public event EventHandler OnCoinPickup;
    public event EventHandler<OnStateChangeEventAgrs> OnStateChange;
    public class OnStateChangeEventAgrs : EventArgs
    {
        public States new_state;
    }
    public event EventHandler<OnLandedEventArgs> OnLanded;
    public class OnLandedEventArgs : EventArgs
    {
        public LandingType landingType;
        public int score;
        public float landingAngle;
        public float landingSpeed;
        public float ScoreMultiplier;
    }

    public enum LandingType
    {
        Success,
        WrongLandingArea,
        TooSteepAngel,
        TooFastLanding,
    }

    public enum States
    {
        WaitingToStart,
        Normal,
        GameOver,
    }

    private Rigidbody2D lander_rb;
    private float force = 700f;
    private float turnspeed = 100f;
    private float FuelAmount = 10f;
    private float MaxFuelAmount;

    private States state;

    private void Awake()
    {
        Instance = this;
        state = States.WaitingToStart;
        lander_rb = GetComponent<Rigidbody2D>();
        lander_rb.gravityScale = 0f;
        MaxFuelAmount = FuelAmount;
    }

    private void FixedUpdate()
    {
        OnBeforeForce?.Invoke(this, EventArgs.Empty);

        switch (state)
        {
            default:
            case States.WaitingToStart:
                if (Keyboard.current.upArrowKey.isPressed ||
                    Keyboard.current.leftArrowKey.isPressed ||
                    Keyboard.current.rightArrowKey.isPressed)
                {
                    lander_rb.gravityScale = GRAVITY_NORMAL;
                    state = States.Normal;
                    SetState(States.Normal);
                }
                break;

            case States.Normal:
                if (FuelAmount <= 0f)
                {
                    return;
                }

                if (Keyboard.current.upArrowKey.isPressed ||
                    Keyboard.current.leftArrowKey.isPressed ||
                    Keyboard.current.rightArrowKey.isPressed)
                {
                    ConsumeFuel();
                }
                //Accelerate forward
                if (Keyboard.current.upArrowKey.isPressed)
                {
                    lander_rb.AddForce(force * transform.up * Time.deltaTime);

                    OnUpForce?.Invoke(this, EventArgs.Empty);
                }

                //Right Turn
                if (Keyboard.current.rightArrowKey.isPressed)
                {
                    lander_rb.AddTorque(-turnspeed * Time.deltaTime);
                    OnLeftForce?.Invoke(this, EventArgs.Empty);
                }
                //Left Turn
                if (Keyboard.current.leftArrowKey.isPressed)
                {
                    lander_rb.AddTorque(+turnspeed * Time.deltaTime);
                    OnRightForce?.Invoke(this, EventArgs.Empty);
                }
                break;
            case States.GameOver:
                break;
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
            int Score_Multipier = landingPad.get_multipier();
            float soft_landing_speed = 4f;
            float relative_velocity = collision.relativeVelocity.magnitude;

            if (relative_velocity > soft_landing_speed)
            {
                OnLanded?.Invoke(this, new OnLandedEventArgs
                {
                    landingType = LandingType.TooSteepAngel,
                    score = 0,
                    landingSpeed = relative_velocity,
                    landingAngle = 0f,
                    ScoreMultiplier = 0
                });
                SetState(States.GameOver);
                Debug.Log("Speed Crashed!");
                return;
            }
            //vector dot product to get landing angle

            float dot_vector = Vector2.Dot(Vector2.up, transform.up);
            float min_dot_vector = 0.9f;
            if (dot_vector < min_dot_vector)
            {
                OnLanded?.Invoke(this, new OnLandedEventArgs
                {
                    landingType = LandingType.TooSteepAngel,
                    score = 0,
                    landingSpeed = relative_velocity,
                    landingAngle = dot_vector,
                    ScoreMultiplier = 0
                });
                SetState(States.GameOver);
                Debug.Log("Angle Crash!");
                return;
            }
            Debug.Log("Landed Safely");

            float maxscore_landing_angle = 100f;
            float score_normalize_dot_vector = 10f;

            /*score using angle checking
                - using dot_vector - 1f and normalize to get the percentage of angle its off by
                - multiplying that percentage to find score after deduction
                - max score you can get here is 100
            */
            float landing_angle_score = maxscore_landing_angle - Mathf.Abs(dot_vector - 1f) * score_normalize_dot_vector * maxscore_landing_angle;

            /*
              Score using impact checking
                - max score you can get here is 400
            */
            float maxscore_landing_speed = 100f;
            float landing_speed_score = (soft_landing_speed - relative_velocity) * maxscore_landing_speed;

            int total_score = Mathf.RoundToInt((landing_angle_score + landing_speed_score) * Score_Multipier);
            SetState(States.GameOver);
            OnLanded?.Invoke(this,new OnLandedEventArgs {
                landingType = LandingType.Success,
                score = total_score,
                landingSpeed = relative_velocity,
                landingAngle = dot_vector,
                ScoreMultiplier = Score_Multipier
            });
            return;
        }
        SetState(States.GameOver);
        OnLanded?.Invoke(this, new OnLandedEventArgs
        {
            landingType = LandingType.WrongLandingArea,
            score = 0,
            landingSpeed = 0f,
            landingAngle = 0f,
            ScoreMultiplier = 0
        });
        Debug.Log("Crashed outside landing pad");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out FuelPickup fuelPickup))
        {
            float fuel_increment = 10f;
            FuelAmount += fuel_increment;
            if(FuelAmount > MaxFuelAmount)
            {
                FuelAmount = MaxFuelAmount;
            }
            fuelPickup.DestroySelf();
        }
        else if (collision.gameObject.TryGetComponent(out CoinPickup coinPickup))
        {
            OnCoinPickup?.Invoke(this, EventArgs.Empty);
            coinPickup.DestroySelf();
        }
    }

    private void SetState(States NewState)
    {
        this.state = NewState;
        OnStateChange?.Invoke(this, new OnStateChangeEventAgrs
        {
            new_state = NewState
        });
    }

    public float Get_Normalized_FuelAmount()
    {
        return FuelAmount/MaxFuelAmount;
    }
    public float GetSpeedX()
    {
        return lander_rb.linearVelocityX;
    }

    public float GetSpeedY()
    {
        return lander_rb.linearVelocityY;
    }

    public float GetFuel()
    {
        return FuelAmount;
    }

    private void ConsumeFuel()
    {
        float fuelconsumptionAmount = 1f;
        FuelAmount -= fuelconsumptionAmount * Time.deltaTime;
    }
}
