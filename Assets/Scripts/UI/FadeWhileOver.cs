using UnityEngine;
using UnityEngine.UI;

public class FadeWhileOver : MonoBehaviour
{
    Collider2D col;
    Image image;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
        image = GetComponent<Image>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            image.color = new Color(1, 1, 1, 0.4f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            image.color = new Color(1, 1, 1, 1);
        }
    }
}
