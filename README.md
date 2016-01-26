##To Do List

###Description
Simple to do list with tasks and categories. With unit testing using Microsoft SQL Server, Nancy 1.3.0, XUnit 2.1.0.
CRUD functionality for

###Instructions
Download source code and run the following lines in PowerShell from the project folder

```
> dnvm install latest
> dnu restore
> dnx kestrel
```

App production database named `todo` created using these commands in PowerShell **SQLCMD**

```
1> CREATE DATABASE todo;
2> GO
1> USE todo;
2> GO
1> CREATE TABLE tasks
2> (
3>   id INT IDENTITY (1,1),
4>   description VARCHAR(255),
5>   categoryId INT
6> );
7> CREATE TABLE categories
8> (
9>   id INT IDENTITY (1,1),
10>   name VARCHAR(255)
11> );
12> GO
```

Testing database is a clone of this named `todo_test`

To run tests, use the PowerShell command `> dnx test`
