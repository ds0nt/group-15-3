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
        GameWindow window;
        int framenum = 0;
        public static LotusWindow me;

        //Camera Variables
        int cameraState = 1;
        Vector2 center = new Vector2(256, 256);
        float camDistance, camPitch, camAngle;
        Vector3 eye;
        public Matrix4 world, view, proj;

        //Input Variables
        bool rightPressed = false;
        bool leftPressed = false;
        int dw, dx, dy, mx, my, mb;
        public GraphicsFacade.BoardRegion[] regions;
        public GraphicsFacade.BoardRegion2D[] regions2D;


        public delegate void UpdateEventHandler(GraphicsFacade.InputEvent m, double time);
        public event UpdateEventHandler onUpdate;

        public delegate void DrawEventHandler();
        public event DrawEventHandler onDraw;

        public LotusWindow()
        {
            me = this;      
        }
        public void Init()
        {
            camDistance = 1000;
            camPitch = (float)Math.PI / 8.0f;
            camAngle = (float)(framenum) / 1000;

            window  = new GameWindow(500, 500);

            window.Title = "Group 15 - Lotus";
            window.RenderFrame += onRender;
            window.Resize += onResize;
            window.Mouse.ButtonDown += this.onMouseButton;
            window.Mouse.ButtonUp += this.onMouseButton;
            window.Mouse.Move += this.onMouseMove;
            window.Mouse.WheelChanged += this.onWheelChanged;
            window.MouseLeave += this.onMouseLeave;
            window.Keyboard.KeyDown += this.onKeyDown;
            window.Keyboard.KeyUp += this.onKeyUp;
            GL.ClearColor(0.0f, 0.0f, 0.2f, 1.0f);
            GL.Enable(EnableCap.DepthTest);
            GL.CullFace(CullFaceMode.Front);
        }

        public void Load()
        {

            Text.Load(new System.Drawing.Font("Gotham Medium", 12, System.Drawing.FontStyle.Bold), 0, 127, false);
            Board.Load();
            Piece.Load();
            Menu.Load();
        }

        void UnLoad()
        {
            Board.UnLoad();
            Piece.UnLoad();
            Menu.UnLoad();
            Text.UnLoad();
        }

        public void Run()
        {
            float aspect = window.Width / window.Height;
            proj = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 8, aspect, 10f, 3000f);
            window.Run(60);
        }
        void onRender(object sender, FrameEventArgs e)
        {
            framenum++;
            GameWindow window = (GameWindow)sender;


            GL.Clear(ClearBufferMask.ColorBufferBit|ClearBufferMask.DepthBufferBit);
            
            world = Matrix4.Identity;

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
                camDistance = (float) Math.Min(camDistance, 2000);
                camDistance = (float) Math.Max(camDistance, 100);
                eye = new Vector3(
                        center.X + camDistance * (float)(Math.Cos(camPitch) * Math.Cos(camAngle)),
                        center.Y + camDistance * (float)(Math.Cos(camPitch) * Math.Sin(camAngle)),
                        (float)Math.Sin(camPitch) * camDistance);
                view = Matrix4.LookAt(
                    eye, 
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
            
            if (onDraw != null)
                onDraw();

            if (onUpdate != null)
            {
                GraphicsFacade.InputEvent m = new GraphicsFacade.InputEvent();
                m.lastKey = lastkey;
                m.regionId = m.x = m.y = int.MinValue;
                if (mx != 0 && my != 0)
                {   
                    m.x = mx;
                    m.y = my;
                    if(mb == 1)
                        if (GraphicsFacade.mode == GraphicsFacade.Mode.BOARD)
                        {
                            m.regionId = TraceMouse3D(mx, my);
                        }
                        else if (GraphicsFacade.mode == GraphicsFacade.Mode.MENU)
                        {
                            m.regionId = TraceMouse2D(mx, my);
                        }
                }
                onUpdate(m, window.RenderTime);
            }
            window.SwapBuffers();
            lastkey = char.MinValue;    
            mb = mx = my = dw = dx = dy = 0;
            
        }

        void onResize(object sender, EventArgs e)
        {
            GameWindow window = (GameWindow)sender;
            Console.WriteLine(window.Width);

            GL.Viewport(0, 0, window.Width, window.Height);
        }

        char lastkey;
        bool shiftdown = false;
        void onKeyUp(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
        {
            switch (e.Key)
            {
                case OpenTK.Input.Key.ShiftLeft:
                case OpenTK.Input.Key.ShiftRight:
                    shiftdown = false;
                    return;
            }
        }
        
        void onKeyDown(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
        {
            char x = (char)e.Key;

            switch (e.Key)
            {
                case OpenTK.Input.Key.Enter:
                case OpenTK.Input.Key.KeypadEnter:
                    lastkey = (char)1;
                    return;
                case OpenTK.Input.Key.BackSpace:
                    lastkey = (char)2;
                    return;
                case OpenTK.Input.Key.Space:
                    lastkey = ' ';
                    return;
                case OpenTK.Input.Key.Period:
                    lastkey = '.';
                    return;
                case OpenTK.Input.Key.Number1:
                    if (shiftdown)
                        lastkey = '!';
                    else
                        lastkey = '1';
                    return;
                case OpenTK.Input.Key.Number2:
                    lastkey = '2';
                    return;
                case OpenTK.Input.Key.Number3:
                    lastkey = '3';
                    return;
                case OpenTK.Input.Key.Number4:
                    lastkey = '4';
                    return;
                case OpenTK.Input.Key.Number5:
                    lastkey = '5';
                    return;
                case OpenTK.Input.Key.Number6:
                    lastkey = '6';
                    return;
                case OpenTK.Input.Key.Number7:
                    lastkey = '7';
                    return;
                case OpenTK.Input.Key.Number8:
                    lastkey = '8';
                    return;
                case OpenTK.Input.Key.Number9:
                    lastkey = '9';
                    return;
                case OpenTK.Input.Key.Number0:
                    lastkey = '0';
                    return;
                case OpenTK.Input.Key.Slash:
                    if (shiftdown)
                        lastkey = '?';
                    return;
                case OpenTK.Input.Key.ShiftLeft:
                case OpenTK.Input.Key.ShiftRight:
                    shiftdown = true;
                    return;
            }

            if(shiftdown)
            {
                if (e.Key >= OpenTK.Input.Key.A && e.Key <= OpenTK.Input.Key.Z)
                {
                    lastkey = (char)(x - ((char)OpenTK.Input.Key.A) + 'A');
                    return;
                }
            }
            else
            {
                if (e.Key >= OpenTK.Input.Key.A && e.Key <= OpenTK.Input.Key.Z)
                {
                    lastkey = (char)(x - ((char)OpenTK.Input.Key.A) + 'a');
                    return;
                }
                else
                {
                    
                }

            }
        }

        float hx, hy;
        void onMouseMove(object sender, OpenTK.Input.MouseMoveEventArgs e)
        {
            hx = e.X;
            hy = e.Y;
            dx = e.XDelta;
            dy = e.YDelta;
        }

        void onMouseButton(object sender, OpenTK.Input.MouseButtonEventArgs e)
        {
            if (leftPressed)
                mb = 1;
            if (rightPressed)
                mb = 2;
            rightPressed = e.Button == OpenTK.Input.MouseButton.Right && e.IsPressed;
            leftPressed = e.Button == OpenTK.Input.MouseButton.Left && e.IsPressed;

            if (!leftPressed && !rightPressed)
            {
                mx = e.X;
                my = e.Y;
            }
        }

        void onWheelChanged(object sender, OpenTK.Input.MouseWheelEventArgs e)
        {
            dw = e.Delta;
        }

        void onMouseLeave(object sender, EventArgs e)
        {
            rightPressed = leftPressed = false;
        }


        public int TraceMouse3D(float x, float y)
        {
            float xpos = 2 * (x / window.Width) - 1;
            float ypos = 2 * (1 - y / window.Height) - 1;

            Vector4 startRay = new Vector4(xpos, ypos, -1, 1);
            Vector4 endRay = new Vector4(xpos, ypos, 1, 1);

            Matrix4 trans = Matrix4.Invert(view * proj);

            startRay = Vector4.Transform(startRay, trans);
            endRay = Vector4.Transform(endRay, trans);
            Vector3 ray = startRay.Xyz / startRay.W;
            Vector3 step = endRay.Xyz / endRay.W - ray;
            step.Normalize();
            
            while (ray.Z > 0 && ray.Z < 2000)
            {
                ray += step;
                for (int i = 0; i < regions.Length; i++)
                {
                    if(ray.Z < regions[i].height*10 + 5)
                        if (((ray.X - regions[i].x - 16) * (ray.X - regions[i].x) - 16) + ((ray.Y - regions[i].y - 16) * (ray.Y - regions[i].y - 16)) < 256)
                        {
                            Console.WriteLine(i);
                            return regions[i].id;
                        }
                }
            }
            return int.MinValue;
        }
        public int TraceMouse2D(float x, float y)
        {
            x = 2 * (x / window.Width) - 1;
            y = 2 * (y / window.Height) - 1;

            for (int i = 0; i < regions2D.Length; i++)
            {
                if (x < regions2D[i].x || x > regions2D[i].x + regions2D[i].width || y < regions2D[i].y || y > regions2D[i].y + regions2D[i].height)
                    continue;
                return regions2D[i].id;
            }
            return int.MinValue;
        }

    }
}