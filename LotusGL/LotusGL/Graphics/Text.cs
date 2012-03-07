using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using OpenTK.Graphics.OpenGL;
using OpenTK;
namespace LotusGL.Graphics
{
    class Text
    {
        struct Character
        {
            public int x, y;
        }

        static Dictionary<char, Character> characters;

        public static void Load(Font font, int first = 32, int last = 126, bool create = false)
        {
            characters = new Dictionary<char, Character>();

            for (int i = first; i <= last; i++)
            {
                char c = Convert.ToChar(i);
                string s = "" + c;
                int x = ((i - first) / 16) * 16;
                int y = ((i - first) % 16) * 16;

                Character cpos;
                cpos.x = x;
                cpos.y = y;
                characters.Add(c, cpos);
            }

            if (create)
            {
                Bitmap objBmpImage = new Bitmap(512, 512);
                System.Drawing.Graphics objGraphics = System.Drawing.Graphics.FromImage(objBmpImage);

                objGraphics.Clear(Color.Red);
                objGraphics.SmoothingMode = SmoothingMode.None;
                objGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;

                for (int i = first; i <= last; i++)
                {
                    char c = Convert.ToChar(i);
                    objGraphics.DrawString("" + c, font, new SolidBrush(Color.White), new RectangleF(characters[c].x, characters[c].y, 16, 16));
                    objGraphics.Flush();
                }
                objBmpImage.Save(@"..\..\images\font.bmp");
            }

            TextureLoader.get().loadTexture(@"..\..\images\font.bmp", "font");
        }

        public static void Draw(OpenTK.Graphics.Color4 color, float posx, float posy, string str, int size = 32)
        {
            if (str == null)
                return;
            GL.PushMatrix();

            Matrix4 translation = Matrix4.CreateTranslation(new Vector3(posx, posy, 0));

            GL.LoadMatrix(ref translation);
            GL.Enable(EnableCap.Texture2D);
            GL.Disable(EnableCap.DepthTest);
            GL.BindTexture(TextureTarget.Texture2D, TextureLoader.get().getTexture("font"));
            GL.Begin(BeginMode.TriangleStrip);
            GL.Color4(color);

            char[] chars = str.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                float tx = (i * size*2/3) / 512f;
                float ty = 0;
                float tx2 = ((i + 1) * size*2/3) / 512f;
                float ty2 = (size) / 512f;
                
                float texx = characters[chars[i]].x / 512f;
                float texy = characters[chars[i]].y / 512f;
                float texx2 = (characters[chars[i]].x + 10f) / 512f;
                float texy2 = (characters[chars[i]].y + 16) / 512f;

                GL.TexCoord2(texx, texy);
                GL.Vertex3(tx, ty2, 0);
                GL.Vertex3(tx, ty2, 0);

                GL.TexCoord2(texx2, texy);
                GL.Vertex3(tx2, ty2, 0);

                GL.TexCoord2(texx, texy2);
                GL.Vertex3(tx, ty, 0);

                GL.TexCoord2(texx2, texy2);
                GL.Vertex3(tx2, ty, 0);
                GL.Vertex3(tx2, ty, 0);

            }

            GL.End();
            GL.Enable(EnableCap.DepthTest);
            GL.PopMatrix();
        }
        public static void UnLoad()
        {
            TextureLoader.get().releaseTextures();
        }
    }
}
