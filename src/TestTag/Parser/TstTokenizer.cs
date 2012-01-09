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
using System.IO;

namespace TestTag.Parser
{
    public class TstTokenizer
    {
        #region Initialization
        public static TstTokenizer FromContent(string fileContent)
        {
            return new TstTokenizer(new StringReader(fileContent));
        }

        public static TstTokenizer FromFilePath(string filePath)
        {
            return new TstTokenizer(new StreamReader(filePath));
        }

        #endregion

        #region Parsing
        private List<TstToken> tokens = new List<TstToken>();
        private StringBuilder currentToken = new StringBuilder();
        private int currentLine = 1;

        private TstTokenizer(TextReader reader)
        {
            for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
            {
                if (IsComment(line))
                    continue;
                foreach (char c in line)
                {
                    switch (c)
                    {
                        case '{':                        
                        case '}':
                        case '(':
                        case ')':
                        case ',':
                            EndToken();
                            currentToken.Append((char)c);
                            EndToken();
                            break;
                        case ' ':
                        case '\t':
                        case '\r':
                            EndToken();
                            break;
                        default:
                            currentToken.Append((char)c);
                            break;
                    }
                }
                EndToken();
                tokens.Add(TstToken.LineBreak(currentLine++));
            }
            EndToken();
            currentToken = null;
        }

        private bool IsComment(string line)
        {
            return line.Trim().StartsWith("//");
        }

        private void EndToken()
        {
            if (currentToken.Length > 0)
            {
                tokens.Add(new TstToken(currentToken.ToString(), currentLine));
                currentToken.Length = 0;
            }
        }

        #endregion

        public bool HasMoreTokens()
        {
            return tokens.Count != 0;
        }

        public TstToken Peek()
        {
            if (!HasMoreTokens())
                return TstToken.EMPTY;
            return tokens.FirstOrDefault();
        }

        public TstToken Next()
        {
            if (!HasMoreTokens())
                return TstToken.EMPTY;
            var token = tokens[0];
            tokens.RemoveAt(0);
            return token;
        }
    }
}
