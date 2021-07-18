using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //run
    private CharacterController controller;
    private Vector3 direction;
    public float speed;

    //move
    private Touch touch;
    private float MoveSpeed;


    //Score
    public Text score;
    private int Score = 0;

    //animation

    protected Animator Anim1;
    protected Animator Anim2;


    //Body parts

    public GameObject greentop, greenbot, goldentop, goldenbot;
    private bool GreenT;
    private bool GreenB;
    private bool GoldenT;
    private bool GoldenB;


    private void Awake()
    {

        Anim1 = GameObject.Find("Char1").GetComponent<Animator>();
        Anim2 = GameObject.Find("Char2").GetComponent<Animator>();
    }

    void Start()
    {
        Time.timeScale = 1;
        GreenB = false; GoldenB = true;
        GreenT = true; GoldenT = false;

        //swipe speed
        MoveSpeed=0.01f;

        controller = GetComponent<CharacterController>();

      

    }

    void Update()
    {
        if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Moved)
            {

                Vector3 pos = Camera.main.ScreenPointToRay(new Vector3(0, 0, 0)).GetPoint(10);
                Vector3 directionMove = Vector3.right * touch.deltaPosition.x * MoveSpeed * Time.deltaTime;

                if (transform.position.x + directionMove.x < -pos.x-0.01 && transform.position.x + directionMove.x > pos.x+0.01)
                {
                    controller.Move(new Vector3(transform.position.x + (touch.deltaPosition.x * MoveSpeed), transform.position.y, transform.position.z));

                }
            }
        }

        direction.z = speed;

    }

    private void FixedUpdate()
    {
      controller.Move(direction * Time.fixedDeltaTime);


#if UNITY_EDITOR

        PlayerMove(Input.GetAxis("Horizontal"));
#endif

    }


   
    private void PlayerMove(float HInput)
    {
        Vector3 pos = Camera.main.ScreenPointToRay(new Vector3(0, 0, 0)).GetPoint(10);
        Vector3 directionMove = Vector3.right * HInput * 2 * Time.deltaTime;
 
        if(transform.position.x+directionMove.x < -pos.x && transform.position.x + directionMove.x > pos.x )
        {
            controller.Move(directionMove);
           
        }
    }

   private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.name == "Obstacle")
        {
            if (GreenT)
            { 
                Anim1.SetBool("PunchIt", false);
                Anim2.SetBool("PunchIt", false);
                hit.gameObject.GetComponent<BreakableWindow>().BreakIt(hit.gameObject);
            }
            else
            {
                GameController.gameover = true;
            }
        }
        if (hit.transform.name == "obstacle01")
        {
            Anim1.SetBool("isGrounded", true);
            Anim2.SetBool("isGrounded", true);
            Destroy(hit.gameObject);
        }
        if (hit.transform.name == "obstacle02")
        {
            if (GreenT)
            { 
                Anim1.SetBool("PunchIt", true);
                Anim2.SetBool("PunchIt", true);
                Destroy(hit.gameObject);
            }
            else
            {
                GameController.gameover = true;
            }
        }

        if (hit.transform.name == "Solid")
        {
            Debug.Log("Solid");
            Anim1.Play("Run");
            Anim2.Play("Run");
        }

        if (hit.transform.name == "CrouchGlass")
        {
            if (GreenB)
            {
                Anim1.Play("Crouch");
                Anim2.Play("Crouch");
            }
            else
            {
                Destroy(hit.gameObject);
                GameController.gameover = true;
            }
            
        }
        if (hit.transform.name == "JumpPosGlass")
        {
            if (GoldenT)
            {           
                Anim1.SetBool("isGrounded", false);
                Anim2.SetBool("isGrounded", false);
                Anim1.SetBool("Hold", true);
                Anim2.SetBool("Hold", true);
                Debug.Log("golden");

            }
            else
            {

                GameController.gameover = true;
            }

        }

        if(hit.transform.name== "Lift-coll")
        {

            Anim1.SetBool("isGrounded", true);
            Anim2.SetBool("isGrounded", true);

            Destroy(hit.gameObject);
            
        }

        if (hit.transform.name == "JumpGlass")
        {
            if (GoldenB)
            { 
                Anim1.SetBool("isGrounded", false);
                Anim2.SetBool("isGrounded", false);
                Anim1.SetBool("Hold", false);
                Anim2.SetBool("Hold", false);
            }
            else
            {
                GameController.gameover = true;
            }

        }

        if (hit.transform.name == "Solid1")
        {
                Anim1.SetBool("isGrounded", true);
                Anim2.SetBool("isGrounded", true);
        }

        if(hit.transform.name=="GlassC")
        {
            if (GreenB)
            {
                Anim1.Play("Crouch");
                Anim2.Play("Crouch");
            }
            else
            {
                GameController.gameover = true;
            }
        }
        if (hit.transform.name == "SolidFinal")
        {
            //dance
        }



        if (hit.transform.tag == "Bonus")
        {
            Debug.Log("moon");
            Score++;
            score.text = Score.ToString();
            Destroy(hit.gameObject);
        }

        if (hit.transform.name == "GreenTop")
        {

            Destroy(hit.gameObject);
            greentop.SetActive(true); GreenT = true;
            goldentop.SetActive(false); GoldenT = false;
        }

        if (hit.transform.name == "GreenBottom")
        {

            Destroy(hit.gameObject);
            goldenbot.SetActive(false); GoldenB = false;
            greenbot.SetActive(true); GreenB = true;

        }

        if (hit.transform.name == "GoldenTop")
        {

            Destroy(hit.gameObject);
            goldentop.SetActive(true); GoldenT = true;
            greentop.SetActive(false); GreenT = false;

        }

        if (hit.transform.name == "GoldenBottom")
        {

            Destroy(hit.gameObject);
            goldenbot.SetActive(true); GoldenB = true;
            greenbot.SetActive(false); GreenB = false;

        }

        if (hit.transform.name == "FinishLine")
        {

            GameController.Won = true;

        }
    }

    /*
    private void LateUpdate()
    {
        Animator.transform.localPosition = Vector3.zero;
        Animator.transform.localRotation = Quaternion.identity;
    }*/
}
