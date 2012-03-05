using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace LotusGL.Graphics
{
    class TextureLoader
    {
        Dictionary<string, int> textures;
        static TextureLoader me = null;

        public TextureLoader()
        {
            textures = new Dictionary<string, int>();
        }

        public void loadTexture(string path, string name)
        {
            if (textures.ContainsKey(name))
                return;
            System.IO.FileStream r = System.IO.File.OpenRead(path);
            uint size = (uint)r.Length;
            byte[] tex;

            Bitmap bmp = new Bitmap(path);
            unsafe
            {
                System.Drawing.Imaging.BitmapData bData = bmp.LockBits(new Rectangle(new Point(), bmp.Size),
                    System.Drawing.Imaging.ImageLockMode.ReadOnly,
                    System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                // number of bytes in the bitmap
                int byteCount = bData.Stride * bmp.Height;
                tex = new byte[byteCount];

                // Copy the locked bytes from memory
                Marshal.Copy(bData.Scan0, tex, 0, byteCount);

                // don't forget to unlock the bitmap!!
                bmp.UnlockBits(bData);
                for (int i = 0; i < tex.Length; i+=4)
                {
                    if (tex[i] == 0 && tex[i + 1] == 0 && tex[i + 2] == 255)
                        tex[i + 3] = 0;
                }
            }
            int id;
            GL.GenTextures(1, out id);
            GL.BindTexture(TextureTarget.Texture2D, id); 
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (float)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (float)TextureWrapMode.ClampToEdge);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, 512, 512, 0, PixelFormat.Rgba, PixelType.UnsignedByte, tex);
            
            textures[name] = id;
        }

        public int getTexture(string name)
        {
            return textures[name];
        }

        public void releaseTextures()
        {
            foreach (int i in textures.Values)
            {
                GL.DeleteTexture(i);
            }
        }

        static public TextureLoader get()
        {
            if (me == null)
                me = new TextureLoader();
            return me;
        }
    }
}
