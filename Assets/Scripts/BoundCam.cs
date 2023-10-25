using UnityEngine;
using System.Collections;

public class BoundCam : MonoBehaviour
{
    [SerializeField] private float offset = 12.5f;

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= offset)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y ,offset, 100000f), transform.position.z);
        }
        else if(transform.position.y > offset + 5)
        {
            Vector2 dir = new Vector2(transform.localPosition.x, transform.localPosition.y);
            transform.Translate(-dir * Time.deltaTime);
        }
    }

    IEnumerator DefaultSize()
    {
        yield return new WaitForSeconds(2.5f);
        Camera.main.orthographicSize = 13f;
    }
}
