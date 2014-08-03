# Ruibarbo

Ruibarbo is a library for automating Windows applications that are based on WPF. Among things, it can be used for creating an automated test suite that drives your application through the user interface. That is, it interacts with the application by opening the real user interface, moving the mouse cursor, clicking controls and typing keys into controls.

The most comparable "competitors" are [Coded UI Tests](http://msdn.microsoft.com/en-us/library/dd286726.aspx) and [White](https://github.com/TestStack/White). The word _competitor_ is however put in quotes since this library is very different. Read the goals section for a description of how it is different.

Find the latest version on [nuget.org](http://nuget.org/List/Packages/ruibarbo).

## Where to start

 * [**Goals**](https://github.com/toroso/ruibarbo/wiki/Goals). A brief description of the goals, that is, the reasons why this library has been developed. This part is not that important, and you can skip reading it if you'd like.
 * [**Philisophy**](https://github.com/toroso/ruibarbo/wiki/Philisophy). A rant about values, principles, architecture and organisation. Much of this is valuable also if you do not intend to use Ruibarbo. I claim that following the advice presented here will make you both more efficient and effective. Must read!
 * [**Getting Started**](https://github.com/toroso/ruibarbo/wiki/GettingStarted). Reading this guide should be enough to get pretty far or all the way.
 * [**Documentation**](https://github.com/toroso/ruibarbo/wiki/Documentation). The required docs. Hopefully you will never need to go here.

## Current state and contributing

Ruibarbo is still moving in all sorts of directions. It is currently in use in one real world project and depending on what is discovered there the interface might change. That is, if you decide to use the library you have signed up for a bumpy ride.

Since the design is not really set yet, I think contributions would currently hurt more than help. Still, if you feel you have something you'd like to add, just contact me.

## Requirements

Ruibarbo is targeted for .NET framework 4.0. The ruibarbo.core DLL has no dependencies. There is an optional ruibarbo.nunit DLL that provides extension methods for using [NUnit's (2.6.3)](http://www.nunit.org/) assertion syntax. Other than that, there are no dependencies.

## Reporting Bugs

Bugs are reported under issues.

## Credits and Contact

Twitter: @ruibarbolib  
E-mail: &#114;&#117;&#105;&#98;&#97;&#114;&#98;&#111; &#91;&#97;&#116;&#93; &#107;&#97;&#108;&#105;&#110; &#91;&#100;&#111;&#116;&#93; &#101;&#117;
