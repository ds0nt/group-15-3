using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;
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
            byte[] tex = new byte[size];
            r.Seek(10, System.IO.SeekOrigin.Begin);

            uint start = new System.IO.BinaryReader(r).ReadUInt32();

            r.Seek(start, System.IO.SeekOrigin.Begin);
            r.Read(tex, 0, (int)(size - start));

            int id;
            GL.GenTextures(1, out id);
            GL.BindTexture(TextureTarget.Texture2D, id); 
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (float)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (float)TextureWrapMode.ClampToEdge);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, 512, 512, 0, PixelFormat.Rgb, PixelType.UnsignedByte, tex);
            
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
