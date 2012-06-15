//  Copyright (C) 2012 - TestTag Project
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
using System.IO;
using TestTag.Output;

namespace TestTagTests
{
    [TestClass]
    public class MetricsGeneratorTest
    {
        private const string EXPECTED_METRICS = @"TEST PLAN METRICS:
 - 1 suites
 - 2 test cases
 - 3 steps

TEST CASES BY SUITE:
 - Average: 2
 - Maximum: 2 (S)
 - Minimum: 2 (S)

STEPS BY TEST CASE:
 - Average: 1
 - Maximum: 2 (S -> TC)
 - Minimum: 1 (S -> TC2)
";

        [TestMethod]
        public void TestMetrics()
        {
            var plan = new TestPlan();
            var suites = new TestSuite("S");
            var tc = new TestCase("TC");
            tc.Add(new TestStep("a", "er"));
            tc.Add(new TestStep("a", "er"));
            suites.AddTestCase(tc);
            var tc2 = new TestCase("TC2");
            tc2.Add(new TestStep("a", "er"));
            suites.AddTestCase(tc2);

            plan.Suites.Add(suites);

            var data = new StringWriter();
            using (var generator = new MetricsGenerator(data))
            {
                generator.Write(plan);
            }
            Assert.AreEqual(EXPECTED_METRICS, data.ToString());
        }
    }
}
