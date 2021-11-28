using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathSkript : MonoBehaviour
{
    public GameObject[] pathNodes;

    int currentNode = 0;
    public bool walker = true;
  
    AIController aicontroller;

    private void Start()
    {
        aicontroller = GetComponent<AIController>();
    
    }

    // Update is called once per frame
    void Update()
    {
        if(!aicontroller.iHearYou && !aicontroller.iSawYou && !gameObject.GetComponentInChildren<alive>().dead && this.walker)
            walkToNode();
    }


    void walkToNode()
    {
        
        transform.position = Vector2.MoveTowards(transform.position, pathNodes[currentNode == pathNodes.Length ? 0 : currentNode].transform.position, 0.02f);
        transform.up = (Vector2)pathNodes[currentNode].transform.position - (Vector2)transform.position;
        //rb.AddForce(force);
        gameObject.GetComponentInChildren<alive>().child.GetChild(0).gameObject.SetActive(true);

        if (Vector2.Distance(transform.position, pathNodes[currentNode == pathNodes.Length? 0 : currentNode].transform.position) < 0.1f)
        currentNode++;
        if (currentNode == pathNodes.Length) currentNode = 0;
        
    }
}
