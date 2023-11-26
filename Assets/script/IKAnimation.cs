using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKAnimation : MonoBehaviour
{
    private Animator anim; // Ссылка на компонент Animator для управления анимациями
    private bool interact; // Флаг, указывающий на наличие взаимодействия
    private Vector3 positionForIK; // Позиция, куда будет направлено взаимодействие
    public float weight = 0; // Вес для плавного перехода взаимодействия

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnAnimatorIK() // Вызывается при каждом обновлении кадра, когда работает система IK
    {
        if (interact)
        {
            // Увеличиваем вес постепенно от 0 до 1 для плавного включения IK
            if (weight < 1) weight += 0.01f;

            // Устанавливаем позицию правой руки для IK
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, weight);
            anim.SetIKPosition(AvatarIKGoal.LeftHand, positionForIK);

            // Устанавливаем вес для взгляда и его позицию
            anim.SetLookAtWeight(weight);
            anim.SetLookAtPosition(positionForIK);
        }
        else if (weight > 0) // Если взаимодействие завершено, уменьшаем вес для плавного выключения IK
        {
            weight -= 0.02f;

            // Сбрасываем вес и позицию правой руки для IK
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, weight);
            anim.SetIKPosition(AvatarIKGoal.LeftHand, positionForIK);

            // Сбрасываем вес и позицию для взгляда
            anim.SetLookAtWeight(weight);
            anim.SetLookAtPosition(positionForIK);
        }
    }

    // Метод для начала взаимодействия, принимает позицию, куда будет направлено взаимодействие
    public void StartInteraction(Vector3 pos)
    {
        positionForIK = pos;
        interact = true;
    }

    // Метод для завершения взаимодействия
    public void StopInteraction()
    {
        interact = false;
    }
}
