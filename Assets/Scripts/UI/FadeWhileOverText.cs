using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadeWhileOverText : MonoBehaviour
{
    Collider2D col;
    TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
        text = GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            text.color = new Color(1, 1, 1, 0.4f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            text.color = new Color(1, 1, 1, 1);
        }
    }
}
