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
using System.Xml;

namespace TestTag.Models
{
    public class TestPlan : XmlNode
    {
        public List<TestSuite> Suites { get; private set; }

        public TestPlan()
        {
            Suites = new List<TestSuite>();
        }

        public override void AppendXml(XmlWriter writer)
        {
            writer.WriteStartElement("testsuite");
            writer.WriteAttributeString("name", "");
            writer.WriteCdataElement("node_order", 1);
            writer.WriteCdataElement("details", "");
            foreach (var suite in Suites)
            {
                suite.AppendXml(writer);
            }
            writer.WriteEndElement();
        }

        public void Add(TestSuite suite)
        {
            Suites.Add(suite);
        }
    }
}
