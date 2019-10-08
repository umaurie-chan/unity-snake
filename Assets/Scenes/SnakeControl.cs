using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SnakeDirection {
    Up, Down, Left, Right
}

public class SnakeControl : MonoBehaviour
{
    private SnakeModel m_Model;
    public SnakeBlock BlockReference;

    private SnakeDirection m_Direction = SnakeDirection.Right;
    
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

        StartCoroutine(move());
    }

    Vector3 SnakeTranslate(Vector3 elem, SnakeDirection direction)
    {
        switch (direction)
        {
            case SnakeDirection.Down:
                return elem + Vector3.down;
            case SnakeDirection.Left:
                return elem + Vector3.left;
            case SnakeDirection.Right:
                return elem + Vector3.right;
            default: 
                return elem + Vector3.up;
        }
    }

    public void Update()
    {
        // deal with input
        if (Input.GetKey(KeyCode.UpArrow))
            m_Direction = SnakeDirection.Up;
        if (Input.GetKey(KeyCode.DownArrow))
            m_Direction = SnakeDirection.Down;
        if (Input.GetKey(KeyCode.LeftArrow))
            m_Direction = SnakeDirection.Left;
        if (Input.GetKey(KeyCode.RightArrow))
            m_Direction = SnakeDirection.Right;
    }

    IEnumerator move()
    {
        while (true)
        {
            // wait for tick
            yield return new WaitForSeconds(1);

            // move body
            for (int i = m_Model.SnakeBlocks.Count - 1;
                i >= 1; i--)
                m_Model.SnakeBlocks[i].transform.position = 
                    m_Model.SnakeBlocks[i - 1].transform.position;
            // move head
            m_Model.SnakeBlocks[0].transform.position = 
                SnakeTranslate(
                m_Model.SnakeBlocks[0].transform.position,
                m_Direction);
        }
    }
}
