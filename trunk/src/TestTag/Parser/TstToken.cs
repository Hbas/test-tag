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

namespace TestTag.Parser
{
    public class TstToken
    {

        public static TstToken LINE_BREAK = new TstToken("\n", "a line break", TstTokenType.LINE_BREAK);
        public static TstToken EMPTY = new TstToken("", "the end of file", TstTokenType.EMPTY);
        public static TstToken OPEN_BRACKET = new TstToken("{");
        public static TstToken CLOSE_BRACKET = new TstToken("}");
        public static TstToken OPEN_PARENTHESIS = new TstToken("(");
        public static TstToken CLOSE_PARENTHESIS = new TstToken(")");

        public static TstToken STEP_SEPARATOR = new TstToken("=>");
        public static TstToken BEFORE = new TstToken("BEFORE:");
        public static TstToken COMMA = new TstToken(",");

        public static TstToken LineBreak(int lineNumber)
        {
            var token = new TstToken("\n", "a line break", TstTokenType.LINE_BREAK);
            token.Line = lineNumber;
            return token;
        }

        public string Content { get; set; }
        public TstTokenType Type { get; private set; }
        public string Description { get; private set; }
        public int Line { get; private set; }

        private TstToken(string token, string description, TstTokenType type)
        {
            this.Content = token;
            this.Description = description;
            Type = type;
        }

        private TstToken(string token)
            : this(token, 0)
        { }

        public TstToken(string token, int currentLine)
        {
            this.Content = token.Trim();
            this.Description = Content;
            this.Line = currentLine;
            switch (Content)
            {
                case "{":
                    this.Type = TstTokenType.OPEN_BRACKET;
                    break;
                case "}":
                    this.Type = TstTokenType.CLOSE_BRACKET;
                    break;
                case "(":
                    this.Type = TstTokenType.OPEN_PARENTHESIS;
                    break;
                case ")":
                    this.Type = TstTokenType.CLOSE_PARENTHESIS;
                    break;
                case ",":
                    this.Type = TstTokenType.COMMA;
                    break;
                case "=>":
                    this.Type = TstTokenType.STEP_SEPARATOR;
                    break;
                case "DESCRIPTION:":
                    this.Type = TstTokenType.TEST_SUMMARY;
                    break;
                case "PRE:":
                    this.Type = TstTokenType.TEST_PRECONDITION;
                    break;
                case "TAG:":
                    this.Type = TstTokenType.TAG;
                    break;
                case "BEFORE:":
                    this.Type = TstTokenType.BEFORE_STEP;
                    break;
                case "AFTER:":
                    this.Type = TstTokenType.AFTER_STEP;
                    break;
                default:
                    this.Type = TstTokenType.TEXT;
                    break;
            }
        }

        public override string ToString()
        {
            return Content + "(" + Type + ")";
        }


    }

    public enum TstTokenType
    {
        TEXT,
        OPEN_BRACKET,
        CLOSE_BRACKET,
        STEP_SEPARATOR,
        LINE_BREAK,
        EMPTY,
        TEST_SUMMARY,
        TEST_PRECONDITION,
        TAG,
        BEFORE_STEP,
        AFTER_STEP,
        CLOSE_PARENTHESIS,
        OPEN_PARENTHESIS,
        COMMA
    }
}
