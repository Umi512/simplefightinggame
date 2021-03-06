using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// プレイヤーキャラクターを左右に移動させる。向きの反転、接地判定も行う。
// 解説記事　http://negi-lab.blog.jp/PlayerWalkAndJump2D
public class CharacterController2 : MonoBehaviour
{
    // 値はインスペクターから変更可能
    [SerializeField] private float moveSpeed = 100000f;

	public float timer = 0;
	[SerializeField] public float time_bettween_shot = 1.35f; 

	//[SerializeField] private float magazine = 10;
	//private float magazine_count = magazine;
	/*[SerializeField] private float cool_time = 3f;
	private float cool_timer = 0;
	[SerializeField] private bool cool_time_swich = false;
	private float[] cool_time_changer = new float[] { 0,cool_time};*/

	public bool isGround;
    public bool have_agun;
    private Rigidbody2D rb;
    //private Vector3 defalutScale;
    private Animator animator;


    //キーコード https://docs.unity3d.com/ja/550/ScriptReference/KeyCode.html
	[SerializeField] private KeyCode jumpkey = KeyCode.UpArrow;
	[SerializeField] private KeyCode attackkey = KeyCode.DownArrow;
	[SerializeField] private KeyCode leftkey = KeyCode.LeftArrow;
	[SerializeField] private KeyCode rightkey = KeyCode.RightArrow;

    //アニメーション管理
    private float jumpThreshold = 10f;
    private static bool facing_right = true;                 // 左右の入力管理
    private static bool prev_facing_right = true;
    public string state;                // プレイヤーの状態管理
    //private string prevState;
    private float stateEffect = 1;       // 状態に応じて横移動速度を変えるための係数

    [SerializeField] private float max_runspeed_x = 50f;      //横移動スピードの上限 


    // 値はインスペクターから変更可能f
    [SerializeField] private float jumpPower = 7000f;

    //JumpBlock
    [SerializeField] private int powerX=0;
	[SerializeField] private int powerY=14000;
	private Vector3 target;

    public GameObject bulletPrefab;
    public GameObject player1;
    public GameObject responepoint;
	public GameObject worpPoint1;
	public GameObject worpPoint2;
	public float worpCoolTime = 0.5f;
    public Transform firePoint;
    public GameObject countdown;
    private bool startsw = false;
    public GameObject UI;

    private void Start()
    {
        // ゲームプレイ中、頻繁に呼び出されるコンポーネントは Start 内でキャッシュしておく
        // 毎回 GetComponent すると負荷が高くなるため
        rb = GetComponent<Rigidbody2D>();

        // 開始時のローカルスケールの値を記憶しておく
        //defalutScale = transform.localScale;
        animator = GetComponent<Animator>();

       // bulletPrefab = GameObject.Find("bulletPrefab");
        responepoint = GameObject.Find("RespawnPoint");
		worpPoint1 = GameObject.Find ("wp1");
		worpPoint2 = GameObject.Find ("wp2");
        countdown = GameObject.Find("StartFinish");
        UI = GameObject.Find("FightingCanvas");
    }

    private void Update()
    {
        if (startsw == false)
        {
            if (countdown.GetComponent<Text>().text == "Start")
            {
                startsw = true;
            }
            return;
        }
        if (countdown.GetComponent<Text>().text == "Finish")
        {
            startsw = false;
        }
            worpCoolTime += Time.deltaTime;
		timer += Time.deltaTime;
        // 左右方向の入力を受け取る
        animator.SetBool("handattack", false);
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
		if (have_agun && timer >= time_bettween_shot) {
			//print (bulletPrefab);
			Instantiate (bulletPrefab, firePoint.position, firePoint.rotation);
			timer = 0;
		}
        else
        {
            animator.SetBool("handattack", true);
        }
    }

	public int Get_inputs()
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
        ////print(col.gameObject.tag);

        ////print("aaaa");
        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "TransformGround" || col.gameObject.tag == "JumpingBlock")
        {
            isGround = true;
        }

    }
    private void OnTriggerEnter2D(Collider2D col)
    {

		//print ("-------");
		//print (col);
        if (col.gameObject.tag == "hand")
        {
            this.gameObject.transform.position = responepoint.transform.position;
            have_agun = false;
            animator.SetBool("have_agun", false);
            this.tag = "Player";
            UI.GetComponent<UIFightingController>().KillCount(2);
            UI.GetComponent<UIFightingController>().DeathCount(1);
        }
        if (col.gameObject.tag == "bullet")
        {
            this.gameObject.transform.position = responepoint.transform.position;
            UI.GetComponent<UIFightingController>().KillCount(2);
            UI.GetComponent<UIFightingController>().DeathCount(1);
        }
		if (col.gameObject.tag == "worpPoint1" && worpCoolTime>0.5)
		{
			worpCoolTime = 0;
			//print ("wp1");
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

        //prevState = state;
        //}
    }
    public void getagun()
    {
        have_agun = true;
        animator.SetBool("have_agun", true);
        this.tag = "attackerPlayer";
    }
}