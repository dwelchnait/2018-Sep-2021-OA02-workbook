<Query Kind="Program" />

void Main()
{
	string message = "hello world";
	Console.WriteLine(message); // message is an instance of the class string
	Console.WriteLine(message.Substring(2)); //.Substring is a method of the class
	Console.WriteLine(message.Quack()); //.Quack() is NOT a method of the class
	Console.WriteLine(message.Quack(5)); //.Quack(int) is NOT method of the class
}

// You can define other methods, fields, classes and namespaces here

//create an extension method for the class string
//step 1: make a static class
public static class MyExtensionStringMethods
{
//step 2: add your public static string method(s) to this class

	public static string Quack(this string self)
	{
		// the return datatype from the method will be a string
		//	  this is the datatype of the instance we are extending
		// note you do not necessarily need to return a value rdt -> void
		// the 1st parameter (the error msg does used the word argument)
		//		of the method signature identifies the class the extendsion
		//		method is associate with
		// the parameter requires the following syntax -> this datatype variable
		// the contents of the variable will be the contents of the calling
		//		instance
		
		//your logic for the method
		string result = "quack " + self + " quack";
		return result;
	}
	
	public static string Quack(this string self, int times)
	{
	 	string quacks = "";
		for(int i = 0; i < times; i++)
		{
			quacks += "..quack.. ";
		}
		return $"{self} ({quacks})";
	}
}