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
using TestTag.Models;

namespace TestTag.Parser
{
    public class TstParser
    {
        private TestPlan testPlan = new TestPlan();
        private TstTokenizer tokens;

        public TestPlan TestPlan
        {
            get
            {
                return testPlan;
            }
        }       

        public void Parse(TstTokenizer tokenizer)
        {
            this.tokens = tokenizer;
            TestSuite suite = new TestSuite(ConsumeString());
            Consume(TstToken.OPEN_BRACKET);
            while (tokens.Peek().Type != TstTokenType.CLOSE_BRACKET)
            {
                RemoveLineBreaks();
                ConsumeTestCase(suite);
                RemoveLineBreaks();
            }
            Consume(TstToken.CLOSE_BRACKET);
            RemoveLineBreaks();
            if (tokens.HasMoreTokens())
            {
                Consume(TstToken.EMPTY);
            }
            testPlan.Add(suite);
        }

        private void ConsumeTestCase(TestSuite suite)
        {
            TestCase tc = new TestCase(ConsumeString());
            Consume(TstToken.OPEN_BRACKET);
            while (tokens.Peek().Type != TstTokenType.CLOSE_BRACKET)
            {
                RemoveLineBreaks();
                if (tokens.Peek().Type == TstTokenType.TEST_SUMMARY)
                {
                    tokens.Next();
                    tc.Summary = ConsumeString();
                }
                else if (tokens.Peek().Type == TstTokenType.TEST_PRECONDITION)
                {
                    tokens.Next();
                    tc.Preconditions.Add(ConsumeString());
                }
                else
                {
                    string action = ConsumeStringWithLines();
                    Consume(TstToken.STEP_SEPARATOR);
                    tc.AddStep(action, ConsumeString());
                }
                if (tokens.Peek().Type != TstTokenType.CLOSE_BRACKET)
                {
                    Consume(TstToken.LINE_BREAK);
                }
            }
            Consume(TstToken.CLOSE_BRACKET);
            suite.TestCases.Add(tc);
        }

        private void RemoveLineBreaks()
        {
            while (tokens.Peek().Type == TstTokenType.LINE_BREAK)
            {
                tokens.Next();
            }
        }

        private void Consume(TstToken expected)
        {
            TstToken token = tokens.Next();
            if (token.Type != expected.Type)
            {
                throw new UnexpectedTokenException("Expected " + expected.Description + " but found " + token.Description + " on line " + token.Line);
            }
        }

        private string ConsumeString()
        {
            return ConsumeString(false);
        }

        private string ConsumeStringWithLines()
        {
            return ConsumeString(true);
        }

        private string ConsumeString(bool withLineBreaks)
        {
            TstToken token = tokens.Next();
            if (token.Type != TstTokenType.TEXT)
            {
                throw new UnexpectedTokenException("Expected a string but found " + token.Description + " on line " + token.Line);
            }

            TstToken peek = tokens.Peek();
            while (peek.Type == TstTokenType.TEXT || (peek.Type == TstTokenType.LINE_BREAK && withLineBreaks))
            {
                TstToken token2 = tokens.Next();
                token.Content += " " + token2.Content;
                peek = tokens.Peek();
            }
            return token.Content;
        }


    }
}
