﻿using System.Collections; using System.Collections.Generic; using UnityEngine;  public class HeroMove : MonoBehaviour  {      public float moveSpeed = 2f;    //이동 속도     public float jumpPower = 3f;    //점프 속도       public int jumpCount = 2; //점프횟수    2를 3으로 바꾸면 3단 점프      new Rigidbody rigidbody;     Animator animator;      bool isGrounded = false;      bool isMoving = false;
    bool isJumping;      Vector3 movement;      public static float horizontalMove;     private Quaternion Up = Quaternion.identity;

           void Awake()     {         rigidbody = GetComponent<Rigidbody>(); //컴포넌트를 불러옴         animator = GetComponent<Animator>(); //애니메이터 불러오기          jumpCount = 0;     }

  

        private void OnCollisionEnter(Collision col)      {          if (col.gameObject.tag == "Ground")         {             isGrounded = true;    //Ground에 닿으면 isGround는 true             jumpCount = 2;          //Ground에 닿으면 점프횟수가 2로 초기화됨
        }     }        void Update()      {         MoveLeft();
        MoveRight(); 


        if (Input.GetButtonDown("Jump"))
        {          isJumping = true;         animator.SetTrigger("isJumping");         }         
         RunAnimationUpdate();         JumpAnimationUpdate();      }     void FixedUpdate()
    {
         Jump();        
    }
     //----------------------        public void MoveRight()     {         if (Input.GetKey(KeyCode.RightArrow))         {             if (transform.rotation.eulerAngles.y != 0)                      {                 Up.eulerAngles = new Vector3(0, 0, 0);                 transform.rotation = Quaternion.Slerp(transform.rotation, Up, 1);             }             transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);             isMoving = true;         }      }
 
    public void MoveLeft()     {         if (Input.GetKey(KeyCode.LeftArrow))         {             if (transform.rotation.eulerAngles.y != 180)             {                 Up.eulerAngles = new Vector3(0, 180, 0);                 transform.rotation = Quaternion.Slerp(transform.rotation, Up, 1);             }             transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);             isMoving = true;         }         else         isMoving = false;      }

    void Jump()
    {
        if(isGrounded)  //땅에 붙어있을 때
        {              if(jumpCount>0)  //점프 카운터가 남아 있으면
            {                  if (!isJumping)                   return;                       rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);                      jumpCount--;                      isJumping = false;
            } 
        } 
    } 
    void RunAnimationUpdate()
    {
        if (isMoving)
        {
            animator.SetBool("isMoving", true);
        }         else
        {
            animator.SetBool("isMoving", false);
        }
    }      void JumpAnimationUpdate()
    {
        if(isJumping)
        {
            animator.SetBool("doJumping", false);
        }          else         {             animator.SetBool("doJumping", true);         } 
    }
         
}  