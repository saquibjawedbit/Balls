using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;

    public float forceMultiplier = 10f;

    public Vector2 maxForce;

    [SerializeField] private float forceEffect = 10f;
    [SerializeField] private Text effectText;
    [SerializeField] private GameObject particle;
    [SerializeField] private GameObject redParticle;
    [SerializeField] private GameObject whiteParticle;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private Slider slider;

    private Vector3 dir;

    [HideInInspector] public Vector3 inital, final;
    private int deltaScore = 100;
    private int scoreEffect = 1;
    [SerializeField] private float scoreBooster = 1.5f;
    private float scoreTime;
    private float fEffect;

    // Start is called before the first frame update
    void Start()
    {
        fEffect = forceEffect;
        rb = GetComponent<Rigidbody>();
        this.enabled = false;
    }

    private void Update()
    {
        if (forceEffect <= 0) return;
        if(Input.GetMouseButtonDown(0))
        {
            inital = Input.mousePosition;
            Time.timeScale = .2f;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Shoot();
        }
        scoreTime -= Time.deltaTime;
        //forceEffect -= Time.deltaTime * .01f * forceEffect;

    }

    private void Shoot()
    {
        final = Input.mousePosition;
        dir = new Vector3(Mathf.Clamp((final.x - inital.x) * forceMultiplier * -0.01f, -maxForce.x, maxForce.x), Mathf.Clamp((final.y - inital.y) * -0.01f * forceMultiplier, -maxForce.y, maxForce.y), 0);
        rb.velocity = dir * forceMultiplier * 0.1f;
        forceEffect -= fEffect * .3f;
        Time.timeScale = 1;
    }

    private void LateUpdate()
    {
        //if (!GameManager.gameStart) return;

        if (scoreTime <= 0) scoreEffect = 1;

        effectText.text = scoreEffect + "X";
        slider.value = forceEffect/fEffect;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Point"))
        {
            GameManager.point += deltaScore * scoreEffect;
            rb.AddForce(new Vector3(Random.Range(0, 1), Random.Range(1, 2), 0) * Mathf.Clamp(transform.position.y, 0, 8.5f) * 2, ForceMode.Impulse);
            if(scoreEffect <= 8) scoreEffect *= 2;
            Destroy(Instantiate(particle, transform.position, transform.rotation), 2f);
            Destroy(collision.gameObject);
            forceEffect += fEffect * 0.2f;
        }
        else if(collision.collider.CompareTag("Lava"))
        {
            gameOver.SetActive(true);
            this.enabled = false;
            GameManager.GameOver();
        }
        else if(collision.collider.CompareTag("Point2"))
        {
            if (scoreEffect <= 8) scoreEffect *= 2;
            deltaScore *= 2;
            Destroy(Instantiate(redParticle, transform.position, transform.rotation), 2f);
            Destroy(collision.gameObject);
            forceEffect += fEffect * 0.2f;
            StartCoroutine(InitializeScore());
        }
        else if(collision.collider.CompareTag("Point3"))
        {
            GameManager.point += (deltaScore + 900) * scoreEffect;
            if (scoreEffect <= 8) scoreEffect *= 2;
            Destroy(Instantiate(particle, transform.position, transform.rotation), 2f);
            Destroy(collision.gameObject);
            forceEffect += fEffect * 0.3f;
        }
        else if (collision.collider.CompareTag("Point4"))
        {
            GameManager.point += (deltaScore + 100) * scoreEffect;
            if (scoreEffect <= 8) scoreEffect *= 2;
            Destroy(Instantiate(whiteParticle, transform.position, transform.rotation), 2f);
            Destroy(collision.gameObject);
            forceEffect = 0;
        }
        else
        {
            forceEffect = fEffect;
            return;
        }

        scoreTime = scoreBooster;
    }

    IEnumerator InitializeScore()
    {
        yield return new WaitForSeconds(5f);
        deltaScore /= 2;
    }
}
