using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlenty : MonoBehaviour
{
    private bool mustBeLunched = false;
    public int numberMax = 7;
    public int CoordXTopL = 1;
    public int CoordYTopL = 10;
    public int CoordXBotR = 9;
    public int CoordYBotR = 6;
    public GameObject item;

    public static FallPlenty instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de FallPlenty dans la scène");
            return;
        }
        instance = this;
    }

    public void Start()
    {
        int random = Random.Range(1, numberMax);

        for (int i = 0; i < random; ++i)
        {
            int x = Random.Range(CoordXTopL, CoordXBotR);
            int y = Random.Range(CoordYTopL, CoordYBotR);

            Vector3 pos = transform.position + new Vector3(x, y, 0);
            Instantiate(item, pos, Quaternion.identity, transform);
        }
    }

    public bool getStatut() {
        return mustBeLunched;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Player")
        {
            mustBeLunched = true;
        }
    }
}
