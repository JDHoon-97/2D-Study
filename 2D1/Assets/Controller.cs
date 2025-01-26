using UnityEngine;

public class Controller : MonoBehaviour
{
    //생성자
    //유니티의 MonoBehaviour는 생성자를 사용할 수 없다.
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    
    void Start()
    {
        //게임오브젝트는 기본적으로 하나의 트랜스폼을 가지고 있다.
        
        //생성자처럼 사용할 수 있다.
    }

    // Update is called once per frame
    void Update()
    {
        //사용자마다 프레임의 수행 속도가 다르다.
        // 프레임 속도가 빠른 경우 1초에 1프레임 >> 3초에 3프레임 >> 3칸 움직임
        //'' 느린 경우 3초에 1프레임 >> 1칸 움직임

        if (Input.GetKey(KeyCode.A))
        {
            //x, y, z 값을 float 형태로 가지고 있는 구조체
            Vector3 position = transform.position;
            
            //실행 속도에 상관 없이 일정하게 움직이게 하는 역할
            position.x -= 1 * Time.deltaTime;
            transform.position = position;
            //자식 >> 부모의 공가능로 변환
            //Matrix4x4 matrix = transform.parent.worldToLocalMatrix * transform.worldToLocalMatrix;
            
            
            //Matrix4x4 positionMatrix =Matrix4x4.Translate(position);
            //Matrix4x4 scaleMatrix = Matrix4x4.Scale(transform.localScale);
            //Matrix4x4 rotationMatrix = Matrix4x4.Rotate(transform.rotation);

            //Matrix4x4.TRS(positionMatrix, scaleMatrix, rotationMatrix);
            //행렬
            //Matrix4x4 matrix = transform.localToWorldMatrix;

           //Debug.Log(message: "Press A");
        }

        if (Input.GetKey(KeyCode.D))
        {
            Vector3 position = transform.position;
            position.x += 1 * Time.deltaTime;
            transform.position = position;
        }
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 position = transform.position;
            position.y += 1 * Time.deltaTime;
            transform.position = position;
        }
        if (Input.GetKey(KeyCode.S))
        {
            Vector3 position = transform.position;
            position.y -= 1 * Time.deltaTime;
            transform.position = position;
        }
    }
}
