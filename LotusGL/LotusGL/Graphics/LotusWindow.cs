using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace LotusGL.Graphics
{
    class LotusWindow
    {
        //GameEngine Variables
        int framenum = 0;

        //Drawing Variables
        List<iDrawable> drawables;

        //Camera Variables
        int cameraState = 1;
        Vector2 center = new Vector2(256, 256);
        float camDistance, camPitch, camAngle;        

        //Input Variables
        bool rightPressed = false;
        bool leftPressed = false;
        int dw, dx, dy = 0;

        public LotusWindow()
        {
            camDistance = 1000;
            camPitch = (float)Math.PI / 8.0f;
            camAngle = (float)(framenum) / 1000;
        
            GameWindow window = new GameWindow(500, 500);
            GL.ClearColor(1.0f, 0.0f, 0.0f, 1.0f);

            window.Title = "Group 15 - Lotus";
            window.RenderFrame += onRender;
            window.Resize += onResize;
            window.Mouse.ButtonDown += this.onMouseButton;
            window.Mouse.ButtonUp += this.onMouseButton;
            window.Mouse.Move += this.onMouseMove;
            window.Mouse.WheelChanged += this.onWheelChanged;
            window.MouseLeave += this.onMouseLeave;


            drawables = new List<iDrawable>();   
            drawables.Add(new Board());
            Load();

            GL.Enable(EnableCap.DepthTest);
            GL.CullFace(CullFaceMode.Front);
            window.Run(60);
        }
        void Load()
        {
            foreach(iDrawable d in drawables)
            {
                d.Load();
            }
        }
        void UnLoad()
        {
            foreach (iDrawable d in drawables)
            {
                d.UnLoad();
            }
        }

        
        void onRender(object sender, FrameEventArgs e)
        {
            framenum++;
            GameWindow window = (GameWindow)sender;


            GL.Clear(ClearBufferMask.ColorBufferBit|ClearBufferMask.DepthBufferBit);

            

            Matrix4 world, view, proj;
            
            world = Matrix4.Identity;
            float aspect = window.Width / window.Height;
            proj = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 8, aspect, 1f, 10000f);

            if(cameraState == 1)
            {
                if (rightPressed)
                {
                    camAngle += (float)dx / 100;
                    camPitch += (float)dy / 100;
                    camPitch = (float) Math.Min(camPitch, Math.PI / 2 - 0.3);
                    camPitch = (float) Math.Max(camPitch, 0.3);
                }
                //Mouse Wheel
                camDistance -= dw*70;
                camDistance = (float) Math.Min(camDistance, 9000);
                camDistance = (float) Math.Max(camDistance, 100);

                view = Matrix4.LookAt(
                    new Vector3(
                        center.X + camDistance * (float)(Math.Cos(camPitch) * Math.Cos(camAngle)),
                        center.Y + camDistance * (float)(Math.Cos(camPitch) * Math.Sin(camAngle)),
                        (float)Math.Sin(camPitch) * camDistance), 
                    new Vector3(256, 256, 0), 
                    new Vector3(0, 0, 1)
                );
                
            }
            else
            {
                view = Matrix4.LookAt(new Vector3(0, 0, 100), new Vector3(256, 256, 0), new Vector3(0, 1, 0));
            }

            Matrix4 cam = world * view * proj;
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref cam);
            
            foreach (iDrawable d in drawables)
            {
                d.Draw();
            }
            
            window.SwapBuffers();
            dw = dx = dy = 0;
        }

        void onResize(object sender, EventArgs e)
        {
            GameWindow window = (GameWindow)sender;
            Console.WriteLine(window.Width);
            GL.Viewport(0, 0, window.Height, window.Width);
        }
        
        void onMouseMove(object sender, OpenTK.Input.MouseMoveEventArgs e)
        {
            dx = e.XDelta;
            dy = e.YDelta;
        }

        void onMouseButton(object sender, OpenTK.Input.MouseButtonEventArgs e)
        {
            rightPressed = e.Button == OpenTK.Input.MouseButton.Right && e.IsPressed;
            leftPressed = e.Button == OpenTK.Input.MouseButton.Left && e.IsPressed;
        }

        void onWheelChanged(object sender, OpenTK.Input.MouseWheelEventArgs e)
        {
            dw = e.Delta;
        }

        void onMouseLeave(object sender, EventArgs e)
        {
            rightPressed = leftPressed = false;
        }

    }
}
