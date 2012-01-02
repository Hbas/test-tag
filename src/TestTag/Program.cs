//  Copyright (C) 2012 - Henrique Borges 
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
using TestTag.Parser;
using System.IO;
using System.Xml;

namespace TestTag
{
    class Program
    {
        static void Main(string[] args)
        {
            TstParser parser = new TstParser();

            foreach (string path in Directory.EnumerateFiles(Directory.GetCurrentDirectory(), "*.tst", SearchOption.AllDirectories))
            {
                try
                {
                    parser.Parse(TstTokenizer.FromFilePath(path));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Couldn´t parse file " + path);
                    Console.WriteLine(ex.ToString());
                    Console.ReadLine();
                }
            }
            StreamWriter writer = new StreamWriter("tests.xml");
            XmlWriterSettings settings = new XmlWriterSettings()
            {
                Indent = true,
                Encoding = Encoding.UTF8
            };
            using (XmlWriter xml = XmlWriter.Create(writer, settings))
            {
                parser.TestPlan.AppendXml(xml);
            }
        }
    }
}
