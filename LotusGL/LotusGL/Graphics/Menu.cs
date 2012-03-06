﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace LotusGL.Graphics
{
    class Menu
    {
        static bool Loaded = false;
        static float height = 10;
        
        public static void Load()
        {
            Loaded = true;
            TextureLoader.get().loadTexture(@"..\..\images\title.bmp", "Title");
            TextureLoader.get().loadTexture(@"..\..\images\Player1-Type.bmp", "Player1");
            TextureLoader.get().loadTexture(@"..\..\images\Player2-Type.bmp", "Player2");
            TextureLoader.get().loadTexture(@"..\..\images\Player3-Type.bmp", "Player3");
            TextureLoader.get().loadTexture(@"..\..\images\Player4-Type.bmp", "Player4");
            TextureLoader.get().loadTexture(@"..\..\images\LotusLogo.bmp", "Logo");
            TextureLoader.get().loadTexture(@"..\..\images\gameover.bmp", "GameOver");
            TextureLoader.get().loadTexture(@"..\..\images\ai-rule.bmp", "RuleAI");
            TextureLoader.get().loadTexture(@"..\..\images\ai-state.bmp", "StateAI");
            TextureLoader.get().loadTexture(@"..\..\images\human.bmp", "Human");
            TextureLoader.get().loadTexture(@"..\..\images\finished.bmp", "Finish");
        }

       public static void Draw(string menuname)
       {
        Draw(menuname, -1, -1, 2, 2);
        }

       public static void Draw(string menuname, float x, float y, float width, float height)
       {
           if (!Loaded)
               Load();
           GL.PushMatrix();

           Matrix4 translation = Matrix4.Identity;// Matrix4.CreateTranslation(new Vector3(-0.5f, -0.5f, 0));
           GL.LoadMatrix(ref translation);
           GL.Enable(EnableCap.Texture2D);
           GL.Disable(EnableCap.DepthTest);
           switch (menuname)
           {
               case "title":
                   GL.BindTexture(TextureTarget.Texture2D, TextureLoader.get().getTexture("Title"));
                   break;
               case "player1":
                   GL.BindTexture(TextureTarget.Texture2D, TextureLoader.get().getTexture("Player1"));
                   break;
               case "player2":
                   GL.BindTexture(TextureTarget.Texture2D, TextureLoader.get().getTexture("Player2"));
                   break;
               case "player3":
                   GL.BindTexture(TextureTarget.Texture2D, TextureLoader.get().getTexture("Player3"));
                   break;
               case "player4":
                   GL.BindTexture(TextureTarget.Texture2D, TextureLoader.get().getTexture("Player4"));
                   break;
               case "logo":
                   GL.BindTexture(TextureTarget.Texture2D, TextureLoader.get().getTexture("Logo"));
                   break;
               case "stateAI":
                   GL.BindTexture(TextureTarget.Texture2D, TextureLoader.get().getTexture("StateAI"));
                   break;
               case "ruleAI":
                   GL.BindTexture(TextureTarget.Texture2D, TextureLoader.get().getTexture("RuleAI"));
                   break;
               case "human":
                   GL.BindTexture(TextureTarget.Texture2D, TextureLoader.get().getTexture("Human"));
                   break;
               case "finish":
                   GL.BindTexture(TextureTarget.Texture2D, TextureLoader.get().getTexture("Finish"));
                   break;
               case "gameover":
                   GL.BindTexture(TextureTarget.Texture2D, TextureLoader.get().getTexture("GameOver"));
                   break;
           }
           GL.Begin(BeginMode.TriangleStrip);
           GL.TexCoord2(new OpenTK.Vector2d(0, 1));
           GL.Vertex3(x, y, 0);
           GL.TexCoord2(new OpenTK.Vector2d(1, 1));
           GL.Vertex3(x + width, y, 0);
           GL.TexCoord2(new OpenTK.Vector2d(0, 0f));
           GL.Vertex3(x, y - height, 0);
           GL.TexCoord2(new OpenTK.Vector2d(1, 0f));
           GL.Vertex3(x + width, y - height, 0);
           GL.End();
           GL.Enable(EnableCap.DepthTest);
           GL.PopMatrix();
       }

        public static void DrawString(string text)
        {
            
        }

        public static void UnLoad()
        {
            TextureLoader.get().releaseTextures();
        }
    }
}
