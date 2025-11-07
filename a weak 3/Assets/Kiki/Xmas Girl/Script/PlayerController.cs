using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SD
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Player Setting")]
        public float turnSpeed = 10f;
        public float runSpeed = 3f;
        public float jumpForce = 8f;
        private int jumpCount = 0;
        public bool stopMoverment = false;

        public bool moving { get; set; }

        float m_Horizontal, m_Vertical;
        private Vector3 m_MoveVector;
        private Rigidbody m_Rigidbody;

        [HideInInspector]
        public Animator m_Animator;
        private Quaternion m_Rotation = Quaternion.identity;
        private Transform camTrans;
        private Vector3 camForward;
        private bool jumpRequested = false;

        void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
            m_Animator = GetComponent<Animator>();
            camTrans = Camera.main.transform;

            // 念のため確認
            m_Rigidbody.useGravity = true;
            m_Rigidbody.isKinematic = false;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && jumpCount == 0)
            {
                jumpRequested = true;
            }
        }

        void FixedUpdate()
        {
            // 入力
            m_Horizontal = Input.GetAxis(Const.Horizontal);
            //m_Vertical = Input.GetAxis(Const.Vetical);

            // カメラ方向に基づく移動ベクトル
            if (camTrans != null)
            {
                camForward = Vector3.Scale(camTrans.forward, new Vector3(1, 0, 1).normalized);
                m_MoveVector = m_Vertical * camForward + m_Horizontal * camTrans.right;
                m_MoveVector.Normalize();
            }

            // アニメーション制御
            bool has_H_Input = !Mathf.Approximately(m_Horizontal, 0);
            bool has_V_Input = !Mathf.Approximately(m_Vertical, 0);
            moving = !stopMoverment && (has_H_Input || has_V_Input);

            float inputSpeed = Mathf.Clamp01(Mathf.Abs(m_Horizontal) + Mathf.Abs(m_Vertical));
            m_Animator.SetBool(Const.Moving, moving);
            m_Animator.SetFloat(Const.Speed, inputSpeed);

            // 移動と回転
            if (moving)
            {
                Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_MoveVector, turnSpeed * Time.deltaTime, 0f);
                m_Rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredForward), turnSpeed);
                m_Rigidbody.MoveRotation(m_Rotation);
                m_Rigidbody.MovePosition(m_Rigidbody.position + inputSpeed * m_MoveVector * runSpeed * Time.deltaTime);
            }

            // ジャンプ処理
            if (jumpRequested)
            {
                m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, 0f, m_Rigidbody.velocity.z);
                m_Rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                jumpCount++;
                m_Animator.SetTrigger("Jump");
                jumpRequested = false; // ← これがないとジャンプし続ける
            }

            //　エモート
            if (Input.GetKey(KeyCode.Alpha1))
            {
                m_Animator.SetTrigger("Emote1");
            }

            if (Input.GetKey(KeyCode.Alpha2))
            {
                m_Animator.SetTrigger("Emote2");
            }
        }

        void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Floor"))
            {
                jumpCount = 0;
            }
        }
    }
}
