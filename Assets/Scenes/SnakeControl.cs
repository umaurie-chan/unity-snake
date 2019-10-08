using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeControl : MonoBehaviour
{
    private SnakeModel m_Model;
    public SnakeBlock BlockReference;

    public void Start()
    {
        // find the model
        m_Model = FindObjectOfType<SnakeModel>();
        
        // create the 3 initial blocks
        SnakeBlock obj = Instantiate(BlockReference, Vector3.zero, Quaternion.identity);
        m_Model.SnakeBlocks.Add(obj);
        
        obj = Instantiate(BlockReference, 
            new Vector3(-1,0,0), Quaternion.identity);
        m_Model.SnakeBlocks.Add(obj);
        
        obj = Instantiate(BlockReference, 
            new Vector3(-2,0,0), Quaternion.identity);
        m_Model.SnakeBlocks.Add(obj);
    }
}
