using System;
namespace OurLittlePOQ{
public interface IHelloWorld{string HelloWorld();}
public class Tester : IHelloWorld { public string HelloWorld(){ return "Hello World"; }}}
