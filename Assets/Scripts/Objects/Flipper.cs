using UnityEngine;

public class Flipper : MonoBehaviour
{
    #region// ENUM -----------------------------------------------------------------------------------------------------------------------
    public enum FlipperSide
    {
        LEFT,
        RIGHT
    }
    #endregion

    [SerializeField] private FlipperSide side;
    private HingeJoint2D hinge;
    private JointMotor2D motor;

    [Header("// POWER -----------------------------------------------------------------------------------------")]
    [SerializeField] private float motorSpeed;
    [SerializeField] private float motorForce;


    // GAME //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    void Awake()
    {
        hinge = GetComponent<HingeJoint2D>();

        motor = hinge.motor;
        motor.maxMotorTorque = motorForce;
    }

    void Update()
    {
        bool inputPressed = false;

        switch (side)
        {
            case FlipperSide.LEFT:
                inputPressed = Input.GetMouseButton(0);
                break;

            case FlipperSide.RIGHT:
                inputPressed = Input.GetMouseButton(1);
                break;
        }

        if (inputPressed) ActivateFlipper();
        else ReleaseFlipper();
    }



    // FUNCTIONS //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-

    void ActivateFlipper()
    {
        if (side == FlipperSide.LEFT)
        { motor.motorSpeed = -motorSpeed; }
        else
        { motor.motorSpeed = motorSpeed; }

        hinge.motor = motor;
    }

    void ReleaseFlipper()
    {
        if (side == FlipperSide.LEFT)
        { motor.motorSpeed = motorSpeed; }
        else
        { motor.motorSpeed = -motorSpeed; }

        hinge.motor = motor;
    }
}