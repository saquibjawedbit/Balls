using UnityEngine;

public class PlayerContoller : MonoBehaviour
{

   

    [SerializeField] private Rigidbody rb;

    [SerializeField] private float height = 2.5f;
    [SerializeField] private float speed = 5f;

    [SerializeField] private SerialField field;
    

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //Jump
            rb.velocity += new Vector3(0, Mathf.Sqrt(-2 * Physics.gravity.y * height) , 0);
        }

        if(field.TouchDist.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }

        rb.AddForce(transform.forward * speed * 10* Time.deltaTime, ForceMode.Acceleration);
    }

    

}
