using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Cinematic : MonoBehaviour
{
    [SerializeField] private VideoClip clip;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("CinematicMethod", (float)clip.length);
    }

    void CinematicMethod()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
