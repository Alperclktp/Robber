using UnityEngine;
using UnityEngine.AI;

public class FootSteps : MonoBehaviour
{
    [SerializeField] public ParticleSystem footStepsEffect;

    [SerializeField] private float delta;

    [SerializeField] private float gap;

    private Vector3 lastEmit;

    private int dir = 1;

    private void Start() => lastEmit = transform.position;

    private void Update()
    {
        FootStep();
    }

    private void FootStep()
    {
        if (Vector3.Distance(lastEmit, transform.position) > delta)
        {
            LeftStep();

            RightStep();
        }
    }

    private void LeftStep()
    {
        ParticleSystem.EmitParams leftEp = new ParticleSystem.EmitParams();

        var leftPos = transform.position + (-transform.right * gap * dir);

        leftEp.position = leftPos;

        leftEp.rotation = transform.rotation.eulerAngles.y;

        leftEp.position = new Vector3(leftEp.position.x, leftEp.position.y + 0.1f, leftEp.position.z);

        footStepsEffect.Emit(leftEp, 10);
    }

    private void RightStep()
    {
        var rightPos = transform.position + (transform.right * gap * dir);

        ParticleSystem.EmitParams rightEp = new ParticleSystem.EmitParams();

        rightEp.position = rightPos;

        rightEp.rotation = transform.rotation.eulerAngles.y;

        rightEp.position = new Vector3(rightEp.position.x, rightEp.position.y + 0.1f, rightEp.position.z);

        footStepsEffect.Emit(rightEp, 10);

        lastEmit = transform.position;
    }
}

