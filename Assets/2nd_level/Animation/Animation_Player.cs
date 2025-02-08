using UnityEngine;

public class Animation_Player : MonoBehaviour
{
    private Animator animator;  // Ссылка на компонент Animator
    private float horizontalInput;  // Для получения ввода по горизонтали

    void Start()
    {
        // Получаем компонент Animator на этом объекте
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Получаем горизонтальное движение (например, от клавиш A и D или стрелок)
        horizontalInput = Input.GetAxis("Horizontal");

        // Если игрок движется, переключаем на анимацию движения
        if (horizontalInput != 0)
        {
            animator.SetBool("IsMoving", true);  // Переключаем на анимацию движения
        }
        else
        {
            animator.SetBool("IsMoving", false); // Переключаем на анимацию остановки
        }
    }
}

