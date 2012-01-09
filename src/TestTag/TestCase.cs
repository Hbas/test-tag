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
using System.Xml;
using System.Text;

namespace TestTag
{
    public class TestCase : XmlNode
    {
        public int Order { get; set; }

        public string Name { get; set; }
        public string Summary { get; set; }

        public List<string> Preconditions { get; private set; }
        public List<string> Tags { get; private set; }
        public List<TestStep> Steps { get; private set; }


        public TestCase(string name)
            : this()
        {
            Name = name;
        }

        public TestCase()
        {
            Preconditions = new List<string>();
            Tags = new List<string>();
            Steps = new List<TestStep>();
            Order = 100;
        }

        public override void AppendXml(XmlWriter writer)
        {
            writer.WriteStartElement("testcase");
            writer.WriteAttributeString("name", Name);
            writer.WriteCdataElement("node_order", Order);
            writer.WriteParagraph("summary", Summary);
            writer.WriteCdataElement("execution_type", 1);
            writer.WriteCdataElement("preconditions", PreconditionsXml);
            writer.WriteStartElement("steps");
            int stepNumber = 1;
            foreach (TestStep step in Steps)
            {
                step.StepNumber = stepNumber++;
                step.AppendXml(writer);
            }
            writer.WriteEndElement();
            writer.WriteEndElement();
        }

        private string PreconditionsXml
        {
            get
            {
                StringBuilder sb = new StringBuilder("<ul>");
                foreach (string preCond in Preconditions)
                {
                    sb.Append("<li>");
                    sb.Append(preCond);
                    sb.Append("</li>");
                }
                sb.Append("</ul>");
                return sb.ToString();
            }
        }
       
    }
}