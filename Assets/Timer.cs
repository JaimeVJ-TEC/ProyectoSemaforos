using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public enum EstadoOpcion
    {
        Inicio,
        Preventivo,
        Apagado
    }

    public enum EstadoTimer
    {
        Verde,
        VerdeParpadeando,
        Amarillo,
        Rojo,
        Apagado
    }

    public enum EstadoSemaforos
    {
        SemaforosV,
        SemaforosH,
        SemaforosVyH,
        Apagados
    }

    [SerializeField]
    public EstadoTimer Estado = EstadoTimer.Apagado;

    [SerializeField]
    public EstadoOpcion Opcion = EstadoOpcion.Apagado;

    [SerializeField]
    public EstadoSemaforos EstadoS = EstadoSemaforos.Apagados;


    [SerializeField]
    int TiempoVerde = 15;

    [SerializeField]
    int TiempoVerdeParpadeando = 3;

    [SerializeField]
    int TiempoAmarillo = 3;

    [SerializeField]
    int TiempoRojo = 2;

    public int TiempoPasoMedioSegundo = 0;
    public float halfSecond = 0.5f;

    bool vertical = true;
    public Text Texto;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TimerTick();
    }

    public void SetTimerText(int Time, EstadoTimer E)
    {
        switch(E)
        {
            case EstadoTimer.Verde:
                Texto.text = (Time + 1).ToString();
                break;
            case EstadoTimer.VerdeParpadeando:
                Texto.text = (Time + 1 - TiempoVerde).ToString();
                break;
            case EstadoTimer.Amarillo:
                Texto.text = (Time + 1 - TiempoVerde - TiempoVerdeParpadeando).ToString();
                break;
            case EstadoTimer.Rojo:
                Texto.text = (Time + 1 - TiempoVerde - TiempoVerdeParpadeando - TiempoAmarillo).ToString();
                break;
            case EstadoTimer.Apagado:
                Texto.text = "0";
                break;
        }
    }

    public void SetTimerColor()
    {
        switch(Estado)
        {
            case EstadoTimer.Verde:
                Texto.color = Color.green;
                break;
            case EstadoTimer.VerdeParpadeando:
                Texto.color = (float)TiempoPasoMedioSegundo / 2 % 1 == 0 ? Color.green : Color.black;
                break;
            case EstadoTimer.Amarillo:
                Texto.color = Color.yellow;
                break;
            case EstadoTimer.Rojo:
                Texto.color = Color.red;
                break;
            case EstadoTimer.Apagado:
                Texto.color = Color.black;
                break;
        }
    }

    public void TimerTick()
    {
        switch(Opcion)
        {
            case EstadoOpcion.Inicio:
                GetHalfStep();
                EstadoS = vertical ? EstadoSemaforos.SemaforosV : EstadoSemaforos.SemaforosH;

                if (TiempoPasoMedioSegundo < (TiempoVerde * 2) - 1)
                {
                    Estado = EstadoTimer.Verde;
                    SetTimerText(TiempoPasoMedioSegundo / 2,Estado);
                }
                else if (TiempoPasoMedioSegundo < TiempoVerde * 2)
                {
                    Estado = EstadoTimer.Apagado;
                    SetTimerText(TiempoPasoMedioSegundo / 2, EstadoTimer.Verde);
                }
                else if (TiempoPasoMedioSegundo - (TiempoVerde * 2) < TiempoVerdeParpadeando * 2)
                {
                    Estado = EstadoTimer.VerdeParpadeando;
                    SetTimerText(TiempoPasoMedioSegundo / 2, Estado);
                }
                else if (TiempoPasoMedioSegundo - ((TiempoVerde + TiempoVerdeParpadeando) * 2) < TiempoAmarillo * 2 - 1)
                {
                    Estado = EstadoTimer.Amarillo;
                    SetTimerText(TiempoPasoMedioSegundo / 2, Estado);
                }
                else if (TiempoPasoMedioSegundo - ((TiempoVerde + TiempoVerdeParpadeando) * 2) < TiempoAmarillo * 2)
                {
                    Estado = EstadoTimer.Apagado;
                    SetTimerText(TiempoPasoMedioSegundo / 2, EstadoTimer.Amarillo);
                }
                else if (TiempoPasoMedioSegundo - ((TiempoVerde + TiempoVerdeParpadeando + TiempoAmarillo) * 2) < TiempoRojo * 2 - 1)
                {
                    Estado = EstadoTimer.Rojo;
                    SetTimerText(TiempoPasoMedioSegundo / 2, Estado);
                }
                else if (TiempoPasoMedioSegundo - ((TiempoVerde + TiempoVerdeParpadeando + TiempoAmarillo) * 2) < TiempoRojo * 2)
                {
                    Estado = EstadoTimer.Apagado;
                    SetTimerText(TiempoPasoMedioSegundo / 2, EstadoTimer.Rojo);
                }
                else
                {
                    TiempoPasoMedioSegundo = 0;
                    vertical = !vertical;
                }
                SetTimerColor();
                break;

            case EstadoOpcion.Preventivo:
                GetHalfStep();
                EstadoS = EstadoSemaforos.SemaforosVyH;
                Estado = ((float)TiempoPasoMedioSegundo / 2) % 1 == 0 ? EstadoTimer.Amarillo : EstadoTimer.Apagado;
                SetTimerColor();
                SetTimerText(-1 + TiempoVerde + TiempoVerdeParpadeando, Estado);
                break;

            case EstadoOpcion.Apagado:
                vertical = true;
                TiempoPasoMedioSegundo = 0;
                halfSecond = 0.5f;
                EstadoS = EstadoSemaforos.SemaforosVyH;
                Estado = EstadoTimer.Apagado;
                SetTimerColor();
                SetTimerText(0, Estado);
                break;
        }
    }

    public void GetHalfStep()
    {
        halfSecond -= Time.deltaTime;
        TiempoPasoMedioSegundo += halfSecond < 0 ? 1 : 0;
        halfSecond = halfSecond < 0 ? 0.5f : halfSecond;
    }
}
