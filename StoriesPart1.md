# User Stories Part 1: Getting started #
In this section we will show the first user stories, that describes how to write your first test cases using our language. All examples compile with no errors.

## A "hello world" suite ##
User stories:
  * A test suite must have a name
  * A test case must have a name
  * A test case must be in a suite
  * A test case must have at least one step and it´s expected result

Example:
```
suite name {
  test case name {
    A sample step => The expected result
  }
}
```

## More steps ##

User stories:
  * A test suite can have more than one test case
  * A test case can have more than one step
  * A test step can have more than one line
  * A test step must have a single line expected result

Example:
```
suite {
  test 1 {
    Press enter after the result => The compiler will wait for the next step or a closing bracket
    Type something => It will be the second step
  }

  test 2 {
    A multi-line step can be created
    Just press enter => The step will have a line break on it
  }

  test 3 {
    You cant create a two line expected result => As the following line is treated as a new step
  }
}
```

## Test description ##

User stories:
  * A test case can have a test description

Example:
```
suite name {
  test case name {
    DESCRIPTION: Begin a line with "DESCRIPTION:" and you can type a single line test description
    A sample step => The expected result
  }
}
```

## Preconditions ##

User stories:
  * A test case can have one or more preconditions

Example:
```
suite name {
  test case name {
    PRE: Begin a line with "PRE:" and you can type a single line test precondition
    PRE: You can put more than one precondition on a test case
    A sample step => The expected result
  }
}
```

## Tips and tricks ##
To be written