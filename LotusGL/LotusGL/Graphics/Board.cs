﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace LotusGL.Graphics
{
    class Board : iDrawable
    {
        public void Load()
        {




            TextureLoader.get().loadTexture(@"..\..\images\marble.bmp", "marble");
            TextureLoader.get().loadTexture(@"..\..\images\board.bmp", "board");
        }

        public void Draw()
        {
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, TextureLoader.get().getTexture("board"));
            GL.Begin(BeginMode.TriangleStrip);
            GL.TexCoord2(new OpenTK.Vector2d(0, 0));
            GL.Vertex3(0, 0, 0);
            GL.TexCoord2(new OpenTK.Vector2d(1, 0));
            GL.Vertex3(512, 0, 0);
            GL.TexCoord2(new OpenTK.Vector2d(0, 1f));
            GL.Vertex3(0, 512, 0);
            GL.TexCoord2(new OpenTK.Vector2d(1, 1f));
            GL.Vertex3(512, 512, 0);
            
            GL.End();

            GL.BindTexture(TextureTarget.Texture2D, TextureLoader.get().getTexture("marble"));
            GL.Begin(BeginMode.TriangleStrip);
            
            GL.TexCoord2(new OpenTK.Vector2(0, 0));
            GL.Vertex3(0, 0, 0);
            GL.TexCoord2(new OpenTK.Vector2(1, 0));
            GL.Vertex3(512, 0, 0);
            GL.TexCoord2(new OpenTK.Vector2(0, 0.1f));
            GL.Vertex3(0, 0, -20);
            GL.TexCoord2(new OpenTK.Vector2(1, 0.1f));
            GL.Vertex3(512, 0, -20);
            GL.Vertex3(512, 0, -20);

            GL.TexCoord2(new OpenTK.Vector2(0, 0));
            GL.Vertex3(512, 0, 0);
            GL.Vertex3(512, 0, 0);
            GL.TexCoord2(new OpenTK.Vector2(1, 0));
            GL.Vertex3(512, 512, 0);
            GL.TexCoord2(new OpenTK.Vector2(0, 0.1f));
            GL.Vertex3(512, 0, -20);
            GL.TexCoord2(new OpenTK.Vector2(1, 0.1f));
            GL.Vertex3(512, 512, -20);
            GL.Vertex3(512, 512, -20);

            GL.TexCoord2(new OpenTK.Vector2(0, 0));
            GL.Vertex3(0, 512, 0);
            GL.Vertex3(0, 512, 0);
            GL.TexCoord2(new OpenTK.Vector2(1, 0));
            GL.Vertex3(512, 512, 0);
            GL.TexCoord2(new OpenTK.Vector2(0, 0.1f));
            GL.Vertex3(0, 512, -20);
            GL.TexCoord2(new OpenTK.Vector2(1, 0.1f));
            GL.Vertex3(512, 512, -20);
            GL.Vertex3(512, 512, -20);

            GL.TexCoord2(new OpenTK.Vector2(0, 0));
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, 0);
            GL.TexCoord2(new OpenTK.Vector2(1, 0));
            GL.Vertex3(0, 512, 0);
            GL.TexCoord2(new OpenTK.Vector2(0, 0.1f));
            GL.Vertex3(0, 0, -20);
            GL.TexCoord2(new OpenTK.Vector2(1, 0.1f));
            GL.Vertex3(0, 512, -20);
            GL.Vertex3(0, 512, -20);

            GL.End();
        }

        public void UnLoad()
        {
            TextureLoader.get().releaseTextures();
        }
    }
}
