using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LineChecker : MonoBehaviour {
    public bool checkline = false;
    float timer = 0;
    int moveDownline;
    public GameObject explosion;
    public GameObject score;
    // Use this for initialization
    private void Update()
    {
        if (checkline)
        { 
            CheckforLine();
            checkline = false;
        }
    }
    void CheckforLine()
    {
        moveDownline = 0;
        for (int i = -1; i<=22; i++)
        {
            if (Physics.OverlapBox(new Vector3(0, -0.1050001f + (i),0), new Vector3(5.885f, 0.1f, 0.4f), transform.rotation, ~0, QueryTriggerInteraction.Ignore).Length >= 12)
            {
                Collider[] g = Physics.OverlapBox(new Vector3(0, -0.1050001f + (i), 0), new Vector3(5.885f, 0.4f, 0.4f), transform.rotation,~0,QueryTriggerInteraction.Ignore);
                int counter = 0;
                for (int j = 0; j< g.Length; j++)
                {
                    if (g[j].gameObject.tag == "Landed" && !g[j].gameObject.GetComponent<BoxCollider>().isTrigger)
                    {
                        counter++;
                    }
                }
                if (counter >= 12)
                {
                    for (int j = 0; j < g.Length; j++)
                    {
                        if (g[j].gameObject.tag == "Landed")
                        {
                            Instantiate(explosion, g[j].transform.position, Quaternion.Euler(-90,0,0));
                            Destroy(g[j].gameObject);
                            moveDownline = i;
                            gameObject.GetComponent<AudioSource>().Play();
                        }
                    }
                    for (int k = moveDownline; k <= 22; k++)
                    {
                        Collider[] m = Physics.OverlapBox(new Vector3(0, -0.1050001f + (k), 0), new Vector3(5.885f, 0.4f, 0.4f), transform.rotation, ~0, QueryTriggerInteraction.Ignore);
                        for (int l = 0; l < m.Length; l++)
                        {
                            if (m[l].gameObject.tag == "Landed")
                            {
                                m[l].gameObject.transform.position = new Vector3(m[l].gameObject.transform.position.x, m[l].gameObject.transform.position.y - 1f, m[l].gameObject.transform.position.z);
                            }
                        }
                    }
                    i = i - 1;
                    score.GetComponent<ScoreController>().lines += 1;
                }
            }
        }
        if (Physics.CheckBox(new Vector3(0,23, 0), new Vector3(5.885f, 0.1f, 0.4f), transform.rotation, ~0, QueryTriggerInteraction.Ignore))
        {
            Collider[] g = Physics.OverlapBox(new Vector3(0, 23, 0), new Vector3(5.885f, 0.4f, 0.4f), transform.rotation, ~0, QueryTriggerInteraction.Ignore);
            for (int j = 0; j < g.Length; j++)
            {
                if (g[j].gameObject.tag == "Landed" && !g[j].gameObject.GetComponent<BoxCollider>().isTrigger)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }
    }
}
