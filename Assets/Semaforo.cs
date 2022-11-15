using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Semaforo : MonoBehaviour
{
    [SerializeField]
    public bool Vertical = false;

    public Timer timer;

    public SpriteRenderer Verde;
    public SpriteRenderer Amarillo;
    public SpriteRenderer Rojo;

    public Timer.EstadoTimer UltimoEstado;

    // Start is called before the first frame update
    void Start()
    {

        timer = GameObject.Find("Main Camera").GetComponent<Timer>();
        Verde = this.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        Amarillo = this.gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>();
        Rojo = this.gameObject.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>();

        Verde.color = Color.black;
        Amarillo.color = Color.black;
        Rojo.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        SincronizarSemaforo();
    }

    public void SincronizarSemaforo()
    {
        switch(timer.EstadoS)
        {
            case Timer.EstadoSemaforos.SemaforosH :
                if(!Vertical)
                {
                    switch(timer.Estado)
                    {
                        case Timer.EstadoTimer.Verde:
                            Verde.color = Color.green;
                            Amarillo.color = Color.black;
                            Rojo.color = Color.black;
                            UltimoEstado = timer.Estado;
                            break;
                        case Timer.EstadoTimer.VerdeParpadeando:
                            Verde.color = (float)timer.TiempoPasoMedioSegundo/2 % 1 == 0 ? Color.green: Color.black;
                            Amarillo.color = Color.black;
                            Rojo.color = Color.black;
                            UltimoEstado = timer.Estado;
                            break;
                        case Timer.EstadoTimer.Amarillo:
                            Verde.color = Color.black;
                            Amarillo.color = Color.yellow;
                            Rojo.color = Color.black;
                            UltimoEstado = timer.Estado;
                            break;
                        case Timer.EstadoTimer.Rojo:
                            Verde.color = Color.black;
                            Amarillo.color = Color.black;
                            Rojo.color = Color.red;
                            UltimoEstado = timer.Estado;
                            break;
                        case Timer.EstadoTimer.Apagado:
                            if (UltimoEstado == Timer.EstadoTimer.Rojo)
                            {
                                Verde.color = Color.black;
                                Amarillo.color = Color.black;
                                Rojo.color = Color.red;
                            }
                            else
                            {
                                Verde.color = Color.black;
                                Amarillo.color = Color.black;
                                Rojo.color = Color.black;
                            }
                            break;
                    }
                }
                else
                {
                    Verde.color = Color.black;
                    Amarillo.color = Color.black;
                    Rojo.color = Color.red;
                }
                break;

            case Timer.EstadoSemaforos.SemaforosV:
                if (Vertical)
                {
                    switch (timer.Estado)
                    {
                        case Timer.EstadoTimer.Verde:
                            Verde.color = Color.green;
                            Amarillo.color = Color.black;
                            Rojo.color = Color.black;
                            UltimoEstado = timer.Estado;
                            break;
                        case Timer.EstadoTimer.VerdeParpadeando:
                            Verde.color = (float)timer.TiempoPasoMedioSegundo / 2 % 1 == 0 ? Color.green : Color.black;
                            Amarillo.color = Color.black;
                            Rojo.color = Color.black;
                            UltimoEstado = timer.Estado;
                            break;
                        case Timer.EstadoTimer.Amarillo:
                            Verde.color = Color.black;
                            Amarillo.color = Color.yellow;
                            Rojo.color = Color.black;
                            UltimoEstado = timer.Estado;
                            break;
                        case Timer.EstadoTimer.Rojo:
                            Verde.color = Color.black;
                            Amarillo.color = Color.black;
                            Rojo.color = Color.red;
                            UltimoEstado = timer.Estado;
                            break;
                        case Timer.EstadoTimer.Apagado:
                            if (UltimoEstado == Timer.EstadoTimer.Rojo)
                            {
                                Verde.color = Color.black;
                                Amarillo.color = Color.black;
                                Rojo.color = Color.red;
                            }
                            else
                            {
                                Verde.color = Color.black;
                                Amarillo.color = Color.black;
                                Rojo.color = Color.black;
                            }
                            break;
                    }
                }
                else
                {
                    Verde.color = Color.black;
                    Amarillo.color = Color.black;
                    Rojo.color = Color.red;
                }
                break;

            case Timer.EstadoSemaforos.SemaforosVyH:
                switch(timer.Estado)
                {
                    case Timer.EstadoTimer.Apagado:
                        Verde.color = Color.black;
                        Amarillo.color = Color.black;
                        Rojo.color = Color.black;
                        break;

                    case Timer.EstadoTimer.Amarillo:
                        Verde.color = Color.black;
                        Amarillo.color = Color.yellow;
                        Rojo.color = Color.black;
                        break;
                }
                break;
        }
    }
}
