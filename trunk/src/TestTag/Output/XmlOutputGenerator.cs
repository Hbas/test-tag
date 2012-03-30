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
using System.Xml;

namespace TestTag.Output
{
    public class XmlOutputGenerator : ITestOutputGenerator
    {
        private XmlWriter writer;

        public XmlOutputGenerator(XmlWriter writer)
        {
            this.writer = writer;
        }

        public void Write(TestPlan plan)
        {
            writer.WriteStartElement("testsuite");
            writer.WriteAttributeString("name", "");
            writer.WriteCdataElement("node_order", 1);
            writer.WriteCdataElement("details", "");
            foreach (var suite in plan.Suites)
            {
                Write(suite);
            }
            writer.WriteEndElement();
        }


        private void Write(TestSuite suite)
        {
            writer.WriteStartElement("testsuite");
            writer.WriteAttributeString("name", suite.Name);
            writer.WriteCdataElement("node_order", 1);
            writer.WriteCdataElement("details", "");
            foreach (var tc in suite.TestCases)
            {
                Write(tc);
            }
            writer.WriteEndElement();
        }


        public void Write(TestCase testCase)
        {
            writer.WriteStartElement("testcase");
            writer.WriteAttributeString("name", testCase.Name);
            writer.WriteCdataElement("node_order", testCase.Order);
            writer.WriteParagraph("summary", testCase.Summary);
            writer.WriteCdataElement("execution_type", 1);
            writer.WriteCdataElement("preconditions", XmlFor(testCase.Preconditions));
            writer.WriteStartElement("steps");
            foreach (var step in testCase.Steps)
            {
                Write(step);
            }
            writer.WriteEndElement();
            writer.WriteEndElement();
        }

        private string XmlFor(List<string> preconditionsList)
        {
            StringBuilder sb = new StringBuilder("<ul>");
            foreach (string preCond in preconditionsList)
            {
                sb.Append("<li>");
                sb.Append(preCond);
                sb.Append("</li>");
            }
            sb.Append("</ul>");
            return sb.ToString();
        }


        public void Write(TestStep step)
        {
            writer.WriteStartElement("step");
            writer.WriteCdataElement("step_number", step.StepNumber);
            writer.WriteParagraph("actions", step.Action);
            writer.WriteParagraph("expectedresults", step.ExpectedResult);
            writer.WriteCdataElement("execution_type", 1);
            writer.WriteEndElement();
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
