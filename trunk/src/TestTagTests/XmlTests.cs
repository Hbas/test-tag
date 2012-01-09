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
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestTag;

namespace TestTagTest
{
    [TestClass]
    public class XmlTests
    {
        [TestMethod]
        public void TestStepXml()
        {
            TestStep test = new TestStep()
            {
                StepNumber = 1,
                Action = "Some action",
                ExpectedResult = "Result number 1"
            };
            string expected = "<step>\r\n" +
                                "  <step_number><![CDATA[1]]></step_number>\r\n" +
                                "  <actions><![CDATA[<p>Some action</p>]]></actions>\r\n" +
                                "  <expectedresults><![CDATA[<p>Result number 1</p>]]></expectedresults>\r\n" +
                                "  <execution_type><![CDATA[1]]></execution_type>\r\n" +
                                "</step>";
            Assert.AreEqual(expected, test.Xml);
        }

        [TestMethod]
        public void TestCaseXml()
        {
            TestCase tc = new TestCase()
            {
                Name = "TestName",
                Summary = "Some goal"
            };
            tc.Steps.Add(new TestStep()
            {
                Action = "A1",
                ExpectedResult = "Resultado esperado"
            });
            tc.Steps.Add(new TestStep()
            {
                Action = "A2",
                ExpectedResult = "Resultado esperado2"
            });
            tc.Preconditions.Add("Precond");
            string expected = "<testcase name=\"TestName\">\r\n" +
                                "  <node_order><![CDATA[100]]></node_order>\r\n" +
                                "  <summary><![CDATA[<p>Some goal</p>]]></summary>\r\n" +
                                "  <execution_type><![CDATA[1]]></execution_type>\r\n" +
                                "  <preconditions><![CDATA[<ul><li>Precond</li></ul>]]></preconditions>\r\n" +
                                "  <steps>\r\n" +
                                "    <step>\r\n" +
                                "      <step_number><![CDATA[1]]></step_number>\r\n" +
                                "      <actions><![CDATA[<p>A1</p>]]></actions>\r\n" +
                                "      <expectedresults><![CDATA[<p>Resultado esperado</p>]]></expectedresults>\r\n" +
                                "      <execution_type><![CDATA[1]]></execution_type>\r\n" +
                                "    </step>\r\n" +
                                "    <step>\r\n" +
                                "      <step_number><![CDATA[2]]></step_number>\r\n" +
                                "      <actions><![CDATA[<p>A2</p>]]></actions>\r\n" +
                                "      <expectedresults><![CDATA[<p>Resultado esperado2</p>]]></expectedresults>\r\n" +
                                "      <execution_type><![CDATA[1]]></execution_type>\r\n" +
                                "    </step>\r\n" +
                                "  </steps>\r\n" +
                                "</testcase>";
            Assert.AreEqual(expected, tc.Xml);
        }
    }
}
