using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaController : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody pizzabody;

    // Update is called once per frame
    void Update()
    {
        Quaternion current = pizzabody.rotation;
        pizzabody.rotation = Quaternion.Slerp(current, Quaternion.identity, 0.01f);
    }
}
