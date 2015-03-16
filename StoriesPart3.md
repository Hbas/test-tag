# Part 3: Other topics #
In this section we will show user stories which describes the use of Comments and multiple files with the same suite name.

## Comments ##
User stories:
  * A single line that starts with the token '//' is a comment

Example:
```
suite name {
  test case name {
    //this is a single line comment
    Step => Expected result
  }
}
```

## Using a tag outside a suite ##
User stories:
  * A tag can be outside a suite
  * A tag outside a suite can be used on any file

Example:

Contents of **test1.tst**:
```
TAG: empty { }
suite name {
   testcase (empty ) {
    Step => Expected result
  }
}
```

Contents of **test2.tst**:
```
suite2 {
   testcase (empty) {
    Step => Expected result
  }
}
```

## Using multiple files ##
User stories:
  * If the same suite name is used in multiple files, it will be threated as the same suite

Example:

Contents of **test1.tst**:
```
suite name {
   testcase1 (empty ) {
    A sample step => The expected result
  }
}
```

Contents of **test2.tst**:
```
suite name {
   testcase2 (empty ) {
    A sample step => The expected result
  }
}
```