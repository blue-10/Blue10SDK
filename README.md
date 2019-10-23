# Blue10 SDK

![Nuget](https://img.shields.io/nuget/v/blue10sdk?label=Nuget&logo=nuget)  

![alt text](https://login.blue10.com/Content/images/Blue10-Logo-RGB-156.png "blue10 logo")

This is the Blue 10 .NET Core SDK.

## Request an environment at Blue10

1. To start using Blue10 you first need to create and environment.
[Contact Blue 10](https://www.blue10.com/contact/).
1. After an environment has been created, request an API key. During the API key creation process our staff will assess which features your application requires and will attach that feature-set to your personal environment's API key.
1. Log in to your blue10 Environment and create a new Company with Erp Adapter `API`

## Getting started

For this example we will create a simple _console_ adapter that synchronises vendor information.

### 1. Create a new  visual studio project

In the main Program.cs you will see this:

```cs
using System;

namespace GettingStartedWithBlue10
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
 ```

### 2. Get the Blue10 Package from nuget

```bat
dotnet add package Blue10SDK --version 0.1.2
```

### 3. Create a desk

#### Simple desk

 ```cs
using System;

class Program
{
    static void Main(string[] args)
    {
        //Build our blue10 client service
        var b10client = Blue10.CreateClient("<Your personal API Key>");
    }
}
 ```

### OR

#### Add a desk to `Microsoft.Extensions.DependencyInjection`'s IServiceCollection

 ```cs
//When configuring ASP.Net Core , Azure Functions or IHosterService projects

public void ConfigureServices(IServiceCollection services)
{
    service.AddBlue10("<Your personal API Key>")
    //..
    //Other services
}

// And then use the client through dependency injection:
public class MyClass
{
    readonly IBlue10Client client;

    public MyClass(IBlue10Client _blue10Client)
    {
        client = _blue10Client;
    }
}
```

### 4. upload some vendors

```cs
using System;

namespace GettingStartedWithBlue10
{
    class Program
    {
        static void Main(string[] args)
        {
            //Build our blue10 client service
            var b10client = Blue10.CreateClient("");

            //retreive your company 
            var myCompanyId = b10client
                .GetCompanies()
                .FirstOrDefault(x => x.id == "<MyCompany>");

            //Create some vendors
            var vendors = new List<Vendor>
            {
                new Vendor{
                        id_company = myCompanyId.id,
                        administration_code = "1",
                        name = "albert heijn"
                    },

                new Vendor{
                        id_company = myCompanyId.id,
                        administration_code = "2",
                        name = "c1000"
                    },

                new Vendor{
                        id_company = myCompanyId.id,
                        administration_code = "3",
                        name = "coop"
                    }
            };

            //Add vendors to blue10
            vendors.ForEach(x => b10client.AddVendor(x));

            Console.WriteLine("Done");
        }
    }
}
 ```
 