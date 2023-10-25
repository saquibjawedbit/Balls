using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerBar : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.position = new Vector3(250, 621, 0);
        }
    }
}
