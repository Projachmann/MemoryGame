using UnityEngine;
public class MemoryCard : MonoBehaviour
{
    [SerializeField] private GameObject cardBack;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (cardBack.activeSelf)
        {
            cardBack.SetActive(false);
        }
    }
}
