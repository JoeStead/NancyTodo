using System;
using System.Collections.Generic;
using Nancy.Hosting.Self;

namespace TodoApi
{
  class Program
  {
    static void Main(string[] args)
    {
      using (var host = new NancyHost(new Uri("http://localhost:4204")))
      {
        host.Start();
        Console.WriteLine("Started!");
        Console.ReadLine();
      }
    }
  }
}


