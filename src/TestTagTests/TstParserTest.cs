//  Copyright (C) 2012 - Henrique Borges <henriqueborges@gmail.com>
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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestTag.Parser;
using TestTag.Models;

namespace TestTagTests
{
    [TestClass]
    public class TstParserTest
    {
        private TstParser parser;

        [TestInitialize]
        public void Setup()
        {
            this.parser = new TstParser();
        }

        #region Acessors for test code legibility

        private TestSuite Suite
        {
            get
            {
                return parser.TestPlan.Suites[0];
            }
        }

        private List<TestCase> TestCases
        {
            get
            {
                return Suite.TestCases;
            }
        }


        private TestCase TestCase
        {
            get
            {
                return TestCases[0];
            }
        }

        private TestStep Step
        {
            get
            {
                return TestCase.Steps[0];
            }
        }
        #endregion

        [TestMethod]
        public void WithOneStep()
        {
            string tst = "suite { testcase { asd => bcd } }";
            parser.Parse(TstTokenizer.FromContent(tst));
            Assert.AreEqual(1, parser.TestPlan.Suites.Count);
            Assert.AreEqual("suite", Suite.Name);
            Assert.AreEqual(1, TestCases.Count);

            Assert.AreEqual(1, TestCase.Steps.Count);
            Assert.AreEqual("asd", TestCase.Steps[0].Action);
            Assert.AreEqual("bcd", TestCase.Steps[0].ExpectedResult);
        }

        [TestMethod]
        public void WithLineBreak()
        {
            string tst = "suite { testcase { asd \n Xyz => bcd } }";
            parser.Parse(TstTokenizer.FromContent(tst));
            Assert.AreEqual(1, TestCases.Count);
            Assert.AreEqual(1, TestCase.Steps.Count);
            Assert.AreEqual("asd \n Xyz", Step.Action);
            Assert.AreEqual("bcd", Step.ExpectedResult);
        }

        [TestMethod]
        public void WithTwoSteps()
        {
            string tst = "suite { testcase { a1 => b1\na2 => b2 } }";
            parser.Parse(TstTokenizer.FromContent(tst));
            Assert.AreEqual(1, TestCases.Count);
            Assert.AreEqual(2, TestCase.Steps.Count);
            Assert.AreEqual("a1", Step.Action);
            Assert.AreEqual("b1", Step.ExpectedResult);
            Assert.AreEqual("a2", TestCase.Steps[1].Action);
            Assert.AreEqual("b2", TestCase.Steps[1].ExpectedResult);
        }

        [TestMethod]
        public void WithObjAndPrecondition()
        {
            string tst = "suiteCase { testcase { DESCRIPTION: objective\n PRE: precondition1\n PRE: precondition2\n a1 => b1} }";
            parser.Parse(TstTokenizer.FromContent(tst));
            Assert.AreEqual("suiteCase", Suite.Name);
            Assert.AreEqual(1, TestCases.Count);
            Assert.AreEqual("objective", TestCase.Summary);
            Assert.AreEqual(2, TestCase.Preconditions.Count);
            Assert.AreEqual("precondition1", TestCase.Preconditions[0]);
            Assert.AreEqual("precondition2", TestCase.Preconditions[1]);
        }

        [TestMethod]
        public void TwoTestCases()
        {
            string tst = "SuiteName { \n testcase {\n a1 => b1\n\n a2 => c3 } \n\n testcase2 { a2 => b2 } }";
            parser.Parse(TstTokenizer.FromContent(tst));
            Assert.AreEqual(2, TestCases.Count);
        }
    }
}
