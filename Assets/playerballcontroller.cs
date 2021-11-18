using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System.Threading;

public class playerballcontroller : MonoBehaviour
{
    // Appears in the Inspector view from where you can set the speed
    public float speed;

    public float time;
    // Rigidbody variable to hold the player ball's rigidbody instance
    private Rigidbody rb;

    private Transform position;

    public Text TimerVr;

    public OVRCameraRig Camera;

    public PhotonView ballOwner;


    // Called before the first frame update
    void Start()
    {
        // Assigns the player ball's rigidbody instance to the variable
        rb = GetComponent<Rigidbody>();
        position = GetComponent<Transform>();



        if (!ballOwner.IsMine)
        {
            ballOwner.TransferOwnership(PhotonNetwork.LocalPlayer);
        }
    }

    // Called once per frame
    private void Update()
    {
        // The float variables, moveHorizontal and moveVertical, holds the value of the virtual axes, X and Z.


        OVRInput.Update();

        Vector3 rotation = Camera.centerEyeAnchor.rotation.eulerAngles;


        rotation.x = (float)Math.Sin((rotation.x - 25) * Mathf.Deg2Rad);
        rotation.z = (float)Math.Sin(rotation.z * Mathf.Deg2Rad);
        float tempValue = rotation.x;
        rotation.x = -rotation.z;
        rotation.z = tempValue;
        rotation.y = 0;


        rb.AddForce(rotation * speed * Time.deltaTime);

        if (position.position.y < -20)
        {
            TimerVr.text = "You lost";
        }

        if (time == 117)
        {
            TimerVr.fontSize = 48;
        }

        else if (time < 120)
        {

            time += Time.deltaTime;

            TimerVr.text = "Timer : " + String.Format("{0:0.00}", time);
        }

        else if (time > 120)
        {
            TimerVr.fontSize = 24;
            TimerVr.text = "You won";
        }


        float testx = Input.GetAxis("Horizontal");
        float testy = Input.GetAxis("Vertical");

        Vector3 test = new Vector3(testx, testy);

        rb.AddForce(test * speed * Time.deltaTime);
    }

}