using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {

    private Transform elevator;
    Vector3 finalPosition;
    Vector3 initialPosition;
    public Vector3 deltaPosition;
    //public float liftHeight;
    public float speed = 1f;
    public bool onElevator = false;
    public bool finalPosElevator = false;
    public bool initialPosElevator = true;
    //public bool loopElevator = false;
    //public float delayElevator = 1; // delay beetween movements


    void Awake()
    {
        //
        elevator = this.transform;
        initialPosition = elevator.position; // guarda a posicao inicial do elevador
        //finalPosition = elevator.position + (Vector3.up * liftHeight); //calcula a posicao final
        finalPosition = elevator.position + deltaPosition;
        Debug.Log("Elevador de " + initialPosition + " ate " + finalPosition);
    }

    void FixedUpdate()
    {
        if (onElevator && !finalPosElevator) //se esta no elevador e nao chegou ao final do movimento
        {
            ElevatorMoveToFinal();
        }
        else
        {
            //Debug.Log()
        }
        if (finalPosElevator)
        {
            ElevatorMoveToStart();
        }
    }

    void ElevatorMoveToFinal()
    {
        if (elevator.position == finalPosition) //confere se o objeto já está na posicao final...
        {
            Debug.Log("Chegou na posicao final!");
            finalPosElevator = true;
            initialPosElevator = false;

        }
        else // ...senao efetua o movimento
        {
            float step = speed * Time.deltaTime; // calcula o pequeno incremento pra funcao MoveTowards
                                                 //Debug.Log(step);
            elevator.position = Vector3.MoveTowards(elevator.position, finalPosition, step);
        }
    }

    void ElevatorMoveToStart()
    {
        if (elevator.position == initialPosition) //confere se o objeto já está na posicao inicial...
        {
            Debug.Log("Chegou na posicao inicial!");
            initialPosElevator = true;
            finalPosElevator = false;

        }
        else // ...senao efetua o movimento
        {
            float step = speed * Time.deltaTime; // calcula o pequeno incremento pra funcao MoveTowards
                                                 //Debug.Log(step);
            elevator.position = Vector3.MoveTowards(elevator.position, initialPosition, step);
        }
    }

    void OnTriggerEnter (Collider other)
    {
	    if (other.CompareTag("Player"))
            {
                onElevator = true;
                //Debug.Log("Player Encostou no elevador");
            }
	}

	void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onElevator = false;
            //Debug.Log("Player Desencostou do elevador");
        }
    }
}
