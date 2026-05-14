using System.Collections;
using UnityEngine;

public partial class Basketball_Game_Manager : MonoBehaviour
{
    [SerializeField] GameObject ball_prefeb;
    [SerializeField] float ball_gravity;


    public void Ball_Animation_Of_Pass(Vector2 start_position , Vector2 end_position , float time , float spin_degree)
    {
        GameObject ball = Instantiate(ball_prefeb);

        StartCoroutine(Ball_Direct_Move(ball, start_position, end_position, time , spin_degree));
        Destroy(ball, time + 0.05f);
        
    }

    private IEnumerator Ball_Direct_Move(GameObject ball , Vector2 start_position , Vector2 end_position , float duration_time , float spin_degree)
    {
        ball.transform.position = Utility.Vector3(start_position, 0);
        float time = 0;
        Vector2 distance = end_position - start_position;

        while (time <= duration_time)
        {
            
            time += Time.deltaTime;
            yield return null;

            float current_x = distance.x * (time / duration_time);
            float current_y = distance.y * (time / duration_time);
            ball.transform.position = new Vector3(start_position.x + current_x, start_position.y +current_y, 0);
            ball.transform.rotation = Quaternion.Euler(0, 0, time * spin_degree);
        }

    }


    public void Ball_Animation_Of_Parabola(Vector2 start_position, Vector2 end_position, out float time, float spin_degree)
    {
        GameObject ball = Instantiate(ball_prefeb);
        float a, b, c;
        float duration_time = Mathf.Abs((end_position.x - start_position.x)/6);
        time = duration_time;
        Get_Quadratic_Function_Of_Shoot(start_position, end_position, out a, out b, out c);

        StartCoroutine(Ball_Parabola_Move(ball ,start_position, end_position, a, b, c, duration_time, spin_degree));

        Destroy(ball, duration_time + 0.05f);
    }

    public void Ball_Animation_Of_Shoot(Vector2 start_position, Vector2 end_position, out float time, float spin_degree) => Ball_Animation_Of_Parabola(start_position, end_position, out time, spin_degree);


    public void Ball_Animation_Of_Rebound(Vector2 start_position, Vector2 end_position, out float time, float spin_degree) => Ball_Animation_Of_Parabola(start_position, end_position, out time, spin_degree);
    

    private IEnumerator Ball_Parabola_Move(GameObject ball, Vector2 start_position, Vector2 end_position, float a, float b, float c, float duration_time, float spin_degree)
    {
        ball.transform.position = Utility.Vector3(start_position, 0);
        float time = 0;
        Vector2 distance = end_position - start_position;

        while (time <= duration_time)
        {
            
            time += Time.deltaTime;
            yield return null;
            float current_x = start_position.x+(distance.x*time/duration_time);
            float current_y = Get_Height_Of_Shooting_Ball(current_x, a, b, c);
            ball.transform.position = new Vector3(current_x , current_y , 0);
            ball.transform.rotation = Quaternion.Euler(0, 0, time * spin_degree);
        }


    }

    private float Get_Height_Of_Shooting_Ball(float current_x , float a , float b , float c)
    {
        return a * current_x * current_x + b * current_x + c;

    }

    private void Get_Quadratic_Function_Of_Shoot(Vector2 start_position, Vector2 end_position, out float a, out float b, out float c)
    {
        float x2_minus_x1 = end_position.x - start_position.x;
        float x2_plus_x1 = end_position.x + start_position.x;



        float peek_position_x = (end_position.y - start_position.y + ball_gravity * (x2_minus_x1) * (x2_plus_x1)) / (ball_gravity * 2 * (x2_minus_x1));
        float peek_position_y = end_position.y + ball_gravity * (end_position.x - peek_position_x) * (end_position.x - peek_position_x);

        a = -ball_gravity;
        b = ball_gravity* 2* peek_position_x;
        c = -(ball_gravity) * (peek_position_x) * (peek_position_x) + peek_position_y;
    }

    
}
