using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSpecificScene : MonoBehaviour
{
    public string sceneName;
    private Animator fadeSytem;
    public AudioClip sound;
    public GameObject winMenuUI;

    void Awake()
    {
        fadeSytem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            AudioManager.instance.PlayClipAt(sound, transform.position);
            LoadAndSaveData.instance.SaveData();
            StartCoroutine(loadNextScene());
        }
    }

    public IEnumerator loadNextScene()
    {
        fadeSytem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        if(sceneName == "Win")
        {
            GameValues.instance.Paused("Win");
        }
        else
            SceneManager.LoadScene(sceneName);
    }
}
