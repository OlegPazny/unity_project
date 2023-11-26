using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private Transform interactObject; // Текущий объект для взаимодействия
    public Transform inHand; // Объект, который находится в руке
    private Vector3 crysPos;

    [SerializeField]
    private IKAnimation playerIK; // Ссылка на компонент IKAnimation

    private void OnTriggerEnter(Collider other)
    {
        // Если взаимодействуем с предметом и в руке ничего нет
        if ((other.CompareTag("item") && !inHand) || (other.CompareTag("itemForTransfer") && !inHand))
        {
            interactObject = other.transform; // Устанавливаем текущий объект для взаимодействия
            playerIK.StartInteraction(other.gameObject.transform.position); // Запускаем IK-анимацию для объекта
        }
    }

    private void FixedUpdate()
    {
        CheckDistance(); // Проверяем расстояние до объекта
    }

    private void Update()
    {
        // При нажатии правой кнопки мыши выполняем действие с объектом
        if (Input.GetMouseButtonDown(1))
        {
            ThroughItem();
        }
    }

    private void ThroughItem()
    {
        if (inHand != null)
        {
            inHand.transform.parent = null; // Отсоединяем объект от руки
            Rigidbody rb = inHand.GetComponent<Rigidbody>();

            StartCoroutine(ReadyToTake()); // Запускаем корутину для задержки перед следующим взятием объекта
        }
    }

    private IEnumerator ReadyToTake()
    {
        yield return new WaitForSeconds(8); // Задержка в 8 секунд
        inHand = null; // Очищаем объект в руке
    }

    // Проверка расстояния до объекта для взаимодействия
    private void CheckDistance()
    {
        // Если объект для взаимодействия есть и расстояние до него больше 1
        if (interactObject != null && Vector3.Distance(transform.position, interactObject.position) > 1)
        {
            interactObject = null; // Сбрасываем объект для взаимодействия
            playerIK.StopInteraction(); // Останавливаем IK-анимацию
        }
    }

    private void TakeItemInPocket(GameObject item)
    {
        if (inHand)
        {
            Debug.Log("В руке уже что-то находится, положите предмет обратно");
        }
        else
        {
            playerIK.StopInteraction(); // Останавливаем IK-анимацию
            Destroy(item); // Уничтожаем предмет
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("item"))
        {
            TakeItemInPocket(collision.gameObject); // Берем предмет в руку
        }

        // Если сталкиваемся с объектом для передачи и в руке ничего нет
        if (collision.gameObject.CompareTag("itemForTransfer") && !inHand)
        {
            TakeItemInHand(collision.gameObject.transform);
        }
    }
 public Vector3 inHandPosition = new Vector3(0f, 0f, 0f);
   public Vector3 inHandRotation = new Vector3(0f, 0f, 0f);

   // Публичная переменная для настройки кинематичности объекта в руке
   public bool itemKinematic = true;
    // Берем объект в руку
   // Берем объект в руку
   private void TakeItemInHand(Transform item)
   {
       inHand = item; // Устанавливаем объект для взаимодействия
       inHand.parent = transform; // Устанавливаем родителя для объекта (рука)

       // Устанавливаем локальную позицию
       inHand.localPosition = inHandPosition;

       // Устанавливаем локальные углы
       inHand.localEulerAngles = inHandRotation;

       playerIK.StopInteraction(); // Останавливаем IK-анимацию

       Rigidbody rb = inHand.GetComponent<Rigidbody>();
       rb.isKinematic = itemKinematic; // Делаем объект кинематическим
   }

   // Публичные переменные для настройки положения и углов объекта в руке


}
