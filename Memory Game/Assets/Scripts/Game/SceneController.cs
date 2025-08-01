using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] MemoryCard originalCard;
    [SerializeField] Sprite[] images;
    [SerializeField] TMP_Text scoreText;

    private MemoryCard firstRevealed;
    private MemoryCard secondRevealed;

    public const int gridRows = 2;
    public const int gridColumns = 4;
    public const float offsetX = 2f;
    public const float offsetY = 2.5f;

    private int score = 0;

    public bool canReveal
    {
        get { return secondRevealed == null; }
    }

    public void CardRevealed(MemoryCard card)
    {
        if (firstRevealed == null)
        {
            firstRevealed = card;
        }
        else
        {
            secondRevealed = card;
            StartCoroutine(CheckMatch());
        }
    }

    void Start()
    {
        Vector3 startPos = originalCard.transform.position;

        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3 };
        numbers = ShuffleArray(numbers);

        for (int i = 0; i < gridColumns; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                MemoryCard card;
                if (i == 0 &&  j == 0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard) as MemoryCard;
                }

                int index = j * gridColumns + i;
                int id = numbers[index];
                card.SetCard(id, images[id]);

                float posX = (offsetX * i) + startPos.x;
                float posY = -(offsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }

    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i <  newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(0, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }

        return newArray;
    }

    private IEnumerator CheckMatch()
    {
        if (firstRevealed.Id == secondRevealed.Id)
        {
            score++;
            scoreText.text = "Score: " + score;
        }
        else
        {
            yield return new WaitForSeconds(0.5f);

            firstRevealed.UnReveal();
            secondRevealed.UnReveal();
        }

        firstRevealed = null;
        secondRevealed = null;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
}
