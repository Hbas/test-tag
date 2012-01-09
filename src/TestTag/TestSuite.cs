﻿//  Copyright (C) 2012 - Henrique Borges 
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
using System.Collections.Generic;
using System.Xml;

namespace TestTag.Models
{
    public class TestSuite : XmlNode
    {
        public string Name { get; private set; }
        public List<TestCase> TestCases { get; private set; }

        public TestSuite(string name)
        {
            Name = name;
            TestCases = new List<TestCase>();
        }

        public override void AppendXml(XmlWriter writer)
        {            
            writer.WriteStartElement("testsuite");
            writer.WriteAttributeString("name", Name);
            writer.WriteCdataElement("node_order", 1);
            writer.WriteCdataElement("details", "");
            foreach (var tc in TestCases)
            {
                tc.AppendXml(writer);
            }
            writer.WriteEndElement();
        }
    }
}