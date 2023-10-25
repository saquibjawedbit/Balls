using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject bluePoint;
    [SerializeField] private GameObject menuBar;
    [SerializeField] private GameObject[] point_2x;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text levelText;

    [SerializeField] private float density = 2f;
    [SerializeField] private float maxLength = 500f;

    [SerializeField] private int levelMultiple = 2;
    [SerializeField] private int levelScore = 10000;

    [SerializeField] private static int level;

    public static int point;
    public static bool gameStart = false;


    // Start is called before the first frame update
    void Start()
    {
        print("High Score " + PlayerPrefs.GetInt("HIGH_SCORE", 0));
        print("LEVEL " + PlayerPrefs.GetInt("LEVEL", 1));
        scoreText.text = "High Score: " + PlayerPrefs.GetInt("HIGH_SCORE", 0000);
        levelText.text = "LEVEL: " + PlayerPrefs.GetInt("LEVEL", 1);
        for (int i = 0; i < density * maxLength; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(-maxLength / 2, maxLength / 2), Random.Range(0, maxLength), 0);
            if(Random.Range(0,100) > 69)
            {
                if(Random.Range(0,100) > 69)
                {
                    Instantiate(point_2x[Random.Range(1, point_2x.Length)], randomPos, Quaternion.identity, transform);
                    continue;
                }
                Instantiate(point_2x[0], randomPos, Quaternion.identity, transform);
                continue;
            }
            Instantiate(bluePoint, randomPos, Quaternion.identity, transform);
        }
    }

    public void StartGame()
    {
        gameStart = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Ball>().enabled = true;
        menuBar.SetActive(false);
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (!gameStart) return;
          scoreText.text = "Score: " + point;

        if (point >= (level * levelMultiple * levelScore))
        {
            level++;
            PlayerPrefs.SetInt("LEVEL", level);
            print("NEW LEVEL " + level);
        }

    }

    public static void GameOver()
    {
        int highScore = PlayerPrefs.GetInt("HIGH_SCORE", 0);
        if (highScore < point)
        {
            PlayerPrefs.SetInt("HIGH_SCORE", point);

            print("High Score " + PlayerPrefs.GetInt("HIGH_SCORE"));
        }
    }

    public void ExitGame()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }
}
