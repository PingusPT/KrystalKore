using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringMovement : MonoBehaviour
{

    private float velocity = 0f;
    private float force = 0f;

    private float height = 0f;

    private float target_height = 0f;

    public float spread = 0.006f;


    [SerializeField] private float springStiffeness = 0.1f;
    [SerializeField] private float dampening = 0.03f;

    [SerializeField] List<SpringMovement> springs = new();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        foreach(SpringMovement springMovementComponent in springs)
        {
            springMovementComponent.WaveSpringUpdate(springStiffeness, dampening);
        }

        UpdateString();
    }


    public void WaveSpringUpdate(float springStiffness, float dampening)
    {
        height = transform.localPosition.y;

        var x = height - target_height;
        var loss = -dampening * velocity;

        force = -springStiffness * x + loss;
        velocity += force;
        var y = transform.localPosition.y;
        transform.localPosition = new Vector3(transform.localPosition.x, y + velocity, transform.localPosition.z);
    }

    private void UpdateString()
    {
        int count = springs.Count;
        float[] left_deltas = new float[count];
        float[] right_deltas = new float[count];

        for (int i = 0; i < count; i++)
        {
            if(i>0)
            {
                left_deltas[i] = spread * (springs[i].height - springs[i - 1].height);
                springs[i - 1].velocity += left_deltas[i];
            }
            if(i < springs.Count - 1)
            {
                right_deltas[i] = spread * (springs[i].height - springs[i + 1].height);
                springs[i + 1].velocity += right_deltas[i];
            }
        }
    }

    private void Splash(int index, float speed)
    {
        if(index>= 0 && index < springs.Count)
        {
            springs[index].velocity += speed;
        }
    }
}
