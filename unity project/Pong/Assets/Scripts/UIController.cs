using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject buttonContainer;
    public GameObject joinButton;

    public void UpdateGameList(List<ClassLibrary.PongGame> pongGames)
    {
        foreach (Transform child in buttonContainer.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (ClassLibrary.PongGame pg in pongGames)
        {
            GameObject jb = Instantiate(joinButton, buttonContainer.transform.position, Quaternion.identity, buttonContainer.transform);
            Text txt = jb.transform.Find("Text").GetComponent<Text>();
            txt.text = pg.GameName;
        }
    }
}
