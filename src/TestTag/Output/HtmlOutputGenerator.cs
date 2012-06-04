//  Copyright (C) 2012 - TestTag Project
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RazorEngine;
using RazorEngine.Templating;

namespace TestTag.Output
{
    public class HtmlOutputGenerator : ITestOutputGenerator
    {
        private TextWriter writer;

        public HtmlOutputGenerator(TextWriter writer)
        {
            this.writer = writer;
        }
        
        public void Write(TestPlan plan)
        {
            StreamReader template = new StreamReader("planTemplate.txt");
            try
            {
                string templateData = template.ReadToEnd();
                string result = Razor.Parse(templateData, plan);
                writer.Write(result);
            }
            catch (TemplateCompilationException ex)
            {
                var firstError = ex.Errors[0];
                throw new Exception(firstError.ErrorText);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && writer != null)
            {
                writer.Close();
                writer = null;
            }
        }
    }
}
