using UnityEngine;

public class PlayerAnimate : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        // Получаем компонент аниматора
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Проверяем нажатие клавиш W, A, S, D
        bool isWalking = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        bool isRunning = isWalking && Input.GetKey(KeyCode.LeftShift);
      //  bool isInteracting = Input.GetKey(KeyCode.E);
        bool isJumping = Input.GetKey(KeyCode.Space);
        // Устанавливаем переменную "walk" в аниматоре в зависимости от состояния isWalking
        animator.SetBool("walk", isWalking);
        animator.SetBool("run", isRunning);



        // Если игрок начал ходить, активируем триггер "startWalk"
        if (isWalking)
        {
            animator.SetTrigger("startWalk");
        }
         else
                {
                    animator.SetBool("startWalk", false);
                }
         // Если игрок начал бежать, активируем триггер "startRun"
                if (isRunning)
                {
                    animator.SetTrigger("startRun");
                }
                else
                                {
                                    animator.SetBool("startRun", false);
                                }



               // Устанавливаем переменную "jump" в аниматоре в зависимости от состояния isJumping
               animator.SetBool("jump", isJumping);

               // Если игрок начал прыгать, активируем триггер "startJump"
               if (isJumping)
               {
            
                   animator.SetTrigger("startJump");
               }
               else
               {
                   animator.SetBool("startJump", false);
               }



                   // Можно также создать метод для включения маски, если это необходимо
                  


// animator.SetBool("interaction", isInteracting);
//
//               if (isInteracting)
//                      {
//                          animator.SetTrigger("startInteraction");
//                      }
//                      else
//                      {
//                          animator.SetBool("startInteraction", false);
//                      }
    }
}
