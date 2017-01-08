# Overview of the ArgData API

The introduction provides a general overview of which files in F1GP that you edit, and a guide to
the basic way that the API provides interaction with these files.


The ArgData API provides a number of classes and functions for editing various parts
of the game Formula One Grand Prix (F1GP).

Classes that perform editing comes in pairs, a Reader and a Writer. If the API can only
read some data, there will only be a Reader class.

The API is generally discoverable, and the goal is that classes have straightforward names
that describe what they do. For instance, to read car colors you use the `CarColorReader` class.

Reader and Writer classes are initialized through static `For` methods that require
references to the files that are being edited.

As en example, the `For` method in the `CarColorReader` takes an instance of a `GpExeFile` as its
input parameter. In this way, the API signals clearly what is required to construct a working
`CarColorReader` objects.

F1GP files that need to be referenced are initialized through static `At` methods that take the
path to the file.

Thus, a `CarColorReader` for a GP.EXE file at `C:\Games\GPRIX\GP.EXE` can be instantiated with:

```
var reader = CarColorReader.For(GpExeFile.At(@"C:\Games\GPRIX\GP.EXE"));
```

With apologies to anyone who hates fluent APIs... :)