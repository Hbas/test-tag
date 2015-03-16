# Introduction #

This page describes the TestTag language. It begins with an overview of the language and follows with the user stories that guided the compiler development, containing some increasing complex examples.

# Language Overview #

The language was created with this things in mind:
  * **Avoid verbosity**: Test cases are really simple structures, so why fill the language with boilerplate syntax? We have curly braces and an "arrow"  (i.e. => ) as the main language elements.
  * **Based on Testlink, not restricted to**: The language was created to be used with Testlink but is not restricted to it. The language will be compiled/exported to other programs/formats in the future.
  * **Let the text editor help you**: The syntax/usage tips should be created to use the full power of the modern text editors, facilitating operations like _find and replace_ (with or without regular expressions), _column editing_, _syntax highlighting_ and _syntax folding_.
  * **You need a suite for a test case**: All test cases should be part of a test suite.

# User Stories #

[Part 1: Getting Started](StoriesPart1.md)

[Part 2: Reuse](StoriesPart2.md)

[Part 3: Other topics](StoriesPart3.md)