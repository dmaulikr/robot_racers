using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use
        private bool allow_jump = true;

        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
        }

        private void FixedUpdate()
        {
            // pass the input to the car!
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            //Can only jump if haven't jumped in last 2 seconds...
            bool jump = false;
            if (allow_jump) {
                jump = CrossPlatformInputManager.GetButtonDown("Jump");
                if (jump) {
                    allow_jump = false;
                    Invoke("TurnOnJump", 2f);
                }
            }
            //print(h + " " + v + " " + jump);
            m_Car.Move(h, v, v, jump);
        }

        private void TurnOnJump() {
            allow_jump = true;
        }
    }
}
