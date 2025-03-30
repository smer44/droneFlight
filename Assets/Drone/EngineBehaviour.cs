using UnityEngine;

public class EngineBehaviour : MonoBehaviour
{
    public bool isThrust;
    public float defaultPower = 740f;
    public float addedPower = 200f;
    public float leanPowerChange = 100f;
    public float rotationSpeed = 0.1f;
    public float yawingChangeCoeficient = 0.1f;
    Rigidbody rigidbody;
    public bool clockwiseDirection;
    public bool lowerPower;
    public bool isFront;
    public bool isRight;
    float yawing;
    float frontLean;
    float rightLean;
    Vector3 thrust;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isThrust = false;
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();
        Debug.DrawLine(transform.position, transform.position + thrust, Color.red);
    }

    void UpdateInput(){

        if (Input.GetKeyDown(KeyCode.Space))
            {
                isThrust = true;
            }
        else{
            if (Input.GetKeyUp(KeyCode.Space)){
                isThrust = false;

            }

        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                lowerPower = true;
            }
        else{
            if (Input.GetKeyUp(KeyCode.LeftControl)){
                lowerPower = false;

            }

        }

        yawing = Input.GetAxis("Horizontal2");
        frontLean = Input.GetAxis("Vertical");
        rightLean = Input.GetAxis("Horizontal");

        frontLean = isFront ? - frontLean : frontLean;
        rightLean = isRight ? - rightLean : rightLean;


    }


    void FixedUpdate(){
        float currentPower = isThrust ? defaultPower + addedPower : defaultPower;
        currentPower = lowerPower ? currentPower - addedPower : currentPower;
        currentPower += frontLean * leanPowerChange;
        currentPower += rightLean * leanPowerChange;
        currentPower = currentPower * (1 + (clockwiseDirection ? yawingChangeCoeficient : -yawingChangeCoeficient ) *  yawing);

        //float currentPower = speed * (1 + (clockwiseDirection ? yawingChangeCoeficient : -yawingChangeCoeficient ) *  yawing);

        
        thrust = transform.up * currentPower * Time.deltaTime;
        
        Vector3 torque = (clockwiseDirection ? transform.up: -transform.up) * (currentPower * rotationSpeed * Time.deltaTime);
        rigidbody.AddForce(thrust, ForceMode.Acceleration);
        rigidbody.AddTorque(torque, ForceMode.Acceleration);
        


    }
}
