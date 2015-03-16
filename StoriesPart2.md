# User Stories Part 2: Tags #
In this section we will show how to use "tags" (or labels) to reuse steps and preconditions

## Defining a tag ##

User stories:
  * An element that starts with the token "TAG:" is a tag
  * A tag can be inside a suite
  * A tag can have preconditions
  * A tag don´t need to have steps
  * A test case can have zero or more tags
  * A tag can have zero or more tags

Example:
```
suite name {
  TAG: empty { }
  TAG: tag1 {
    PRE: All test cases that have this tag will also have this precondition
  }
  TAG: tag2 (tag1) {
    PRE: Another precondition
  }
  testcase (tag2) {
    Step => Expected result
  }
}
```

This example is equivalent to:
```
suite name {
  testcase {
    PRE: All test cases that have this tag will also have this precondition    
    PRE: Another precondition
    Step => Expected result
  }
}
```

## A tag with steps ##

User stories:
  * A tag can have zero or more steps that should be added before the current test steps
  * A tag can have zero or more steps that should be added after the current test steps
  * Tags are applied in the order they are called on the test case

Example:
```
suite name {
  TAG: logout {
    PRE: The user must be logged
    AFTER: Click on logout => The system closes the window
  }

  TAG: check name {
    AFTER: Click on "Home" => The system returns to the initial screen
  }

  TAG: open the menu {
    BEFORE: Open the menu => The system displays the requested screen
  }

  testcase (check name, open the menu, logout) {
    Step => Expected result
  }
}
```

This example is equivalent to:
```
suite name {
  testcase {
    PRE: The user must be logged
    Open the menu => The system displays the requested screen
    Step => Expected result
    Click on "Home" => The system returns to the initial screen
    Click on logout => The system closes the window
  }
}
```

## Tips on tags ##
To be written