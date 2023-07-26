using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Vector2 dirMove;
    [SerializeField] float changeDirTimmer;
    float changeDirCounter;
    private void Update()
    {
        if (changeDirCounter > changeDirTimmer)
        {
            dirMove *= -1;
            changeDirCounter = 0f;
        }
        changeDirCounter += Time.deltaTime;
        MoveInDirection(dirMove);
    }
    void MoveInDirection(Vector2 dir)
    {
        dir.Normalize();
        Vector2 currPos = transform.position;
        currPos.x += dir.x * speed * Time.deltaTime;
        currPos.y += dir.y * speed * Time.deltaTime;
        transform.position = currPos;

    }
}
