using UnityEngine;

public class Hazard : MonoBehaviour
{

    public Transform spawn;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.transform.position = spawn.position;
        }
    }
}
