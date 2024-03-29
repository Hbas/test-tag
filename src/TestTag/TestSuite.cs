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
using System.Collections.Generic;
using System.Xml;
using System.Linq;

namespace TestTag
{
    public class TestSuite
    {
        public string Name { get; private set; }
        public List<TestCase> TestCases { get; private set; }
        public List<TstTag> Tags { get; private set; }
   
        public TestSuite(string name)
        {
            Name = name;
            TestCases = new List<TestCase>();
            Tags = new List<TstTag>();
        }

      
        public void AddTestCase(TestCase tc)
        {
            GetAllChildrenTags(tc, tc.Tags);
            ApplyAll(tc, tc.NormalizedTags);
            TestCases.Add(tc);
        }

        private void GetAllChildrenTags(TestCase tc, List<string> tags)
        {
            foreach (string tag in tags)
            {
                TstTag tstTag = GetTag(tc.Name, tag);
                if (!tc.NormalizedTags.Contains(tag))
                {
                    tc.NormalizedTags.Add(tag);
                    GetAllChildrenTags(tc, tstTag.Tags);                    
                }                
            }
        }

        private void ApplyAll(TestCase tc, List<string> tags)
        {
            foreach (string tag in tags)
            {
                TstTag tstTag = GetTag(tc.Name, tag);
                tstTag.ApplyTo(tc);
            }
        }

        private TstTag GetTag(string tcName, string tag)
        {
            TstTag tstTag = Tags.Where(x => x.Name == tag).FirstOrDefault();
            if (tstTag == null)
            {
                throw new TagNotFoundException(tcName, tag);
            }
            return tstTag;
        }

        public void AddTag(TstTag tag)
        {
            Tags.Add(tag);
        }

        public void AddAllTags(IEnumerable<TstTag> tags)
        {
            foreach(TstTag tag in tags)
                Tags.Add(tag);
        }
    }
}
