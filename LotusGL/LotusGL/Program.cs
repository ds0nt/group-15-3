using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;

using LotusGL.Graphics;

namespace LotusGL
{
    class Program
    {
        static void Main(string[] args)
        {
            LotusWindow window = new LotusWindow();

	        //glutIdleFunc(IdleCallback);
	        //glutMouseFunc(MouseCallback);
	        //atexit(Exit);

	        // Setup default render states
	        //GL.Enable(GL_DEPTH_TEST);
	        //GL.Enable(GL_COLOR_MATERIAL);

	        // Setup lighting
	        //GL.Enable(GL_LIGHTING);
	        //float ambientColor[] = {0.0f, 0.1f, 0.2f, 0.0f};
	        //float diffuseColor[] = {1.0f, 1.0f, 1.0f, 0.0f};
	        //float specularColor[] = {0.0f, 0.0f, 0.0f, 0.0f};	
	        //float position[] = {100.0f, 100.0f, 400.0f, 1.0f};
	        //glLightfv(GL_LIGHT0, GL_AMBIENT, ambientColor);
	        //glLightfv(GL_LIGHT0, GL_DIFFUSE, diffuseColor);
	        //glLightfv(GL_LIGHT0, GL_SPECULAR, specularColor);
	        //glLightfv(GL_LIGHT0, GL_POSITION, position);
	        //glEnable(GL_LIGHT0);

        }
    }
}
