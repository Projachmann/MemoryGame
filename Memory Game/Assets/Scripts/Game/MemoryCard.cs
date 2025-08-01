using UnityEngine;
public class MemoryCard : MonoBehaviour
{
    [SerializeField] private GameObject cardBack;
    [SerializeField] private SceneController sceneController;
    
    private int _id;
    public int Id
    {
        get { return _id; }
    }

    public void SetCard(int id, Sprite image)
    {
        _id = id;
        gameObject.GetComponent<SpriteRenderer>().sprite = image;
    }

    private void OnMouseDown()
    {
        if (cardBack.activeSelf && sceneController.canReveal)
        {
            cardBack.SetActive(false);
            sceneController.CardRevealed(this);
        }
    }

    public void UnReveal()
    {
        cardBack.SetActive(true);
    }
}
