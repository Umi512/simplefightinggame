using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// プレイヤーキャラクターを左右に移動させる。向きの反転、接地判定も行う。
// 解説記事　http://negi-lab.blog.jp/PlayerWalkAndJump2D
public class characterController1 : MonoBehaviour
{
    // 値はインスペクターから変更可能
    [SerializeField] private float moveSpeed = 100000f;

	public float timer;
	[SerializeField] public float time_bettween_shot = 1.35f; 
	//[SerializeField] private float cool_time = ;
	//private float cool_timer = 0;

    public bool isGround;
    public bool have_agun;

    private Rigidbody2D rb;
    private Vector3 defalutScale;
    private Animator animator;
    //キーコード https://docs.unity3d.com/ja/550/ScriptReference/KeyCode.html
    [SerializeField] private KeyCode jumpkey = KeyCode.W;
    [SerializeField] private KeyCode attackkey = KeyCode.S;
    [SerializeField] private KeyCode leftkey = KeyCode.A;
    [SerializeField] private KeyCode rightkey = KeyCode.D;

    //アニメーション管理
    private float jumpThreshold;
    private static bool facing_right;                 // 左右の入力管理
    private static bool prev_facing_right;
    private string state;                // プレイヤーの状態管理
    private string prevState;
    private float stateEffect;       // 状態に応じて横移動速度を変えるための係数

    [SerializeField] private float max_runspeed_x = 50f;      //横移動スピードの上限 


    // 値はインスペクターから変更可能f
    [SerializeField] private float jumpPower = 7000f;

    public GameObject bulletPrefab;
    public Transform firePoint;

    public GameObject player2;
    public GameObject responepoint;
	public GameObject worpPoint1;
	public GameObject worpPoint2;
    public GameObject countdown;
    public GameObject UI;

    //JumpBlock
    [SerializeField] private int powerX=0;
	[SerializeField] private int powerY=500;
	private Vector3 target;

    public float worpCoolTime;
    public bool startsw;

    private GameObject leftLoop;
    private GameObject rightLoop;
    private bool tagTrigger = false;
    private float tagTime;

    private void Start()
    {
        timer = 0;
        stateEffect = 1;
        jumpThreshold = 10f;
        facing_right = true;
        prev_facing_right = true;
        worpCoolTime = 0.5f;
        startsw = false;
        tagTime = 0.0f;
        startsw = false;
        // ゲームプレイ中、頻繁に呼び出されるコンポーネントは Start 内でキャッシュしておく
        // 毎回 GetComponent すると負荷が高くなるため
        rb = GetComponent<Rigidbody2D>();

        // 開始時のローカルスケールの値を記憶しておく
        defalutScale = transform.localScale;

        animator = GetComponent<Animator>();
		responepoint = GameObject.Find("RespawnPoint");//RespawnPoint
        countdown = GameObject.Find("StartFinish");
        worpPoint1 = GameObject.Find ("wp1");
		worpPoint2 = GameObject.Find ("wp2");
        UI = GameObject.Find("FightingCanvas");
        leftLoop = GameObject.Find ("LoopLeft");
		rightLoop = GameObject.Find ("LoopRight");
    }

    private void Update()
    {
        if (startsw == false)
        {
            if (countdown.GetComponent<Text>().text == "Start"){
                startsw = true;
            }
            return;
        }
        if (countdown.GetComponent<Text>().text == "Finish")
        {
            startsw = false;
        }
        if(tagTrigger == true)
        {
            tagTime += Time.deltaTime;
        }
        if(tagTime > 0.5f)
        {
            tagTime = 0.0f;
            this.tag = "Player";
            tagTrigger = false;
        }

        worpCoolTime += Time.deltaTime;
		timer += Time.deltaTime;
        // 左右方向の入力を受け取る
        animator.SetBool("handattack", false);
        //hc.tag = "Player";
        var input_h = Get_inputs();
        Turn(input_h);
        Walk(input_h);


        if (Input.GetKeyDown(jumpkey))
        {
            //print("space");
            // 接地しているときのみ、ジャンプできる（多段ジャンプをさせない）
            if (isGround)
            {
                rb.AddForce(Vector2.up * jumpPower);
            }
        }

        if (Input.GetKeyDown(attackkey))
        {
            Attack();
        }
        ChangeState();
        ChangeAnimation();
    }

    private void Attack()
    {
		if (have_agun && timer >= time_bettween_shot)
        {
            BulletController.who = 1;
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
			timer = 0;
        }
        else
        {
            animator.SetBool("handattack", true);
        }
    }

    private int Get_inputs()
    {
        var input_h = 0;
        //var input_h = Input.GetAxis("Horizontal");
        if (Input.GetKey(leftkey))
        {
            input_h -= 1;
        }
        if (Input.GetKey(rightkey))
        {
            input_h += 1;
        }
        return input_h;
    }
    // 水平方向の移動
    private void Walk(float inputValue)
    {
        if (inputValue != 0)
        {
            ////print(inputValue);
            if (max_runspeed_x > Mathf.Abs(rb.velocity.x))
            {
                // 接地していないときは水平方向に移動する力を弱める
                var mult = isGround ? 1f : 0.5f;

                // Rigidbody2D に力を加えることでプレイヤーキャラクターを移動させる
                rb.AddForce(Vector2.right * inputValue * moveSpeed * mult * Time.deltaTime);

                //transform.Translate(-0.1f * inputValue * moveSpeed * mult * Time.deltaTime, 0, 0);
            }
            else
            {
                transform.position += new Vector3(max_runspeed_x * Time.deltaTime * stateEffect * inputValue, 0, 0);
            }
        }
        else if (isGround)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    // 向きを変える
    private void Turn(float inputValue)
    {
        if (inputValue > 0)
        {
            facing_right = true;
        }
        else if (inputValue < 0)
        {
            facing_right = false;
        }
        
        if (prev_facing_right != facing_right)
        {
            // ローカルスケールのXの値を反転させることで、スプライトを反転させる
            transform.Rotate(0f, 180f, 0f);
        }
        prev_facing_right = facing_right;
    }

    // 接地判定
    private void OnCollisionEnter2D(Collision2D col)
    {
        //print(col.gameObject.tag);

        //print("aaaa");
		if (col.gameObject.tag == "Ground" || col.gameObject.tag == "TransformGround" || col.gameObject.tag == "JumpingBlock")
        {
            isGround = true;
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        //print(col.gameObject.tag);

        //print("aaaa");
		if (col.gameObject.tag == "Ground" || col.gameObject.tag == "TransformGround" || col.gameObject.tag == "JumpingBlock")
        {
            isGround = true;
        }

    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        print(col.tag);
        if (col.gameObject.tag == "hand" || col.gameObject.tag == "hand1")
        {
            this.gameObject.transform.position = responepoint.transform.position;
            have_agun = false;
            animator.SetBool("have_agun", false);
            //this.tag = "Player";
            tagTrigger = true;
            UIFightingController.KillCount(1);
            UI.GetComponent<UIFightingController>().DeathCount(2);
        }
        if (col.gameObject.tag == "bullet")
        {
            this.gameObject.transform.position = responepoint.transform.position;
            UI.GetComponent<UIFightingController>().DeathCount(2);
        }
        if (col.gameObject.tag == "DeadLine")
        {
            this.gameObject.transform.position = responepoint.transform.position;
            UI.GetComponent<UIFightingController>().DeathCount(2);
        }
        if (col.gameObject.tag == "worpPoint1" && worpCoolTime>0.5)
		{
			worpCoolTime = 0;
			print ("wp1");
			this.gameObject.transform.position = worpPoint2.transform.position;
			rb.velocity = Vector2.zero;
			rb.AddForce(Vector2.up * jumpPower);
		}
		if (col.gameObject.tag == "worpPoint2"&& worpCoolTime>0.5)
		{
			worpCoolTime = 0;
			//print ("wp2");
			this.gameObject.transform.position = worpPoint1.transform.position;
			rb.velocity = Vector2.zero;
			rb.AddForce(Vector2.up * jumpPower);
		}

        //キャラクターを動く床の動きに追従させる
		if (col.tag == "TransformGround")
        	transform.SetParent(col.transform);
        
        //JumpBlock
        if(col.tag == "JumpingBlock")
		{	//Playerの速度に合わせて、ジャンプ力調整
			target = -col.GetComponent<Rigidbody2D>().velocity;
			rb.AddForce (new Vector3(powerX,powerY,0)+target*50);
		}
        //loop map
        if (col.gameObject.tag == "LoopLeft" && worpCoolTime > 0.5) {
			worpCoolTime = 0;
			Vector3 pos = this.gameObject.transform.position;
			this.gameObject.transform.position = new Vector3 (rightLoop.transform.position.x, pos.y, pos.z);
		}
		if (col.gameObject.tag == "LoopRight" && worpCoolTime > 0.5) {
			worpCoolTime = 0;
			Vector3 pos = this.gameObject.transform.position;
			this.gameObject.transform.position = new Vector3 (leftLoop.transform.position.x, pos.y, pos.z);
		}
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        //print(col.gameObject.tag);
        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "TransformGround" || col.gameObject.tag == "JumpingBlock")
        {
            //   //print("a");
            isGround = false;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        //キャラクターを動く床の動きに追従に追従させる、を停止	
		if (col.gameObject.tag == "TransformGround") 
        	transform.SetParent(null);
    }

    //アニメーション---------------------------------------------------------------------------------------------------------------
    private void ChangeState()
    {
        ////print(System.Math.Abs(rb.velocity.x)) ;
        // 空中にいるかどうかの判定。上下の速度(rigidbody.velocity)が一定の値を超えている場合、空中とみなす
        //if (Mathf.Abs(rb.velocity.y) > jumpThreshold)
        //{
        //    isGround = false;
        //}

        // 接地している場合
        if (isGround)
        {

            // 走行中
            if (System.Math.Abs(rb.velocity.x) > jumpThreshold)
            {
                state = "RUN";
                //待機状態

            }
            else
            {
                //  //print("A");
                state = "IDLE";
            }
            // 空中にいる場合
        }
        else
        {
            // 上昇中
            ////print(rb.velocity.y);
            if (rb.velocity.y > 0)
            {
                state = "JUMP";
                // 下降中
            }
            else if (rb.velocity.y < 0)
            {
                state = "FALL";
            }
        }
    }

    void ChangeAnimation()
    {
        // 状態が変わった場合のみアニメーションを変更する

        ////print(state);
        //if (prevState != state)
        //{
        // //print(isGround);
        switch (state)
        {

            case "JUMP":
                animator.SetBool("isJump", true);
                animator.SetBool("isFall", false);
                animator.SetBool("isRun", false);
                animator.SetBool("isIdle", false);
                stateEffect = 0.5f;
                break;
            case "FALL":
                animator.SetBool("isFall", true);
                animator.SetBool("isJump", false);
                animator.SetBool("isRun", false);
                animator.SetBool("isIdle", false);
                stateEffect = 0.5f;
                break;
            case "RUN":
                animator.SetBool("isRun", true);
                animator.SetBool("isFall", false);
                animator.SetBool("isJump", false);
                animator.SetBool("isIdle", false);
                stateEffect = 1f;
                //GetComponent<SpriteRenderer> ().flipX = true;
                //transform.localScale = new Vector3(key, 1, 1); // 向きに応じてキャラクターのspriteを反転
                break;
            default:
                animator.SetBool("isIdle", true);
                animator.SetBool("isFall", false);
                animator.SetBool("isRun", false);
                animator.SetBool("isJump", false);
                stateEffect = 1f;
                break;
        }
        // 状態の変更を判定するために状態を保存しておく

        prevState = state;
        //}
    }
    public void getagun()
    {
        have_agun = true;
        animator.SetBool("have_agun", true);
        this.tag = "attackerPlayer";
    }
}