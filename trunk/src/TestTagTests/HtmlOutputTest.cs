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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using TestTag.Output;
using TestTag;

namespace TestTagTests
{
    [TestClass]
    public class HtmlOutputTest
    {
        [TestMethod]
        [DeploymentItem(@"TestTag\planTemplate.htm")]
        public void HtmlOutput()
        {
            //Pre-conditions
            Assert.IsTrue(File.Exists("planTemplate.htm"), "deployment failed: file did not get deployed");

            StringWriter writer = new StringWriter();
            TestPlan plan = new TestPlan();
            plan.Add(new TestSuite("Suite name"));
            using (HtmlOutputGenerator output = new HtmlOutputGenerator(writer))
            {                
                output.Write(plan);
            }
            string expected = "<html>Suite name</html>";
            Assert.AreEqual(expected, writer.ToString());
        }
    }
}
