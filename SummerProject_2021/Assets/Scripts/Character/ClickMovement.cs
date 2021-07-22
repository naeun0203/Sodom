//*******************************
// Editor:CHG
// LAST EDITED DATE : 2021.07.19
// Script Purpose : Character_ClickMove
//*******************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMovement : MonoBehaviour
{
    [System.Serializable] // class�� �ν����Ϳ��� ������
    //[SerializeField] // private ������ �ν����Ϳ��� ������
    public class Floor
    {
        public string name;
        public Transform enemy;
    }
    public Floor[] floor;
    private Camera camera1;

    private bool isClick;
    private bool isMoving;
    
    private Vector2 destination;
    public float speed = 5;

    private Rigidbody2D rb;

    
    Vector2 MousePosition;



    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (Physics.Raycast(ray, out hit, 100))
                {
                    // whatever tag you are looking for on your game object
                    if (hit.collider.tag == "Trigger")
                    {
                        Debug.Log("---> Hit: ");
                    }
                }
            }

                MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            destination = new Vector2(MousePosition.x, transform.position.y);
            //Debug.Log(destination);
            
            isClick = true; //�ִϸ��̼� �Ҷ� �ʿ���
        }
        if (isClick && transform.position.x != destination.x)
        {
            isMoving = true;
            isClick = false;
            //transform.Translate(new Vector2(MousePosition.x - transform.position.x, 0)* Time.deltaTime * speed);
            
        }
        else
        {
            isClick = false;
        }
    }
    private void FixedUpdate()
    {
        if (isMoving == true)
        {
            //transform.position = Vector2.MoveTowards(transform.position, destination, Time.deltaTime * speed);
            //transform.position += (Vector3.right)*speed*Time.deltaTime; // ���⼱ ������ Vector3�� ����Ѵٰ���, �̰��ϸ� ��� �������θ� ��
            //rb.velocity = new Vector2(speed, 0); //������ ������� �ʰ� ������, y�� �������� ���� �� ����
            //rb.AddForce(new Vector2(-speed, 0)); //������ ����Ͽ� ������
            ///*
            if (isMoving == true && transform.position.x > destination.x)
            {
                rb.velocity = new Vector2(-speed, 0);//velocity�� ����ϸ� �������� �ٴٶ����� ����
                if (transform.position.x < destination.x)
                {
                    isMoving = false;
                }
            }
            else if(isMoving == true && transform.position.x <= destination.x)
            {
                rb.velocity = new Vector2(speed, 0);
                if(transform.position.x > destination.x)
                {
                    isMoving = false;
                }
            }
            //*/
        }
        
        if(isMoving == false)
        {
            rb.AddForce(new Vector2(0, 0));
            
        }
        
            //transform.position = new Vector2(0, 0);
    }
}
