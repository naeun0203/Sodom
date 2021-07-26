//*******************************
// Editor:CHG
// LAST EDITED DATE : 2021.07.19
// Script Purpose : Character_ClickMove
//*******************************
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMovement : MonoBehaviour
{
    [System.Serializable] // class�� �ν����Ϳ��� ������
    //[SerializeField] // private ������ �ν����Ϳ��� ������
    public class Floor
    {
        public Transform up;
        public Transform down;
        public GameObject floor;
    }
    public Floor[] floor;
    public float speed = 5;
    public float stairspeed;

    private Vector2 destination;
    private Vector2 first_destination;
    private Camera camera1;
    private Rigidbody2D rb;
    private Transform stairstart;
    private Transform stairend;

    private bool isClick;
    private bool isMoving = false;
    private bool isFirst = false;
    private bool isSecond = false;
    private bool isFirst_ing = false;
    private bool isSecond_ing = false;

    private int MovingCase;
    private int WhereCharacteris;
    private int WhatFloorToGo;


    Vector2 MousePosition;



    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            isFirst = false;
            isSecond = false;
            isMoving = false;
            MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            destination = new Vector2(MousePosition.x, transform.position.y);
            //destination = new Vector2(MousePosition.x, MousePosition.y);
            Debug.Log(MousePosition);
            
            //isClick = true; //�ִϸ��̼� �Ҷ� �ʿ���
            if (transform.position.x != destination.x)
            {
                isClick = true;
                WhereToGo();
                WhereCharacter();
                CaseSetting();
            }
        }
        
        else
        {
            //isClick = false;
        }
    }
    
    private void FixedUpdate()
    {
        if(isClick == true)
        {
            switch (MovingCase)
            {
                case 1:
                    Move();
                    break;
                case 2:
                    if(isFirst == false)
                    {
                        firstmove();
                        if (Math.Abs(transform.position.x - stairstart.position.x) < 1f)
                        {
                            isFirst = true;
                            isFirst_ing = false;

                        }
                    }
                    
                    if (isFirst == true)
                    {
                        secondmove();
                        if (Math.Abs(transform.position.y - stairend.position.y) < 0.1f)
                        {
                            isFirst = false;
                            isMoving = true;
                            //floor[WhatFloorToGo - 1].floor.gameObject.SetActive(true);
                            //Move();
                        }
                            
                    }
                    
                    break;
            }
        }
    }
    void firstmove()
    {
        isFirst_ing = true;
        //first_destination = new Vector2(stairstart.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, stairstart.position, Time.deltaTime * speed);
    }
    public void secondmove()
    {
        isSecond_ing = true;
        //transform.position = Vector2.MoveTowards(stairstart.position, stairend.position, Time.deltaTime * speed);
        transform.position = Vector2.Lerp(transform.position, stairend.position, Time.deltaTime * stairspeed);
        floor[WhatFloorToGo - 1].floor.gameObject.SetActive(true);
    }
    public void Move()
    {
        switch (MovingCase)
        {
            case 1:
                transform.position = Vector2.MoveTowards(transform.position, destination, Time.deltaTime * speed);
                if (Math.Abs(transform.position.x - destination.x) < 0.01f)
                {
                    isMoving = false;
                    isClick = false;
                }
                break;
            case 2:
                destination = new Vector2(MousePosition.x, transform.position.y); //��� �� �ö󰣵��� y��ǥ�� �ٽ� ��ǥ����
                transform.position = Vector2.Lerp(transform.position, destination, Time.deltaTime * speed);
                if (Math.Abs(transform.position.x - destination.x) < 0.01f)
                {
                    isMoving = false;
                    isClick = false;
                }
                break;


        }
    }
    void StairMove(Transform start, Transform end, float stairspeed)
    {
        transform.position = Vector2.MoveTowards(start.transform.position, end.transform.position, Time.deltaTime * stairspeed);
    }
    void WhereToGo()
    {
        if (MousePosition.y > -500 && MousePosition.y < 50)
        {
            WhatFloorToGo = 1;//1���� ����
        }
        if (MousePosition.y > 50 && MousePosition.y < 600)
        {
            WhatFloorToGo = 2;//2���� ����
        }
    }
    private void WhereCharacter()
    {
        if (transform.position.y > -500 && transform.position.y < 50)
        {
            WhereCharacteris = 1;//ĳ���Ͱ� 1���� ����
        }
        if (transform.position.y > 50 && transform.position.y < 600)
        {
            WhereCharacteris = 2;//ĳ���Ͱ� 2���� ����
        }
    }
    private void CaseSetting()
    {
        if(WhatFloorToGo == WhereCharacteris)//���� �������� �����̱�
        {
            MovingCase = 1; 
        }
        if(WhatFloorToGo > WhereCharacteris)//�ö󰡱�
        {
            MovingCase = 2; 
            stairstart = floor[WhereCharacteris - 1].down;
            stairend = floor[WhereCharacteris - 1].up;
            floor[WhatFloorToGo - 1].floor.gameObject.SetActive(false);
        }
        if (WhatFloorToGo < WhereCharacteris)//��������
        {
            MovingCase = 3; 
            stairstart = floor[WhereCharacteris - 2].up;
            stairend = floor[WhereCharacteris - 2].down;
        }
    }
    private void GoUp()
    {

    }
    private void GoDown()
    {

    }
    private void StairMove()
    {

    }

}
