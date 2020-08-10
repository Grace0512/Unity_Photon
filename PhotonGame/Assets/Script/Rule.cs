using UnityEngine;
using UnityEngine.UI;

public class Rule : MonoBehaviour
{

   
    public GameObject rule;
    public bool open;
    public void printRule()
    {
        open = !open;
        rule.SetActive(open);

    }
}
