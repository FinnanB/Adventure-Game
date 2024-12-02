using UnityEngine;
using System.Collections;

public class Hazard : MonoBehaviour
{
    public Transform spawn;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(PlayerMove(other.gameObject));
        }
    }

    IEnumerator PlayerMove(GameObject player)
    {
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = spawn.position;
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<CharacterController>().enabled = true;
        yield return null;
    }
}
