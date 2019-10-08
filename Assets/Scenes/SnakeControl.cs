using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum SnakeDirection {
    Up, Down, Left, Right
}

public class SnakeControl : MonoBehaviour
{
    private SnakeModel m_Model;
    public SnakeBlock blockReference;
    private SnakeBlock m_Fruit;
    private float m_TickTime = 1;

    private SnakeDirection m_Direction = SnakeDirection.Right;
    
    public void Start()
    {
        // find the model
        m_Model = FindObjectOfType<SnakeModel>();
        
        // create the 3 initial blocks
        var obj = Instantiate(blockReference, Vector3.zero, Quaternion.identity);
        m_Model.SnakeBlocks.Add(obj);
        
        obj = Instantiate(blockReference, 
            new Vector3(-1,0,0), Quaternion.identity);
        m_Model.SnakeBlocks.Add(obj);
        
        obj = Instantiate(blockReference, 
            new Vector3(-2,0,0), Quaternion.identity);
        m_Model.SnakeBlocks.Add(obj);
        
        // fruit
        m_Fruit = Instantiate(
            blockReference,
            new Vector3(
                UnityEngine.Random.Range(-10,10), 
                UnityEngine.Random.Range(-10,10),
                0), Quaternion.identity);

        StartCoroutine(Move());
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

    IEnumerator Move()
    {
        while (true)
        {
            // wait for tick
            yield return new WaitForSeconds(m_TickTime);

            // move body
            for (int i = m_Model.SnakeBlocks.Count - 1;
                i >= 1; i--)
                m_Model.SnakeBlocks[i].transform.position = 
                    m_Model.SnakeBlocks[i - 1].transform.position;
            // move head
            var newpos =
                SnakeTranslate(
                m_Model.SnakeBlocks[0].transform.position,
                m_Direction);
            // todo: improve collision check
            // if distance between fruit and head <1
            if (Vector3.Distance(newpos,
                    m_Fruit.transform.position) < 1)
            { // eat it!
                m_Model.SnakeBlocks.Insert(0, m_Fruit);
                m_Fruit = Instantiate(
                    blockReference,
                    new Vector3(
                        UnityEngine.Random.Range(-10,10), 
                        UnityEngine.Random.Range(-10,10),
                        0), Quaternion.identity);
            }
            else // move
                m_Model.SnakeBlocks[0].transform.position = newpos;
        }
    }
    private void UpdateTickTime()
    {
        m_TickTime = (m_TickTime -0.1f)*0.9f + 0.1f;
    }
}
