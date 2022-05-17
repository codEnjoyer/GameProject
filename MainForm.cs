﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using GameProject.Domain;
using GameProject.Entities;
using GameProject.Physics;
using Timer = System.Windows.Forms.Timer;

namespace GameProject
{
    public partial class MainForm : Form
    {
        private Game game;
        private Timer timer;
        private Label testLabel;
        public MainForm()
        {
            InitializeComponent();
            var center = new Vector(Screen.PrimaryScreen.WorkingArea.Width / 2,
                Screen.PrimaryScreen.WorkingArea.Height / 2);
            var gameSize = new Size(4000, 4000);
            var gameZone = new Rectangle(new Point((int)(-gameSize.Width / 2 + center.X),
                    (int)(-gameSize.Height / 2 + center.Y)),
                gameSize);

            var playerSpawnPoint = new Vector(center.X, center.Y + 2);
            View.Location = playerSpawnPoint;

            game = new Game(new Player(playerSpawnPoint), gameZone);

            

            timer = new Timer();

            timer.Interval = 15;
            timer.Tick += (sender, args) => Invalidate();
            timer.Start();

            Cursor = Cursors.Cross;
            
            KeyDown += MainForm_KeyDown;
            KeyUp += MainForm_KeyUp;
            MouseMove += MainForm_MouseMove;
            MouseClick += MainForm_MouseClick;

            testLabel = new Label()
            {
                Location = new Point(50, 50),
                Size = new Size(90, 30),
            };
            Controls.Add(testLabel);

            KeyDown += (s, e) =>
            {
                testLabel.Text = "Camera offset: " + View.Offset;
            };
        }


        private static void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            Game.KeyPressed = true;
            Controller.ControlKeys(e.KeyCode, true);
            
        }

        private static void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            Controller.ControlKeys(e.KeyCode, false);
        }

        private static void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            Controller.ControlMouse(e);
        }

        private static void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
            Controller.ControlMouse(e);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            var eGraphics = e.Graphics;
            View.UpdateTextures(eGraphics);
        }

        #region Fullscreen, FormLoad
        private void MainForm_Load(object sender, EventArgs e)
        {
            GoFullscreen(true);
            //GameZone = Screen.PrimaryScreen.WorkingArea.GameZone;
            DoubleBuffered = true;
        }


        //TODO : Move to View.cs
        private void GoFullscreen(bool fullscreen)
        {
            if (fullscreen)
            {
                //TopMost = true;
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                FormBorderStyle = FormBorderStyle.Sizable;
                WindowState = FormWindowState.Normal;
                Bounds = Screen.PrimaryScreen.Bounds;
            }
        }
        #endregion
    }
}
