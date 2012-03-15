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

       
        public void Write(TestSuite suite)
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
    }
}
