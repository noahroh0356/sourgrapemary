using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatchaHead : MonoBehaviour
{
    public GatchaCanvas gatchaCanvas;
    public float moveSpeed=5;

    //public float moveUpSpeed = 1f; // 올라가는 속도 추가
    public Transform clawLeft; // 왼쪽 집게
    public Transform clawRight; // 오른쪽 집게
    public float clawCloseDistance = 1f; // 집게가 닫히는 거리
    public float clawMoveSpeed = 0.5f; // 집게 움직이는 속도

    public bool isMovingDown = false; // 내려가는 중인지 확인
    public bool isCollided = false; // 충돌 여부 확인
    public bool isClawClosed = false; // 집게가 닫혔는지 확인
    public bool isMovingUp = false; // 올라가는 중인지 확인
    public bool isDone = false; // 물건 들어올린 후 
    public bool isComeback = false; // 떨어뜨린 후



    public void MoveLeft()
    {
        transform.position += Vector3.left*moveSpeed*Time.deltaTime;
    }

    public void MoveRight()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }

    public void StartMoveDown()
    { 
        if (isComeback)
            return;
        isMovingDown = true;
    }

    // 밑으로 내려가게 하기, 게임 오브젝트는 그대로 가챠 이미지만 내려가는 걸로+ 집게에 충돌체가 닿으면 스톱하기, 가능하면 위로

    private void Update()
    {

        if (!isComeback && isMovingUp ==false)
        {
            if (gatchaCanvas.pushDownLeft == true)
            {
                MoveLeft();
            }

            if (gatchaCanvas.pushDownRight == true)
            {
                MoveRight();
            }

            if (isMovingDown && !isCollided)
            {
                transform.position += Vector3.down * moveSpeed * Time.deltaTime;
            }

        }




        if (isCollided&&!isClawClosed)
        {
            CloseClaw();
        }



    }

    public GatchaBall gatchaBall;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (isMovingDown && !isCollided)
        {
            if (collision.CompareTag("GatchaWall") || collision.CompareTag("GatchaBall"))
            {
                if (collision.CompareTag("GatchaBall"))
                {
                    gatchaBall = collision.GetComponent<GatchaBall>();
                    if (gatchaBall == null)
                    {
                        gatchaBall = collision.GetComponentInParent<GatchaBall>();
                    }
                    Debug.Log($"gatchaBall name{gatchaBall.name}");

                    gatchaBall.transform.parent = transform; // 가챠 볼의 부모를 가챠 헤드로 설정 따라 올라오게 하기 위해서
                    gatchaBall.rgdy.bodyType = RigidbodyType2D.Kinematic;

                    float xGap = Mathf.Abs(gatchaBall.transform.position.x - transform.position.x); //현재 가챠 헤드와 가챠 볼의 x거?

                    float pickedChance = 100 - (xGap*100); // 곱해지는 수가 높을 수록 난이도가 상승

                    if (pickedChance <= 10)
                    { pickedChance = 10; }

                    float randomChance = Random.Range(0f, 100f);

                    //if (randomChance <= pickedChance)
                    //{ }
                    //else
                    //{ }
                    StartCoroutine(CoProcessGatchaBall(gatchaBall, randomChance<= pickedChance));
                        }
                isCollided = true;
                isMovingDown = false;
                StartCoroutine(CoProcessMove());

            }
        }
    }

    //가챠 헤더가 집은 가챠볼 처리용
    IEnumerator CoProcessGatchaBall(GatchaBall gatchaBall, bool success)
    {
        if (success == false)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 2f));
            Debug.Log("실패했으니 떨어뜨린다");

            gatchaBall.rgdy.bodyType = RigidbodyType2D.Dynamic;
            gatchaBall.transform.parent = null;
        }
    }
    //가챠 헤더 이동용


    IEnumerator CoProcessMove()
    {
        isMovingUp = true;

        while (true)
        { 
        if (transform.position.y >= 14.5f)
        {
            //moveUpSpeed = 0f;
                isDone = true;
            break; 
        }

            transform.position += Vector3.up * moveSpeed * Time.deltaTime;

            yield return null;

        }

        isMovingUp = false;


        if (isDone)
        {
            StartCoroutine(DropMoveCoroutine());
        }

        //어느만큼 위? 어느 만큼 오른쪽으? 특정 위치 도착하면 멈춤
    }

    IEnumerator DropMoveCoroutine()
    {
        Debug.Log("DropMove 시작");

        while (Mathf.Abs(transform.localPosition.x - 6.5f) > 0.1f) 
        {
            Debug.Log("DropMove 도착");
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(6.5f, transform.localPosition.y, transform.localPosition.z), moveSpeed * Time.deltaTime);

            yield return null;
        }

        moveSpeed = 10;
        isDone = false;
        isComeback = true;
        isClawClosed = false;        //**집게 열기 코드
        // 집게를 다 열고
        // 제자리로 돌아오기

        if (gatchaBall != null && gatchaBall.gameObject.activeSelf)
        {
            gatchaBall.rgdy.bodyType = RigidbodyType2D.Dynamic;
            gatchaBall.transform.parent = null;
        }


        while (Mathf.Abs(transform.localPosition.x) > 0.1f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0f, transform.localPosition.y, transform.localPosition.z), moveSpeed * Time.deltaTime);
            yield return null;
        }
        // 원래 자리로 돌아가게 하는 코드 나올자리

        isCollided = false;
        isMovingDown = false;
        isMovingUp = false;
        isComeback = false;

        Debug.Log("DropMove 완료, Comeback 시작");
    }

    //IEnumerator DropMoveCoroutine()
    //{
    //    transform.position += Vector3.right * moveSpeed * Time.deltaTime;

    //    if (!Mathf.Approximately(transform.position.x, 6.5f))
    //    {
    //        moveSpeed = 0;
    //        isDone = false;
    //        isComeback = true;
    //        yield return null;
    //    }


    //}

    //public void DropMove()
    //{

    //    if (!Mathf.Approximately(transform.position.x, 6.5f))
    //    {

    //        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    //    }
    //    else
    //    {
    //        moveSpeed = 0;
    //        isDone = false;

    //    }
    //    gatchaBall.rgdy.bodyType = RigidbodyType2D.Dynamic;

    //}


    public void CloseClaw()
    {
        if (Vector3.Distance(clawLeft.localPosition, clawRight.localPosition) > clawCloseDistance)
        {
            clawLeft.localPosition += Vector3.right * clawMoveSpeed * Time.deltaTime;
            clawRight.localPosition += Vector3.left * clawMoveSpeed * Time.deltaTime;
        }
        else
        {
            clawMoveSpeed = 0f;
            isClawClosed = true;
        }
    }
}


