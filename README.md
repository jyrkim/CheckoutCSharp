# CheckoutCSharp

CheckoutCSharp provides C# code templates that can be used with .NET projects that use [Checkout Finland’s](https://checkout.fi/) payment service. Checkout Finland’s payment service allows customers to use Finnish banks for paying orders on the Internet.

CheckoutCSharp project was made using C#, and it can be used for ASP.NET and ASP.NET MVC Web sites. This project was coded with .NET version 4.6 and Visual Studio 2015.

The [src](../master/src) folder has following subfolders:

* [Checkout](../master/src/Checkout) folder has reusable code for making and receiving payments, and making queries.
* [Content](../master/src/Content) folder has css that has been used for demo purposes.
* [MVC](../master/src/MVC) folder has a controller and views that can be used in ASP.NET MVC project. Current files are for testing but can be used as a kind of template for your own project.
* [WebForm](../master/src/WebForm) folder has WebForms and User Controllers that can be used in ASP.NET project. Current files are for testing that show how to make payments with Checkout Finland.


The [example](../master/example) folder has DemoProject made using Visual Studio 2015 which uses the code in [src](/../master/src) folder.

Special thanks go to *rkioski’s* php GitHub project [CheckoutAPIClient](https://github.com/rkioski/CheckoutAPIClient),  which I used as basis for this project. 

More Checkout Finland [info](https://checkout.fi/materiaalit/tekninen-materiaali/). 

If you have any questions & feedback, you can email me at jyrki.mizaras[at]gmail.com

