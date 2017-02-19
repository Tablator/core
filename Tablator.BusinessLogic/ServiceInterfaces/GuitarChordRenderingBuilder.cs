using System;
using System.Collections.Generic;
using System.Text;
using Tablator.Infrastructure.Models;

namespace Tablator.BusinessLogic.Services
{
    public interface IGuitarChordRenderingBuilderService
    {
        bool DrawChord(string chord, out string svg, int cursorWidth = 0, int cursorHeight = 0);
        void Init(GuitarChordRenderingOptions options);
        int GetWidth();
        int GetHeight();
    }
}