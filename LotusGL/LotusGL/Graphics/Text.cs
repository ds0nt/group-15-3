//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Drawing;
//using System.Drawing.Text;
//using System.Drawing.Drawing2D;

//namespace LotusGL.Graphics
//{
//    class Text
//    {
//        struct Character
//        {
//            int x, y;
//        }
//        Dictionary<char, Character> characters;
//        public void Load(Font font, int first = 32, int last = 126)
//        {
//            characters = new Dictionary<char, Character>();
//            Bitmap objBmpImage = new Bitmap(objBmpImage, new Size(256, 256));
//            System.Drawing.Graphics objGraphics = System.Drawing.Graphics.FromImage(objBmpImage);

//            objGraphics.Clear(Color.White);
//            objGraphics.SmoothingMode = SmoothingMode.None;
//            objGraphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;

//            for (int i = first; i <= last; i++)
//            {
//                char c = Convert.ToChar(i);
//                string s = "" + c;
//                int x = ((i - first) / 16) * 16;
//                int y = ((i - first) % 16) * 16;
//                objGraphics.DrawString(s, font, new SolidBrush(Color.Black), x, y);
//                objGraphics.Flush();
//                Character cpos;
//                cpos.x = x;
//                cpos.y = y;
//                characters.Add(c, cpos);
//            }
//        }

//    }
//}
