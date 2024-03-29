﻿//  Copyright (C) 2012 - TestTag Project
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

using System.Security;
using System.Xml;

namespace TestTag.Output
{  
    public static class XmlWriterExtensions
    {
        public static void WriteCdataElement(this XmlWriter writer, string tag, int cDataContents)
        {
            writer.WriteCdataElement(tag, cDataContents.ToString());
        }

        public static void WriteParagraph(this XmlWriter writer, string tag, string paragraphContents)
        {
            writer.WriteCdataElement(tag, "<p>" + SecurityElement.Escape(paragraphContents) + "</p>");
        }

        public static void WriteEncodedCdataElement(this XmlWriter writer, string tag, string cDataContents)
        {
            writer.WriteCdataElement(tag, SecurityElement.Escape(cDataContents));
        }

        public static void WriteCdataElement(this XmlWriter writer, string tag, string cDataContents)
        {
            writer.WriteStartElement(tag);
            writer.WriteCData(cDataContents.Replace("\n","<br />"));
            writer.WriteEndElement();
        }
    }

}
