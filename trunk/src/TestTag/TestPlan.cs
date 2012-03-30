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
using System.Xml;

namespace TestTag
{
    public class TestPlan 
    {
        public List<TestSuite> Suites { get; private set; }
        public List<TstTag> Tags { get; private set; }

        public TestPlan()
        {
            Suites = new List<TestSuite>();
            Tags = new List<TstTag>();
        }

        public void Add(TestSuite suite)
        {
            Suites.Add(suite);
        }

        public void AddTag(TstTag tag)
        {
            Tags.Add(tag);
        }

        public TestSuite GetSuite(string name)
        {
            foreach (TestSuite suite in Suites)
            {
                if (suite.Name.Equals(name))
                {
                    return suite;
                }
            }
            TestSuite testSuite = new TestSuite(name);
            testSuite.AddAllTags(Tags);
            Suites.Add(testSuite);
            return testSuite;
        }
    }
}
