Copyright (C) 2012 - Henrique Borges

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.

You should have received a copy of the source code along with this binary.  
If not, see <http://code.google.com/p/test-tag/>.
=============================
USAGE:

1. Install the TestTag compiler
2. Copy the executable file (TestTag.exe) to the directory with your test cases ("*.tst" files)

INTEGRATION WITH TESTLINK:
3. Run the executable. It will process all "tst" files and create a "tests.xml" output
4. Import the "tests.xml" as the root (blank) suite in TestLink

TO GENERATE A HTML:
3. Run the executable file passing "-html" as an argument. It will process all "tst" files and create a "tests.htm" output.
4. If you want to chance the template (for internacionalization, for example) edit the file "planTemplate.txt". On the TestTag wiki there are some sugestions.

TO GENERATE A PDF:
3. Run the executable file passing "-pdf" as an argument. It will process all "tst" files and create a "tests.pdf" output.
4. If you want to replace the company/project logo on the PDF, pass "-pdf -logo <replace-with-actual-image-path>" as an argument to the compiler. Valid extensions are jpg, jpeg, gif and png.
============================

