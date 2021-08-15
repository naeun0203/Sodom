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
    EnemyNPC enemynpc;
    [System.Serializable] // class를 인스펙터에서 보여줌
    //[SerializeField] // private 변수를 인스펙터에서 보여줌
    public class Floor
    {
        public Transform up;
        public Transform down;
    }
    public Floor[] floor;
    public Transform pos;
    public Vector2 boxSize;
    public LayerMask EnemyNPCMask;
    public GameObject AttackRange;

    private Vector2 destination;
    private Vector2 first_destination;
    private Vector2 MousePosition;
    private Camera camera1;
    public Rigidbody2D rb;
    private Transform stairstart;
    private Transform stairend;
    private RaycastHit2D hit;
    public Animator anim;


    public bool isNormalMoving = false;
    public bool isFirst_ing = false;
    public bool isSecond_ing = false;
    public bool isClickEnemy;
    public bool isCollideEnemy = false;
    public bool attack =true;
    public bool isFirstChanged = false;
    public bool isSecondChanged = false;
    public bool isNormalChanged = false;

    private bool isFirst = false;
    private bool isSecond = false;
    private bool isNormalMove = false;
    //private bool isClick;
    private bool isSecond_ing_click = false;
    

    public int LeftRight;
    public int FinalDirection = 1;

    public int MovingCase;
    public int Wherecharacteris;
    public int Wheretogo;
    public int HP;
    public float speed = 5;
    public float stairspeed;


    



    void Start()
    {
        enemynpc = GameObject.Find("EnemyNPC_CHG").GetComponent<EnemyNPC>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        AttackRange.SetActive(false);

    }

    void Update()
    {
        if (isSecond_ing == false)
        {
            if (Input.GetMouseButtonUp(0))
            {
                anim.SetBool("isPunching", false);
                rb.isKinematic = true;
                EnemyClick();
                MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);//항상 제일 먼저
                WhereToGo();
                Debug.Log("Wheretogo: " + Wheretogo);

                if (Wheretogo != 0)
                {
                    WhereCharacter();
                    CaseSetting();
                }
            }
            else if (isSecond_ing_click == true)
            {
                WhereCharacter();
                CaseSetting();
                isSecond_ing_click = false;
                EnemyClick();
            }
        }
        if (isSecond_ing)
        {
            if (Input.GetMouseButtonUp(0))
            {
                rb.isKinematic = true;
                
                MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                WhereToGo();
                //Debug.Log("Wheretogo: " + Wheretogo);
                if (Wheretogo != 0)
                {
                    isSecond_ing_click = true;
                }
            }
        }
    }
    //계단 올라가는중 => 클릭 => 일단 위치 저장 => 계단 이동이 끝나면 바로 이 저장한 위치로 이동해야함
    
    private void FixedUpdate()
    {
        switch (MovingCase)
        {
            case 1:
                if(isNormalMoving == true) NormalMove();
                break;
            case 2:
                if (isFirst == false)
                {
                    Firstmove();
                    
                }
                else if (isFirst == true && isSecond == false)
                {
                    //rb.isKinematic = true;
                    Secondmove();
                    if (isSecond_ing_click)
                    {
                        break;
                    }
                }
                else if(isSecond)
                {
                    if (isNormalMoving) NormalMove();
                }
                break;
            case 3:
                if (isFirst == false && isSecond_ing == false)
                {
                    Firstmove();
                }
                else if (isFirst == true && isSecond == false)
                {
                    //rb.isKinematic = true;
                    Secondmove();
                }
                else if (isSecond == true)
                {
                    if (isNormalMoving == true) NormalMove();
                }
                break;
        }
        
        if (isClickEnemy)
        {
            if (enemynpc.iscollide)
            {
                if (attack == true)
                {
                    enemynpc.EnemyHP -= 5;
                    attack = false;
                    StartCoroutine(WaitForAttack());
                }
            }
        }
    }
    IEnumerator WaitForAttack()
    {
        yield return new WaitForSeconds(1.0f);
        attack = true;
    }
    void Firstmove()
    {
        isFirst_ing = true;
        if (this.transform.localScale.x > 0)
        {
            FinalDirection = 1;
        }
        else FinalDirection = -1;

        LeftOrRight(isFirstChanged, stairstart.position);

        anim.SetBool("isWalking", true);
        transform.position = Vector2.MoveTowards(transform.position, stairstart.position, Time.deltaTime * speed);
        
        if (Math.Abs(transform.position.x - stairstart.position.x) < 1f)
        {
            isFirst = true;
            isFirst_ing = false;
            
        }
    }

    private void Secondmove()
    {
        isSecond_ing = true;
        LeftOrRight(isSecondChanged, stairend.position);
        rb.isKinematic = true;
        //transform.position = Vector2.MoveTowards(stairstart.position, stairend.position, Time.deltaTime * speed);
        transform.position = Vector2.MoveTowards(transform.position, stairend.position, Time.deltaTime * stairspeed);
        if (Math.Abs(transform.position.y - stairend.position.y) < 0.1f)
        {
            rb.isKinematic = false;
            isSecond = true;
            isSecond_ing = false;
            isNormalMoving = true;
        }
    }
    public void NormalMove()
    {
        if (isClickEnemy)
        {
            AttackRange.SetActive(true);
        }
        else rb.isKinematic = true;
        destination = new Vector2(MousePosition.x, transform.position.y);
        LeftOrRight(isNormalChanged, destination);
        anim.SetBool("isWalking", true);
        transform.position = Vector2.MoveTowards(transform.position, destination, Time.deltaTime * speed);
        if (Math.Abs(transform.position.x - destination.x) < 0.01f)
        {
            isNormalMoving = false;
            anim.SetBool("isWalking", false);
            rb.isKinematic = false;
            //isClick = false;
        }
    }
    void LeftOrRight(bool isChanged, Vector3 vector3)//isChanged 
    {
        if (!isChanged)//방향을 바꾸려는걸 시도하지 않았을때만
        {
            if (transform.localScale.x > 0) // 현재 방향이 오른쪽이면
            {
                FinalDirection = 1;
            }
            else FinalDirection = -1;
            
            if (transform.position.x > vector3.x) // 가야할 방향이 왼쪽이면
            {
                LeftRight = -1;
                if (FinalDirection != LeftRight) // 현재 방향과 가야할 방향이 다를때만 변화주기
                {
                    transform.localScale += new Vector3(-2f, 0f, 0f);
                }
            }
            else // 가야할 방향이 오른쪽이면
            {
                LeftRight = 1; 
                if (FinalDirection != LeftRight)
                {
                    transform.localScale += new Vector3(2f, 0f, 0f);
                }
                
            }

            isChanged = true;
        }
    }
    void EnemyClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        hit = Physics2D.Raycast(ray.origin, ray.direction, 10f);
        if (hit)
        {
            //Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 1f);
            Debug.Log(hit.collider.tag);
            if (hit.collider.CompareTag("EnemyNPC"))
            {
                //Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                isClickEnemy = true;
                rb.isKinematic = false;
            }
            else
            {
                isClickEnemy = false;
                AttackRange.SetActive(false);
            }
        }
        else
        {
            isClickEnemy = false;
            AttackRange.SetActive(false);
        }
    }
    void WhereToGo()
    {
        if (MousePosition.y > -385 && MousePosition.y < -81)
        {
            Wheretogo = 1;//1층에 갈것
        }
        else if (MousePosition.y > -55 && MousePosition.y < 250)
        {
            Wheretogo = 2;//2층에 갈것
        }
        else
            Wheretogo = 0; //집 외에 다른곳 선택
    }
    private void WhereCharacter()
    {
        if (transform.position.y > -291 && transform.position.y < -25)
        {
            Wherecharacteris = 1;//캐릭터가 1층에 있음
        }
        if (transform.position.y > 24 && transform.position.y < 290)
        {
            Wherecharacteris = 2;//캐릭터가 2층에 있음
        }
        
    }
    private void CaseSetting()
    {
        if(Wheretogo == Wherecharacteris)//같은 층내에서 움직이기
        {
            MovingCase = 1;
            isNormalMoving = true;
            isNormalChanged = false;
        }
        if(Wheretogo > Wherecharacteris)//올라가기
        {
            isFirst = false;
            isSecond = false;
            isNormalMove = false;
            isFirstChanged = false;
            isSecondChanged = false;
            isNormalChanged = false;
            MovingCase = 2; 
            stairstart = floor[Wherecharacteris - 1].down;
            stairend = floor[Wherecharacteris - 1].up;
        }
        if (Wheretogo < Wherecharacteris)//내려가기(3층이상 되면 달라져야함)
        {
            isFirst = false;
            isSecond = false;
            isFirstChanged = false;
            isSecondChanged = false;
            isNormalChanged = false;
            MovingCase = 3;
            stairstart = floor[Wherecharacteris - 2].up;
            stairend = floor[Wherecharacteris - 2].down;
        }
        /*
        if(isSecond_ing_click == true)//계단 올라가거나 내려가던 중에 클릭됐을때
        {
            MovingCase = 4;
        }
        */
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }
    private void GoUp()
    {

    }
    private void GoDown()
    {

    }

}
