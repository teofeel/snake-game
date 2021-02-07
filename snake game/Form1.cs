using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace snake_game
{
    public partial class Form1 : Form
    {
        int cols = 50, rows = 25; 
        int score = 0;
        int px = 0, py = 0; // smer kretanja zmije
        int frnt = 0, bck = 0; 
        Body[] snake = new Body[1250]; 
        List<int> available = new List<int>(); 

        bool[,] isvisited; 

        Random rnd = new Random();
        Timer timer = new Timer();

        void initialize()
        {
            isvisited = new bool[rows, cols];
            Body head = new Body((rnd.Next() % cols)*20, (rnd.Next() % rows)*20);
            lblFood.Location = new Point((rnd.Next() % cols) * 20, (rnd.Next() % rows) * 20);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    isvisited[i, j] = false;
                    available.Add(i * cols + j);
                }

            }

            isvisited[head.Location.Y / 20, head.Location.X / 20] = true;
            available.Remove(head.Location.Y/20 * cols + head.Location.X/20);

            Controls.Add(head); 
            snake[frnt] = head;
        }

        void launchtimer() 
        {
            timer.Interval = 50; 
            timer.Tick += move;
            timer.Start(); //game starts
        }

        private void move(object sender, EventArgs e)
        {
            int x = snake[frnt].Location.X;
            int y = snake[frnt].Location.Y;

            if (px == 0 && py == 0)
                return;

            if(Game_Over(x + px, y + py))
            {
                timer.Stop(); 
                MessageBox.Show("Game Over");

                return;
            }

            if(FoodEaten(x + px, y + py))
            {
                score++;
                lbl1.Text = "Score: " + score.ToString();

                if (hits((y + py) / 20, (x + px) / 20))
                    return;

                Body head = new Body(x + px, y + py);

                frnt = (frnt+1249)%1250;
                snake[frnt] = head;
                isvisited[head.Location.Y/20, head.Location.X/20] = true;

                Controls.Add(head);

                Generate_Food();
            }
            else
            {
                if (hits((y + py) / 20, (x + px) / 20))
                    return;

                isvisited[snake[bck].Location.Y / 20, snake[bck].Location.X / 20] = false;

                frnt = (frnt + 1249) % 1250;
                snake[frnt] = snake[bck];
                snake[frnt].Location = new Point(x + px, y + py);
                bck = (bck + 1249) % 1250;

                isvisited[(y + py) / 20, (x + px) / 20] = true;
            }
        }

        void Generate_Food()
        {
            available.Clear();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (!isvisited[i, j])
                        available.Add(i * cols + j);
                }
            }

            int id = rnd.Next(available.Count) % available.Count;

            lblFood.Left = (available[id] * 20) % Width;
            lblFood.Top = (available[id] * 20) / Width * 20 ;
        }

        bool hits(int x, int y) 
        {
            if (isvisited[x,y])
            {
                timer.Stop();
                MessageBox.Show("Game Over");
                return true;
            }
            return false;
        }

        bool FoodEaten(int x, int y) 
        {
            return x == lblFood.Location.X && y == lblFood.Location.Y;
        }

        private void lblFood_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        bool Game_Over(int x, int y) 
        {
            return x < 0 || y < 0 || x > 980 || y > 480;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            px = 0;
            py = 0;

            switch(e.KeyCode)
            {
                case Keys.Right:
                    px = 20;
                    break;
                case Keys.Left:
                    px = -20;
                    break;
                case Keys.Up:
                    py = -20;
                    break;
                case Keys.Down:
                    py = 20;
                    break;
                case Keys.Enter: //ne radi
                    ready();
                    break;
            }
        }

        void ready()
        {
            initialize();
            launchtimer();
        }
        public Form1()
        {
            InitializeComponent();
            ready();
        }

        private void lbl1_Click(object sender, EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}
