﻿using UnityEngine;

[RequireComponent(typeof (Rigidbody))]
public class CharacterController : MonoBehaviour
{
    private Rigidbody _rigidbody;   //кэшированный компонент Rigidbody (чтобы не создавать каждый раз, когда обращаемся)

    [SerializeField]
    private float movingForce = 20.0f;  //сила для передвижения

    [SerializeField]
    private float jumpForce = 80f;  //сила прыжка

    [SerializeField]
    private float maxSlope = 30f;   //Максимальный уклон, по которому может идти персонаж

    private bool onGround = false;  //Стоит ли персонаж на подходящей поверхности (или летит/падает)

    //Инициализация объекта
    private void Awake ()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();  //Находим и запоминаем (кэшируем) компонент Rigidbody
    }
	
	private void Start ()
    {
        //Здесь будет взаимодействие с другими объектами в начале игры, после того, как они уже проинициализировались в их методе Awake () 
        //Например, можно найти инвентарь, и вычесть его вес из скорости перемещения. 
    }
    
    //Коллайдер персонажа прекращает взаимодействие с каким-то другим коллайдером
    private void OnCollisionExit(Collision collision)
    {
        onGround = false;
    }
  
    //Коллайдер персонажа начинает взаимодействие с каким-то другим коллайдером
    private void OnCollisionStay(Collision collision)
    {
        onGround = CheckIsOnGround(collision);
    }

    //Вызывается каждый кадр. Частота может меняться в зависимости от сложности рендеринга и мощности компьтера.
    private void Update ()
    {
        LookAtTarget(); //Поворачиваем персонажа к курсору 
    }

    //Вызывается каждый шаг просчета физики, вне зависимости от FPS (вне зависимости от скорости рендеринга)
    private void FixedUpdate()
    {
        if (onGround)   //если стоим на земле
        {
            ApplyMovingForce(); //прикладываем к персонажу горизонтальную силу, соответствующую осям ввода (кнопкам WSAD или стрелкам)

            if (Input.GetKeyDown(KeyCode.Space))    //Если игрок нажал "пробел"
            {
                _rigidbody.AddForce(Vector3.up * jumpForce);    //прикладываем к Rigidbody силу, направленную вверх, и имеющую величину, равную jumpForce.
            }
        }
    }

    // Проверяем, подходит ли поверхность коллайдера для того, чтобы персонаж на ней стоял.
    //Объект Collision для проверки.
    //return true, если поверхность подходящая, false - если нет.
    private bool CheckIsOnGround(Collision collision)
    {
        for (int i = 0; i < collision.contacts.Length; i++) //Проверяем все точки соприкосновения
        {
            if (collision.contacts[i].point.y < transform.position.y)   //если точка соприкосновения находится ниже центра нашего персонажа
            {
                if (Vector3.Angle(collision.contacts[i].normal, Vector3.up) < maxSlope)   //Если уклон поверхности не превышает допустимое значение
                {
                    return true;    //найдена точка соприкосновения с подходящей поверхностью - выходим из функции, возвращаем значение true.
                }
            }
        }
        return false;   //Подходящая поверхность не найдена, возвращаем значение false.
    }

    // Рассчитываем и прикладываем силу перемещения персонажа в зависимости от значений осей инпута
    private void ApplyMovingForce()
    {
        //При рассчете силы по той или иной оси используются локальные оси персонажа. Transform автоматически предоставляет вектора, соответствующие его текущей оси Z и оси X (и оси Y тоже):
        Vector3 xAxisForce = transform.right * Input.GetAxis("Horizontal"); //определяем силу по оси Х
        Vector3 zAxisForce = transform.forward * Input.GetAxis("Vertical"); //определяем силу по оси Z

        Vector3 resultXZForce = xAxisForce + zAxisForce;    //Складываем вектора

        //Если сложить два перпендикулярных вектора, каждый длиной 1, 
        //получится вектор длиной примерно 1,41... (квадратный корень из двух).
        //То есть персонаж будет быстрее бегать по диагонали, чем строго по одной из осей.
        //Чтобы этого не было, нормализуем результирующий вектор (установим его длину равной 1):
        resultXZForce.Normalize();

        resultXZForce = resultXZForce * movingForce; //умножаем результирующий вектор на силу движения персонажа (задаем скорость)

        _rigidbody.AddForce(resultXZForce); //Прикладываем силу к Rigidbody
    }

    /// Разворачиваем персонажа лицом к курсору
    private void LookAtTarget()
    {
        RaycastHit hit; //заготавливаем переменную, в которую будет выведено значение столкновения луча с коллайдером.

        //Находим главную камеру, и с ее помощью получаем луч, идущий из камеры в ту точку пространства, которая находится под курсором мыши.
        // Input.mousePosition - текущее положение курсора в пространстве экрана (нижний левый угол - 0, 0; верхний правый угол - ширина окна, высота окна)
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    

        //С помощью физического движка запускаем полученный луч
        if (Physics.Raycast(ray, out hit))  //если луч попал в какой-то коллайдер, метод возвращает true, и выводит параметры столкновения в переменную hit (ключевое слово out)
        {
            Vector3 position = ray.GetPoint(hit.distance);  //Находим на луче точку, находящуюся на заданном расстоянии от начала луча. Это расстояние берем из параметров столкновения - переменной hit. 
            position.y = transform.position.y;  //Поскольку точка столкновения может находиться выше или ниже персонажа, приравниваем ее координату Y к координате Y персонажа.
            transform.LookAt(position); //Поворачиваем трансформ персонажа локальной осью Z (forward) в сторону точки столкновения луча с коллайдером.
        }
    }

}
