using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Simulacion_Semaforos
{
    public partial class Form1 : Form
    {

        Timer timer1 = new Timer();
        int tiempoRojo;   // Tiempo en segundos para el color rojo
        int tiempoAmarillo; // Tiempo en segundos para el color amarillo
        int tiempoVerde ;   // Tiempo en segundos para el color verde
        int tiempoActual;       // Tiempo restante en el estado actual
        private enum EstadoSemaforo { Rojo, Amarillo, Verde }
        EstadoSemaforo estadoSemaforo1 = EstadoSemaforo.Rojo;
        EstadoSemaforo estadoSemaforo2 = EstadoSemaforo.Verde;
        EstadoSemaforo estadoSemaforo3 = EstadoSemaforo.Rojo;
        EstadoSemaforo estadoSemaforo4 = EstadoSemaforo.Verde;
        SoundPlayer SonidoSemaforo = new SoundPlayer("C:\\Users\\usuario\\source\\repos\\Simulacion_Semaforos\\Simulacion_Semaforos\\Resources\\light-switch-82388.wav");
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // Configuración inicial del Timer
            timer1.Interval = 500; // Intervalo de un segundo
            timer1.Tick += Timer1_Tick; // Asocia el evento Tick al método Timer1_Tick
            // Hacer redondos los PictureBox de ambos semáforos
            MakeCircular(pictureBoxrojo1);
            MakeCircular(pictureBoxamarillo1);
            MakeCircular(pictureBoxverde1);
            MakeCircular(pictureBoxrojo2);
            MakeCircular(pictureBoxamarillo2);
            MakeCircular(pictureBoxverde2);
            MakeCircular(pictureBoxrojo3);
            MakeCircular(pictureBoxamarillo3);
            MakeCircular(pictureBoxverde3);
            MakeCircular(pictureBoxrojo4);
            MakeCircular(pictureBoxamarillo4);
            MakeCircular(pictureBoxverde4);
            // Inicializa el tiempo actual
            tiempoActual = tiempoRojo;
        }
        private void MakeCircular(PictureBox pictureBox)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, pictureBox.Width, pictureBox.Height);
            pictureBox.Region = new Region(path);
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            tiempoActual--;

            if (tiempoActual <= 0)
            {
                CambiarEstadoSemaforos();
            }
            ActualizarColores();
        }
        private void CambiarEstadoSemaforos()
        {
            // Secuencia para cambiar el estado de los semáforos
            if (estadoSemaforo1 == EstadoSemaforo.Rojo && estadoSemaforo2 == EstadoSemaforo.Verde)
            {
                // Semáforos 2 y 4 pasan de verde a amarillo
                estadoSemaforo2 = EstadoSemaforo.Amarillo;
                estadoSemaforo4 = EstadoSemaforo.Amarillo;
                tiempoActual = tiempoAmarillo;
                SonidoSemaforo.Play();
            }
            else if (estadoSemaforo1 == EstadoSemaforo.Rojo && estadoSemaforo2 == EstadoSemaforo.Amarillo)
            {
                // Semáforos 2 y 4 pasan de amarillo a rojo, y semáforos 1 y 3 de rojo a verde
                estadoSemaforo2 = EstadoSemaforo.Rojo;
                estadoSemaforo4 = EstadoSemaforo.Rojo;
                estadoSemaforo1 = EstadoSemaforo.Verde;
                estadoSemaforo3 = EstadoSemaforo.Verde;
                tiempoActual = tiempoVerde;
                SonidoSemaforo.Play();
            }
            else if (estadoSemaforo1 == EstadoSemaforo.Verde)
            {
                // Semáforos 1 y 3 pasan de verde a amarillo
                estadoSemaforo1 = EstadoSemaforo.Amarillo;
                estadoSemaforo3 = EstadoSemaforo.Amarillo;
                tiempoActual = tiempoAmarillo;
                SonidoSemaforo.Play();
            }
            else if (estadoSemaforo1 == EstadoSemaforo.Amarillo)
            {
                // Semáforos 1 y 3 pasan de amarillo a rojo, y semáforos 2 y 4 de rojo a verde
                estadoSemaforo1 = EstadoSemaforo.Rojo;
                estadoSemaforo3 = EstadoSemaforo.Rojo;
                estadoSemaforo2 = EstadoSemaforo.Verde;
                estadoSemaforo4 = EstadoSemaforo.Verde;
                tiempoActual = tiempoRojo;
                SonidoSemaforo.Play();
            }
      
        }

        private void ActualizarColores()
        { 
            // Semáforo 1
            pictureBoxrojo1.BackColor = estadoSemaforo1 == EstadoSemaforo.Rojo ? Color.Red : Color.Gray;
            pictureBoxamarillo1.BackColor = estadoSemaforo1 == EstadoSemaforo.Amarillo ? Color.Yellow : Color.Gray;
            pictureBoxverde1.BackColor = estadoSemaforo1 == EstadoSemaforo.Verde ? Color.Green : Color.Gray;

            // Semáforo 2
            pictureBoxrojo2.BackColor = estadoSemaforo2 == EstadoSemaforo.Rojo ? Color.Red : Color.Gray;
            pictureBoxamarillo2.BackColor = estadoSemaforo2 == EstadoSemaforo.Amarillo ? Color.Yellow : Color.Gray;
            pictureBoxverde2.BackColor = estadoSemaforo2 == EstadoSemaforo.Verde ? Color.Green : Color.Gray;

            // Semáforo 3
            pictureBoxrojo3.BackColor = estadoSemaforo3 == EstadoSemaforo.Rojo ? Color.Red : Color.Gray;
            pictureBoxamarillo3.BackColor = estadoSemaforo3 == EstadoSemaforo.Amarillo ? Color.Yellow : Color.Gray;
            pictureBoxverde3.BackColor = estadoSemaforo3 == EstadoSemaforo.Verde ? Color.Green : Color.Gray;

            // Semáforo 4
            pictureBoxrojo4.BackColor = estadoSemaforo4 == EstadoSemaforo.Rojo ? Color.Red : Color.Gray;
            pictureBoxamarillo4.BackColor = estadoSemaforo4 == EstadoSemaforo.Amarillo ? Color.Yellow : Color.Gray;
            pictureBoxverde4.BackColor = estadoSemaforo4 == EstadoSemaforo.Verde ? Color.Green : Color.Gray;
        }
        private void ApagarSemaforos()
        {
            // Poner todas las luces en gris para simular que están apagadas
            pictureBoxrojo1.BackColor = Color.Gray;
            pictureBoxamarillo1.BackColor = Color.Gray;
            pictureBoxverde1.BackColor = Color.Gray;

            pictureBoxrojo2.BackColor = Color.Gray;
            pictureBoxamarillo2.BackColor = Color.Gray;
            pictureBoxverde2.BackColor = Color.Gray;

            pictureBoxrojo3.BackColor = Color.Gray;
            pictureBoxamarillo3.BackColor = Color.Gray;
            pictureBoxverde3.BackColor = Color.Gray;

            pictureBoxrojo4.BackColor = Color.Gray;
            pictureBoxamarillo4.BackColor = Color.Gray;
            pictureBoxverde4.BackColor = Color.Gray;
            // Detener el temporizador para detener la simulación
            timer1.Stop();
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            // Validar y asignar los tiempos ingresados por el usuario
            if (int.TryParse(txtRojo.Text, out int tiempoRojoUsuario))
            {
                tiempoRojo = tiempoRojoUsuario;
            }
            else
            {
                MessageBox.Show("Por favor, ingresa un valor válido para el tiempo en rojo.");
                return;
            }

            if (int.TryParse(txtAmarillo.Text, out int tiempoAmarilloUsuario))
            {
                tiempoAmarillo = tiempoAmarilloUsuario;
            }
            else
            {
                MessageBox.Show("Por favor, ingresa un valor válido para el tiempo en amarillo.");
                return;
            }

            if (int.TryParse(txtVerde.Text, out int tiempoVerdeUsuario))
            {
                tiempoVerde = tiempoVerdeUsuario;
            }
            else
            {
                MessageBox.Show("Por favor, ingresa un valor válido para el tiempo en verde.");
                return;
            }
            tiempoActual = tiempoRojo; // Inicializa el tiempo actual para el primer estado
            timer1.Start(); // Inicia el Timer y comienza a ejecutar Timer1_Tick cada segundo
        }

        private void btnDetener_Click(object sender, EventArgs e)
        {
            timer1.Stop(); // Detiene el Timer
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {   
            // Detener el temporizador para detener la simulación
            ApagarSemaforos();
        }

        private void pictureBoxrojo2_Click(object sender, EventArgs e)
        { }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            //Limpia los texBox por si el usuario desea ingresar nuevos tiempos para los semaforos
            txtRojo.Text = "";
            txtAmarillo.Text = "";
            txtVerde.Text = "";
        }
    }
}
