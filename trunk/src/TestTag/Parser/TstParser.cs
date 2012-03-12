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
using TestTag;

namespace TestTag.Parser
{
    //TODO: Refactor
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

            if (NextTokenTypeIs(TstTokenType.TAG))
            {
                ParseTag(tokenizer);
            }
            else
            {
                ParseSuite(tokenizer);
            }
        }

        public void ParseTag(TstTokenizer tokenizer)
        {
            this.tokens = tokenizer;

            while (tokens.HasMoreTokens())
            {
                RemoveLineBreaks();
                if (NextTokenTypeIs(TstTokenType.TAG))
                {
                    tokens.Next();
                    testPlan.AddTag(ConsumeTag());
                }
                else
                {
                    ParseSuite(tokenizer);
                }
                RemoveLineBreaks();
            }
        }

        public void ParseSuite(TstTokenizer tokenizer)
        {
            this.tokens = tokenizer;
            TestSuite suite = GetSuite(ConsumeString());
            
            Consume(TstToken.OPEN_BRACKET);
            while (!NextTokenTypeIs(TstTokenType.CLOSE_BRACKET))
            {
                RemoveLineBreaks();
                if (NextTokenTypeIs(TstTokenType.TAG))
                {
                    tokens.Next();
                    suite.AddTag(ConsumeTag());
                }
                else
                {
                    ConsumeTestCase(suite);
                }
                RemoveLineBreaks();
            }
            Consume(TstToken.CLOSE_BRACKET);
            RemoveLineBreaks();
            if (tokens.HasMoreTokens())
            {
                Consume(TstToken.EMPTY);
            }
            if (!suite.IsAddedToTheTestPlan)
            {
                testPlan.Add(suite);
            }
        }

        private TstTag ConsumeTag() 
        {
            TstTag tag = new TstTag(ConsumeStringWithoutSpecialChars());
            Consume(TstToken.OPEN_BRACKET);
            RemoveLineBreaks();
            while (!NextTokenTypeIs(TstTokenType.CLOSE_BRACKET))
            {
                if (NextTokenTypeIs(TstTokenType.TEST_PRECONDITION))
                {
                    tokens.Next();
                    tag.Preconditions.Add(ConsumeString());
                }
                else if (NextTokenTypeIs(TstTokenType.AFTER_STEP))
                {
                    tokens.Next();
                    tag.AfterSteps.Add(ConsumeStep());
                }
                else
                {
                    Consume(TstToken.BEFORE);
                    tag.BeforeSteps.Add(ConsumeStep());
                }
                RemoveLineBreaks();
            }
            Consume(TstToken.CLOSE_BRACKET);

            return tag;
        }

        private bool NextTokenTypeIs(TstTokenType type)
        {
            return tokens.Peek().Type == type;
        }

        private void ConsumeTestCase(TestSuite suite)
        {
            TestCase tc = new TestCase(ConsumeStringWithoutSpecialChars());
            if (NextTokenTypeIs(TstTokenType.OPEN_PARENTHESIS))
            {
                AddTags(tc);
            }
            Consume(TstToken.OPEN_BRACKET);
            RemoveLineBreaks();
            while (!NextTokenTypeIs(TstTokenType.CLOSE_BRACKET))
            {                
                if (NextTokenTypeIs(TstTokenType.TEST_SUMMARY))
                {
                    tokens.Next();
                    tc.Summary = ConsumeString();
                }
                else if (NextTokenTypeIs(TstTokenType.TEST_PRECONDITION))
                {
                    tokens.Next();
                    tc.Preconditions.Add(ConsumeString());
                }
                else
                {
                    tc.Steps.Add(ConsumeStep());
                }
                RemoveLineBreaks();
            }
            Consume(TstToken.CLOSE_BRACKET);
            suite.AddTestCase(tc);
        }

        private TestStep ConsumeStep()
        {
            string action = ConsumeStringWithLines();
            Consume(TstToken.STEP_SEPARATOR);
            return new TestStep(action, ConsumeString());
        }

        private void AddTags(TestCase tc)
        {
            Consume(TstToken.OPEN_PARENTHESIS);
            while (!NextTokenTypeIs(TstTokenType.CLOSE_PARENTHESIS))
            {
                tc.Tags.Add(ConsumeStringWithoutSpecialChars());
                if (!NextTokenTypeIs(TstTokenType.CLOSE_PARENTHESIS))
                {
                    Consume(TstToken.COMMA);
                }
            }
            Consume(TstToken.CLOSE_PARENTHESIS);
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
            return ConsumeString(false, true);
        }

        private string ConsumeStringWithoutSpecialChars()
        {
            return ConsumeString(false, false);
        }

        private string ConsumeStringWithLines()
        {
            return ConsumeString(true, true);
        }

        //TODO Refactor
        private string ConsumeString(bool withLineBreaks, bool withPunctuation)
        {
            TstToken token = tokens.Next();
            if (token.Type != TstTokenType.TEXT)
            {
                throw new UnexpectedTokenException("Expected a string but found " + token.Description + " on line " + token.Line);
            }

            TstToken peek = tokens.Peek();
            while (peek.Type == TstTokenType.TEXT ||
                (peek.IsPunctuation && withPunctuation) ||
                (peek.Type == TstTokenType.LINE_BREAK && withLineBreaks))
            {
                TstToken token2 = tokens.Next();                
                token.Concat(token2.Content);
                peek = tokens.Peek();
            }
            return token.Content;
        }

        private TestSuite GetSuite(string name)
        {
            foreach (TestSuite suite in testPlan.Suites)
            {
                if (suite.Name.Equals(name))
                {
                    suite.IsAddedToTheTestPlan = true;
                    return suite;
                }
            }

            TestSuite testSuite = new TestSuite(name);
            testSuite.AddAllTags(testPlan.Tags);
            testSuite.IsAddedToTheTestPlan = false;
            return testSuite;
        }        
    }
}
