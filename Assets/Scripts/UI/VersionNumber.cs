using UnityEngine;
using UnityEngine.UI;

public class VersionNumber : MonoBehaviour
{
    public Text versionText;

    private void Start()
    {
        versionText.text = "v" + Application.version;
    }
}
