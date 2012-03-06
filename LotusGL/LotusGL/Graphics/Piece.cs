using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace LotusGL.Graphics
{
    class Piece
    {
        static bool Loaded = false;
        static float height = 10;
        
        public static void Load()
        {
            Loaded = true;
            TextureLoader.get().loadTexture(@"..\..\images\marble.bmp", "marble");
            TextureLoader.get().loadTexture(@"..\..\images\piece.bmp", "piece");
            TextureLoader.get().loadTexture(@"..\..\images\select.bmp", "select");
        }


        public static void Draw(OpenTK.Graphics.Color4 color, Vector2 position, int level)
        {
            Draw(color, new Vector3(position.X, position.Y, level * height));
        }

        public static void Draw(OpenTK.Graphics.Color4 color, Vector3 position)
        {
            if(!Loaded)
                Load();
            GL.PushMatrix();
            
            Matrix4 translation = Matrix4.CreateTranslation(position) * LotusWindow.me.view * LotusWindow.me.proj;
            GL.LoadMatrix(ref translation);
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, TextureLoader.get().getTexture("piece"));
            GL.Begin(BeginMode.TriangleStrip);
            GL.Color4(color);
            GL.TexCoord2(new OpenTK.Vector2d(0, 0));
            GL.Vertex3(0, 0, 0);
            GL.TexCoord2(new OpenTK.Vector2d(1, 0));
            GL.Vertex3(32, 0, 0);
            GL.TexCoord2(new OpenTK.Vector2d(0, 1f));
            GL.Vertex3(0, 32, 0);
            GL.TexCoord2(new OpenTK.Vector2d(1, 1f));
            GL.Vertex3(32, 32, 0);

            GL.End();
            GL.PopMatrix();
        }

        public static void DrawSelected(Vector2 position, int level)
        {
            DrawSelected(new Vector3(position.X, position.Y, level * height + 5));
        }
        public static void DrawSelected(Vector3 position)
        {
            if (!Loaded)
                Load();
            GL.PushMatrix();

            Matrix4 translation = Matrix4.CreateTranslation(position) * LotusWindow.me.view * LotusWindow.me.proj;
            GL.LoadMatrix(ref translation);
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, TextureLoader.get().getTexture("select"));
            GL.Begin(BeginMode.TriangleStrip);
            GL.Color4(OpenTK.Graphics.Color4.White);
            GL.TexCoord2(new OpenTK.Vector2d(0, 0));
            GL.Vertex3(0, 0, 0);
            GL.TexCoord2(new OpenTK.Vector2d(1, 0));
            GL.Vertex3(32, 0, 0);
            GL.TexCoord2(new OpenTK.Vector2d(0, 1f));
            GL.Vertex3(0, 32, 0);
            GL.TexCoord2(new OpenTK.Vector2d(1, 1f));
            GL.Vertex3(32, 32, 0);

            GL.End();
            GL.PopMatrix();
        }
        public static void UnLoad()
        {
            TextureLoader.get().releaseTextures();
        }
    }
}
